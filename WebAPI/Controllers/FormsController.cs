using Application.Commands;
using Application.Queries;
using Domain.Forms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFormCommand command)
        {
            try
            {
                var formId = await _mediator.Send(command);
                return Ok(new { Id = formId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Form>> GetByIdAsync(int id)
        {
            var query = new GetFormByIdQuery(id);
            var form = await _mediator.Send(query);
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }
    }
}
