using Application.Forms.Commands;
using Application.Forms.Queries;
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

        [HttpPost("acceptance")]
        public async Task<IActionResult> CreateAcceptanceForm([FromBody] CreateAcceptanceFormCommand command)
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

        [HttpGet("{formId}")]
        public async Task<IActionResult> GetFormDetailsByFormId(int formId)
        {
            var query = new GetFormByIdQuery(formId);
            var orderFormDetails = await _mediator.Send(query);

            if (orderFormDetails == null)
            {
                return NotFound(new { Message = $"Order form with Id {formId} not found." });
            }

            return Ok(orderFormDetails);
        }
    }
}
