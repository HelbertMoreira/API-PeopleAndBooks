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
