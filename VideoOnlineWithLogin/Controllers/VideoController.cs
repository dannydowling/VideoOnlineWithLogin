using VideoOnlineWithLogin.Api.Models;
using VideoOnlineWithLogin.Shared;
using Microsoft.AspNetCore.Mvc;

namespace VideoOnlineWithLogin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;

        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        [HttpGet]
        public IActionResult GetAllVideos()
        {
            return Ok(_videoRepository.GetAllVideos());
        }

        [HttpGet("{id}")]
        public IActionResult GetVideoById(int id)
        {
            return Ok(_videoRepository.GetVideoById(id));
        }

        [HttpPost]
        public IActionResult CreateVideo([FromBody] Video video)
        {
            if (video == null)
                return BadRequest();

            if (video.videoName == string.Empty)
            {
                ModelState.AddModelError("Name missing", "Name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdVideo = _videoRepository.AddVideo(video);

            return Created("video", createdVideo);
        }

        [HttpPut]
        public IActionResult UpdateVideo([FromBody] Video video)
        {
            if (video == null)
                return BadRequest();

            if (video.videoName == string.Empty)
            {
                ModelState.AddModelError("Name missing", "Name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var videoToUpdate = _videoRepository.GetVideoById(video.videoId);

            if (videoToUpdate == null)
                return NotFound();

            _videoRepository.UpdateVideo(video);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideo(int id)
        {
            if (id == 0)
                return BadRequest();

            var videoToDelete = _videoRepository.GetVideoById(id);
            if (videoToDelete == null)
                return NotFound();

            _videoRepository.DeleteVideo(id);

            return NoContent();//success
        }
    }
}
