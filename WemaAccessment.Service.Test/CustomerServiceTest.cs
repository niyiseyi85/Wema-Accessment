using System.Linq.Expressions;
using AutoMapper;
using Castle.Core.Resource;
using Moq;
using WemaAccessment.Common.Integration;
using WemaAccessment.Data.Dto;
using WemaAccessment.Data.IRepository;
using WemaAccessment.Data.Models;
using WemaAccessment.Data.ResponseModels;
using WemaAccessment.Service.Service;

namespace WemaAccessment.Service.Test
{
  public class CustomerServiceTest
  {
    private readonly Mock<IOTPService> otpServiceMock = new Mock<IOTPService>();
    private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();
    private readonly CustomerService customerService;
    public CustomerServiceTest()
    {
      customerService = new CustomerService(otpServiceMock.Object, mapperMock.Object, unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task OnboardCustomer_ValidCustomer_Success()
    {
      var customerDto = new CustomerDto
      {
        PhoneNumber = "1234567890",
        Email = "test@example.com",
        Password = "password123",
        StateOfResidence = "Lagos",
        LGA = "Epe"
      };

      var customer = new Customer
      {
        PhoneNumber = customerDto.PhoneNumber,
        Email = customerDto.Email,
        Password = customerDto.Password,
        StateOfResidence = customerDto.StateOfResidence,
        LGA = customerDto.LGA
      };

      var responseModel = new ResponseModel();
      mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customer);
      unitOfWorkMock.Setup(u => u.CustomerRepository).Returns(customerRepositoryMock.Object);
      customerRepositoryMock.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(customer);
      unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);
      otpServiceMock.Setup(o => o.SendOTP(customerDto.PhoneNumber));

      // Act
      var result = await customerService.OnboardCustomer(customerDto);

      // Assert
      Assert.False(result.HasError);
      Assert.Equal("Check Phone number for OTP verification", result.Message);
      unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
      otpServiceMock.Verify(o => o.SendOTP(customerDto.PhoneNumber), Times.Once);
    }

    [Fact]
    public async Task GetAllCustomer_WithRecords_ReturnsRecords()
    {
      var customers = new List<Customer>
      {
        new Customer{ Email = "abc@gmail.com", LGA = "ifo", PhoneNumber = "0912121234", StateOfResidence = "Ogun", Id= 1, IsVerified=true, Password = "qwakkddn" },
        new Customer{ Email = "xyz@gmail.com", LGA = "ifo", PhoneNumber = "0802334454", StateOfResidence = "Ogun", Id= 2, IsVerified=true, Password = "qwakkddn" },
        new Customer{ Email = "ada@gmail.com", LGA = "ifo", PhoneNumber = "0912347577", StateOfResidence = "Ogun", Id= 3, IsVerified=true, Password = "qwakkddn" }
      };

      var expectedCustomerDtos = new List<GetCustomerDto>
      {
        new GetCustomerDto{ Email = "abc@gmail.com", LGA = "ifo", PhoneNumber = "0912121234", StateOfResidence = "Ogun"},
        new GetCustomerDto{ Email = "xyz@gmail.com", LGA = "ifo", PhoneNumber = "0802334454", StateOfResidence = "Ogun"},
        new GetCustomerDto{ Email = "ada@gmail.com", LGA = "ifo", PhoneNumber = "0912347577", StateOfResidence = "Ogun"}
      };


      unitOfWorkMock.Setup(u => u.CustomerRepository).Returns(customerRepositoryMock.Object);
      customerRepositoryMock.Setup(x => x.GetAll(
    It.IsAny<Expression<Func<Customer, bool>>>(),
    It.IsAny<Func<IQueryable<Customer>, IOrderedQueryable<Customer>>>(),
    It.IsAny<string>())).ReturnsAsync(customers);



      mapperMock.Setup(m => m.Map<List<GetCustomerDto>>(customers)).Returns(expectedCustomerDtos);

      // Act
      var result = await customerService.GetAllCustomer();

      // Assert
      Assert.False(result.HasError);
      Assert.Equal($"{customers.Count} record(s) found", result.Message);
      Assert.Equal(expectedCustomerDtos, result.Data);
    }

    [Fact]
    public async Task GetAllCustomer_NoRecords_ReturnsNoRecordsMessage()
    {
      var customer = new List<Customer>();

      unitOfWorkMock.Setup(u => u.CustomerRepository).Returns(customerRepositoryMock.Object);
      customerRepositoryMock.Setup(x => x.GetAll(
    It.IsAny<Expression<Func<Customer, bool>>>(),
    It.IsAny<Func<IQueryable<Customer>, IOrderedQueryable<Customer>>>(),
    It.IsAny<string>())).ReturnsAsync(customer);

      // Act
      var result = await customerService.GetAllCustomer();

      // Assert
      Assert.True(result.HasError);
      Assert.Equal("No record found", result.Message);
      Assert.Null(result.Data);
    }
    [Fact]
    public async Task VerifyCustomer_NoCustomerFound_ReturnsNoCustomerMessage()
    {
      // Arrange
      var customer = new Customer();

      unitOfWorkMock.Setup(u => u.CustomerRepository).Returns(customerRepositoryMock.Object);
      
      customerRepositoryMock.Setup(repo => repo.FirstOrDefault(
    It.IsAny<Expression<Func<Customer, bool>>>(),
    It.IsAny<string>())).ReturnsAsync(null as Customer);
      var verifyCustomerDto = new VerifyCustomerDto { PhoneNumber = "1234567890", OTP = 1234567 };

      // Act
      var result = await customerService.VerifiyCustomer(verifyCustomerDto);

      // Assert
      Assert.True(result.HasError);
      Assert.Equal("No customer found for verification with the Phone Number provided", result.Message);
    }

    [Fact]
    public async Task VerifyCustomer_CustomerEmailAlreadyVerified_ReturnsAlreadyVerifiedMessage()
    {
      // Arrange
      

      var existingCustomer = new Customer { IsVerified = true }; 

      unitOfWorkMock.Setup(u => u.CustomerRepository).Returns(customerRepositoryMock.Object);
      
      customerRepositoryMock.Setup(repo => repo.FirstOrDefault(
        It.IsAny<Expression<Func<Customer, bool>>>(),
        It.IsAny<string>())).ReturnsAsync(existingCustomer);
          

      var verifyCustomerDto = new VerifyCustomerDto { PhoneNumber = "1234567890", OTP = 123456 };

      // Act
      var result = await customerService.VerifiyCustomer(verifyCustomerDto);

      // Assert
      Assert.True(result.HasError);
      Assert.Equal("Customer email already verified", result.Message);
    }
  }
}