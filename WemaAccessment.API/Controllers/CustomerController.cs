using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WemaAccessment.Data.Dto;
using WemaAccessment.Data.ResponseModels;
using WemaAccessment.Service.IService;

namespace WemaAccessment.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly ICustomerService _customerService;
    private readonly IValidator<CustomerDto> _customerDtoValidator;

    public CustomerController(ICustomerService customerService, IValidator<CustomerDto> customerDtoValidator)
    {
      _customerService = customerService;
      _customerDtoValidator = customerDtoValidator;
    }
    /// <summary>
    /// Endpoint to all customers
    /// </summary>
    [HttpGet("GetAllCustomer")]
    [ProducesResponseType(typeof(ResponseModel<List<GetCustomerDto>>), 200)]
    public async Task<IActionResult> GetAllCustomers()
    {
      var allCustomerList = await _customerService.GetAllCustomer();
      if (allCustomerList.HasError)
      {
        return BadRequest(allCustomerList);
      }
      return Ok(allCustomerList);
    }

    /// <summary>
    /// Onboard customer
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("OnboardCustomer")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> OnboardCustomer(CustomerDto request)
    {
      var validateModel = await _customerDtoValidator.ValidateAsync(request);
      if (!validateModel.IsValid)
      {
        return BadRequest(validateModel.ToString());
      }
      var response = await _customerService.OnboardCustomer(request);
      if (response.HasError)
      {
        return BadRequest(response);
      }
      return Ok(response);
    }

    /// <summary>   
    /// Verify customer
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("VerifyCustomer")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> VerifyCustomer(VerifyCustomerDto request)
    {
      var response = await _customerService.VerifiyCustomer(request);
      if (response.HasError)
      {
        return BadRequest(response);
      }
      return Ok(response);
    }
  }
}
