using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.JwtApp.Application.Features.CQRS.Commands;
using Onion.JwtApp.Application.Features.CQRS.Queries;
using Onion.JwtApp.Application.Tools;

namespace Onion.JwtApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //günün sonunda biz bir token üreticez ondan post.
        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(CheckUserQueryRequest request)
        {
            //bu bize geriye bir dto dönüyor.
            var userDto = await _mediator.Send(request);
            //kontrol edelim
            if (userDto.IsExist == true)
            {
                //o zaman sıkıntı yok check işlemi başarılı token'ı üretelim.
                //token oluşturma işlemini JwtTokenGenerator classı içinde yapıp burda kullanıcaz.
                var token = JwtTokenGenerator.GenerateToken(userDto);

                return Created("", token);
            }
            //check işlemi başarılı değildir
            return BadRequest("Username veya password yanlıştır.");
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Created("", result);
        }
    }
}
