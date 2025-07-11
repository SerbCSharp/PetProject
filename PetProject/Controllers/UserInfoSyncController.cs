using Microsoft.AspNetCore.Mvc;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoSyncController : ControllerBase
    {
        [HttpGet("user-info")]
        public IActionResult GetUserInfo()
        {
        }
    }
}
