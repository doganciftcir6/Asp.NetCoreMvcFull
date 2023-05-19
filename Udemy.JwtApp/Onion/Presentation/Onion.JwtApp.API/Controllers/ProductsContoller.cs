using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.JwtApp.Application.Features.CQRS.Commands;
using Onion.JwtApp.Application.Features.CQRS.Queries;

namespace Onion.JwtApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsContoller : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsContoller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetProductsQueryRequest());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductQueryRequest(id));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommentRequest request)
        {
            var result = await _mediator.Send(request);
            return Created("", result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
