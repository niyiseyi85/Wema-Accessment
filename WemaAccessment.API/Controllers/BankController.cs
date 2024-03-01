using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WemaAccessment.Service.IService;
using WemaAccessment.Service.Service;

namespace WemaAccessment.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BankController : ControllerBase
  {
    private readonly IThirdPartyService _thirdPartyService;

    public BankController(IThirdPartyService thirdPartyService)
    {
      _thirdPartyService = thirdPartyService;
    }
    /// <summary>
    /// Get Bank list
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetBankList")]
    public async Task<IActionResult> Getbank()
    {
      var response = await _thirdPartyService.GetBankList();
      if (response.HasError)
      {
        return BadRequest(response);
      }
      return Ok(response);
    }
  }
}
