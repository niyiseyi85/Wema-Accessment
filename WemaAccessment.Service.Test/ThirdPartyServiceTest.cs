using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WemaAccessment.Common.Integration;
using WemaAccessment.Data.Dto;
using WemaAccessment.Data.ResponseModels;
using WemaAccessment.Service.IService;
using WemaAccessment.Service.Service;

namespace WemaAccessment.Service.Test
{
  public class ThirdPartyServiceTest
  {
    private readonly Mock<IThirdPartyIntegration> thirdPartyIntegrationMock = new Mock<IThirdPartyIntegration>();
    private readonly ThirdPartyService thirdPartyService;

    public ThirdPartyServiceTest()
    {
      thirdPartyService = new ThirdPartyService(thirdPartyIntegrationMock.Object);
    }
    [Fact]
    public async Task GetBankList_Success_ReturnsBanksList()
    {
      // Arrange
      var bankList = new List<BankModel>
      {
        new BankModel { BankName = "Bank A", BankCode = "001" },
        new BankModel { BankName = "Bank B", BankCode = "002" },
      };

      var responseModel = new GetBankListResponseModel
      { HasError = false, Result = bankList, ErrorMessage = "Banks retrieved successfully" };

      thirdPartyIntegrationMock.Setup(x => x.GetBankList())
          .ReturnsAsync(responseModel);

      // Act
      var result = await thirdPartyService.GetBankList();

      // Assert
      Assert.False(result.HasError);
      Assert.Equal("Banks retrieved successfully", result.Message);
      Assert.NotNull(result.Data);
      Assert.Equal(bankList.Count, result.Data.Count);
      
    }

    [Fact]
    public async Task GetBankList_Error_ReturnsErrorMessage()
    {
      // Arrange
      var errorMessage = "Error occurred while retrieving banks";

      var responseModel = new GetBankListResponseModel
      { HasError = true,  ErrorMessage = errorMessage };

      thirdPartyIntegrationMock.Setup(tpi => tpi.GetBankList())
          .ReturnsAsync(responseModel);

      // Act
      var result = await thirdPartyService.GetBankList();

      // Assert
      Assert.True(result.HasError);
      Assert.Equal(errorMessage, result.Message);
      Assert.Null(result.Data);
    }
  }
}
