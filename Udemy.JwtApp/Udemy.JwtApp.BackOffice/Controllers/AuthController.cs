using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries;
using Udemy.JwtApp.BackOffice.Infrastructure.Tools;

namespace Udemy.JwtApp.BackOffice.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //1-User Register => member rolü ile beraber register edilecek
        //2- username ve password db de varsa ve kullanıcının girdiği usernaem ve password dbdeki kayıt ile eşleşiyorsa token üreteceğim.
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //doğrudan metotumun adıyla istek yapıcam
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
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


    }
}
