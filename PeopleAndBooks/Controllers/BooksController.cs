using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAndBooks.Business;
using PeopleAndBooks.Model;

namespace PeopleAndBooks.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiversion}")]    
    public class BooksController : Controller
    {
        #region Injeção de dependência
        private readonly ILogger<BooksController> _logger;
        private readonly IBookBusiness _bookBusiness;

        public BooksController(ILogger<BooksController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        #endregion

        #region EndPoints
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
        #endregion
    }
}
