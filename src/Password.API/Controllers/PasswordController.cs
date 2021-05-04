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

        [HttpGet("is-valid/{password}")]
        public IActionResult IsValid(string password)
        {
            bool isValid = _passwordValidator.IsValid(password);
            
            return Ok(isValid);
        }
    }
}
