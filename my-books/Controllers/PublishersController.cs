using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService _service;

        public PublishersController(PublishersService service)
        {
            _service = service;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetPublishers()
        {
            var publishers = _service.GetPublishers();
            return Ok(publishers);
        }


        [HttpGet("get-publisher/{id}")]
        public IActionResult GetPublisher(int id)
        {
            var publisher = _service.GetPublisherById(id);
            if (publisher is null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }


        [HttpGet("get-publisher-with-book-and-author/{id}")]
        public IActionResult GetPublisherWithBookAndAuthor(int id)
        {
            var publisher = _service.GetPublisherWithBookAndAuthor(id);
            if (publisher is null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }
        


        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            _service.AddPublisher(publisher);
            return NoContent();
        }

        [HttpPut("update-publisher/{id}")]
        public IActionResult UpdatePublisher(int id, [FromBody] PublisherVM publisher)
        {
            var _publisher = _service.GetPublisherById(id);
            if (_publisher is null)
            {
                return NotFound();
            }

            var updatedPublisher = _service.UpdatePublisher(id, publisher);
            return Ok(updatedPublisher);
        }

        [HttpDelete("delete-publisher/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            var publisher = _service.GetPublisherById(id);
            if (publisher is null)
            {
                return NotFound();
            }

            _service.DeletePublisher(id);
            return NoContent();
        }
    }
}
