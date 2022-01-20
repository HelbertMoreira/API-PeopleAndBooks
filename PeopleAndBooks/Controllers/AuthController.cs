using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleAndBooks.Business;
using PeopleAndBooks.DataConverter.VO;

namespace PeopleAndBooks.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiversion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILogin _login;

        public AuthController(ILogin login)
        {
            _login = login;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserSystemVO user)
        {
            if (user == null) return BadRequest("Usuário ou senha inválido");
            var token = _login.ValidateCredentials(user);
            if (token == null) return Unauthorized(); 
            return Ok(token);
        }
    }
}
