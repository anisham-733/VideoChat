using Microsoft.AspNetCore.Mvc;
using VideoChat.Services;

namespace VideoChat.Controllers
{
    [

        ApiController,
        Route("api/video")
    ]
    public class VideoController : Controller
    {
        private readonly IVideoService _service;
        public VideoController( IVideoService service )
        {
            _service = service;
        }
        [HttpGet("token")]
        public IActionResult GetToken() => new JsonResult(new
        {
            token = _service.GetTwilioJwt(User.Identity.Name)
        });

        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
            =>new JsonResult(await _service.GetAllRoomsAsync());
        
    }
}
