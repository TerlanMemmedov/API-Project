using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Data.ViewModels.Authentication;

namespace my_books.Controllers
{
    [Authorize(Roles = UserRoles.Author + "," + UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorsService _service;

        public AuthorsController(AuthorsService service)
        {
            _service = service;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllAuthors()
        {
            var authors = _service.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _service.GetAuthorById(id);
            if (author is null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorWithBooksById(int id)
        {
            var author = _service.GetAuthorWithBooks(id);
            if (author is null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            //if ModelState

            _service.AddAuthor(author);
            return NoContent();
        }

        [HttpPut("update-author/{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorVM author)
        {
            var _author = _service.GetAuthorById(id);
            if (_author is null)
            {
                return NotFound();
            }
            var updatedAuthor = _service.UpdateAuthor(id, author);
            return Ok(updatedAuthor);
        }

        [HttpDelete("delete-author/{id}")]
        public IActionResult DeleteAuthor(int id) 
        {
            var author = _service.GetAuthorById(id);
            if (author is null)
            {
                return NotFound();
            }
            _service.DeleteAuthor(id);
            return NoContent();
        }
    }
}
