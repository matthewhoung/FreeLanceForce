using Application.Forms.Commands;
using Application.Forms.Queries;
using Domain.Forms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/forms")]
    public class FormsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateFormCommand command)
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
        public async Task<ActionResult<Form>> GetByIdAsync([FromBody]int id)
        {
            var query = new GetFormByIdQuery(id);
            var form = await _mediator.Send(query);
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Form>>> GetAllAsync()
        {
            var query = new GetAllFormsQuery();
            var forms = await _mediator.Send(query);
            return Ok(forms);
        }
    }
}
