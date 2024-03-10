using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        #region Constructor
        public PlatformsController()
        {

        }
        #endregion

        #region HttpGet
        [HttpGet]
        public ActionResult TestMe()
        {
            return Ok("Hi from Command Service.");
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("Inbound test of from Platforms Controller");
        }
        #endregion
    }
}
