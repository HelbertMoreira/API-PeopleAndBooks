using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Token não foi informado.");
            var token = _login.ValidateCredentials(tokenVO);
            if (token == null) return BadRequest("Token informado não existe na base de dados.");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {            
            var userName = User.Identity.Name;
            var result = _login.RevokeToken(userName);
            if (!result) return BadRequest("Token inválido para o usuário informado.");
            return NoContent();
        }
    }
}
