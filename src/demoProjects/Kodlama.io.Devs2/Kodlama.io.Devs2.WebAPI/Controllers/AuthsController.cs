using Core.Security.Dtos;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Features.Auths.Commands.Login;
using Kodlama.io.Devs2.Application.Features.Auths.Commands.Register;
using Kodlama.io.Devs2.Application.Features.Auths.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };

            RegisteredDto result = await Mediator.Send(registerCommand); // Register olan detayı çekiyoruz
            SetRefreshTokenToCookie(result.RefreshToken);

            return Created("", result.AccessToken);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = GetIpAddress() };
            LoggedDto result = await Mediator.Send(loginCommand);

            if (result.RefreshToken is not null) SetRefreshTokenToCookie(result.RefreshToken);

            return Ok(result.CreateResponseDto());
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            // Oluşan RefreshToken'ı aynı zamanda Cookie ye eklemek gerekir

            CookieOptions cookieOptions = new()
            {
                HttpOnly = true, // Http isteklerinde bu işi yap demek
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
