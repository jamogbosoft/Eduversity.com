using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAddressesController : ControllerBase
    {
        private readonly IUserAddressService _userAddressService;

        public UserAddressesController(IUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<UserAddressResponse>>> GetAddress()
        {
            var result =  await _userAddressService.GetAddress();
            return Ok(result);
        }

        [HttpGet("{userId}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<UserAddressResponse>>> GetAddress(long userId)
        {
            var result = await _userAddressService.GetAddress(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<UserAddressResponse>>> AddOrUpdateAddress(UserAddressResponse address)
        {
            var result = await _userAddressService.AddOrUpdateAddress(address);
            return Ok(result);
        }
    }
}
