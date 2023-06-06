using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serdiuk.ToDoList.Application.Dtos.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Serdiuk.ToDoList.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager,
            ILogger<AccountController> logger,
            SignInManager<IdentityUser> signinManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _signinManager = signinManager;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel userLogin)
        {
            if (!ModelState.IsValid || userLogin == null)
            {
                _logger.LogError($"Invalid login object provided.");
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            if (user == null)
            {
                ModelState.AddModelError("email", "Login or password error");
                return BadRequest();
            }
            var signInResult = await _signinManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, true, false);
            if (!signInResult.Succeeded)
            {
                _logger.LogError($"Unable to sign-in findUser {userLogin.Email}.");
                return Unauthorized();
            }
            _logger.LogInformation($"User {userLogin.Email} logged in successfully.");
            var claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, userLogin.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JWT:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Authentication:JWT:Issuer"],
                _configuration["Authentication:JWT:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { AccessToken = tokenString });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel userRegister)
        {
            if (!ModelState.IsValid || userRegister == null)
            {
                _logger.LogError($"Invalid login object provided.");
                return BadRequest();
            }
            try
            {
                var findUser = await _userManager.FindByEmailAsync(userRegister.Email);
                if (findUser != null)
                {
                    ModelState.AddModelError("", "Username is already used");
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            };

            var user = new IdentityUser
            {
                Email = userRegister.Email,
                UserName = userRegister.Email,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, userRegister.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("{Email} created", user.Email);
                     await _signinManager.SignInAsync(user, false);
                    _logger.LogInformation("{Email} login", user.Email);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, userRegister.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JWT:SecurityKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Authentication:JWT:Issuer"],
                        audience: _configuration["Authentication:JWT:Audience"],
                        claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: creds
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new { AccessToken = tokenString });
                }
                else
                {
                    _logger.LogError("Create account errors: {Errors}",result.Errors.Select(s => s.Description));
                    return BadRequest(string.Join(", ", result.Errors.Select(s => s.Description)));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}

