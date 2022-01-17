using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAndBooks.Business;
using PeopleAndBooks.DataConverter.Converter.VO;
using PeopleAndBooks.Model;

namespace PeopleAndBooks.Controllers
{
    [ApiVersion("1")] // Adiciona na rota o número da versão da controller. 
    [ApiController]
    [Route("api/[controller]/v{version:apiversion}")] // Modificando a rota desta controller // Versionamento por controller
    public class PersonController : ControllerBase
    {

        #region Injeção de dependência
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;   
        }

        #endregion

        #region EndPoints
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
        #endregion
    }
}
