using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Password.API.Services;

namespace PasswordValidation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly ILogger<PasswordController> _logger;
        private readonly IPasswordValidator _passwordValidator;

        public PasswordController(ILogger<PasswordController> logger, IPasswordValidator passwordValidator)
        {
            _logger = logger;
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
