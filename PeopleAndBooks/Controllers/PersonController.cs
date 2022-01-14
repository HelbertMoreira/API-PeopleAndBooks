using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAndBooks.Model;
using PeopleAndBooks.Services;

namespace PeopleAndBooks.Controllers
{
    [ApiVersion("1")] // Adiciona na rota o número da versão da controller. 
    [ApiController]
    [Route("api/[controller]/v{version:apiversion}")] // Modificando a rota desta controller // Versionamento por controller
    public class PersonController : ControllerBase
    {

        #region Injeção de dependência
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        #endregion

        #region EndPoints
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
        #endregion
    }
}
