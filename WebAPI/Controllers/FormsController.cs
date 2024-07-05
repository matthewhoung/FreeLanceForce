using Application.Forms.Commands;
using Application.Forms.DTOs;
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
        public async Task<IActionResult> Create([FromBody]CreateOrderFormCommand command)
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

        [HttpPost("signature/member")]
        public async Task<IActionResult> CreateSignatureMembers([FromBody] List<CreateSignatureMemberDTO> signatureMembers)
        {
            var command = new CreateSignatureMemberCommand(signatureMembers);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("lineitem")]
        public async Task<IActionResult> CreateLineItems([FromBody] List<CreateLineItemDTO> lineItems)
        {
            var command = new CreateLineItemCommand(lineItems);
            await _mediator.Send(command);
            return Ok();
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

        [HttpGet("{formId}/signature")]
        public async Task<IActionResult> GetSignatureByFormId(int formId)
        {
            var query = new GetSignatureByIdQuery(formId);
            var signatures = await _mediator.Send(query);

            if (signatures == null)
            {
                return NotFound(new { Message = $"Signatures for form with Id {formId} not found." });
            }

            return Ok(signatures);
        }
    }
}
