using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Data.ViewModels.Authentication;
using my_books.Exceptions;

namespace my_books.Controllers
{
    [Authorize(Roles = UserRoles.Publisher + "," + UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService _service;
        private readonly ILogger<PublishersController> _logger;

        public PublishersController(PublishersService service, ILogger<PublishersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetPublishers(string sortBy, string searchString, int pageNumber)
        {
            try
            {
                _logger.LogInformation("This is just a log in GetAllPublishers()");
                var publishers = _service.GetPublishers(sortBy, searchString, pageNumber);
                return Ok(publishers);
            }
            catch (Exception)
            {
                return BadRequest("Sory, we couldn't load publihsers");
            }
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
            try
            {
                var newPublisher = _service.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
            
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
            try
            {
                // var publisher = _service.GetPublisherById(id);
                _service.DeletePublisher(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
