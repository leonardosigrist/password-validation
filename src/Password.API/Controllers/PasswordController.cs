using Microsoft.AspNetCore.Mvc;
using Password.Services;

namespace PasswordValidation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordValidator _passwordValidator;

        public PasswordController(IPasswordValidator passwordValidator)
        {
            _passwordValidator = passwordValidator;
        }

        /// <summary>
        /// Valida se a senha atende os requisitos mínimos de segurança
        /// </summary>
        /// <param name="password">Senha a ser validada</param>
        /// <response code="200">Retorna se a senha é válida</response>
        [HttpGet("is-valid/{password}")]
        public IActionResult IsValid(string password)
        {
            bool isValid = _passwordValidator.IsValid(password);
            
            return Ok(isValid);
        }
    }
}
