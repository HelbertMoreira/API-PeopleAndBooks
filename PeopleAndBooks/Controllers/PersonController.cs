using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAndBooks.Business;
using PeopleAndBooks.DataConverter.Converter.VO;
using System.Collections.Generic;

namespace PeopleAndBooks.Controllers
{
    [ApiVersion("1")] // -- VERSIONAMENTO DA API -- Adiciona na rota o número da versão da controller. 
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiversion}")] // Modificando a rota desta controller
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
        
        // Após a implementação do método de busca paginada, este código já não faz mais sentido, pois fará praticamente
        // a msma coisa do que faz uma busca paginada.
        
        /*
        [HttpGet]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))] // Anotation para customização do swagger
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }
        */

        [HttpGet("{sortDirection}/{pageSize}/{currentPage}")]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))] // Anotation para customização do swagger
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(
            [FromQuery] string name,
            string sortDirection,
            int pageSize,
            int currentPage)
        {
            return Ok(_personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, currentPage));
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpGet("findPersonByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindByName([FromQuery] string nome, [FromQuery] string sobrenome)
        {
            var person = _personBusiness.FindByName(nome, sobrenome);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpPatch("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult DisableOrEnable(int id)
        {
            var person = _personBusiness.DisableOrEnable(id);
            return Ok(person);
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType((204))]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
        #endregion
    }
}
