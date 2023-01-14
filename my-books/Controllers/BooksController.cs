using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Data.ViewModels.Authentication;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _service;

        public BooksController(BooksService service)
        {
            _service = service;
        }

        [Authorize(Roles = UserRoles.Author + "," + UserRoles.User)]
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var books = _service.GetAllBooks();
            return Ok(books);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _service.GetBookById(id);
            if (book is null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest();
            }

            _service.AddBook(book);
            return NoContent();
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookVM book)
        {
            if (!ModelState.IsValid) { }
            var updatedBook = _service.UpdateBook(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("delete-book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _service.GetBookById(id);
            if (book is null)
            {
                return NotFound();
            }

            _service.DeleteBook(id);
            return Ok();
        }
    }
}
