using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("OnlyIT")]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;

        public PetController(ILogger<PetController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            return Ok();
        }
    }
}
