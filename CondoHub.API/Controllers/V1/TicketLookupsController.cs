using CondoHub.Domain.Dto.Ticket;
using CondoHub.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CondoHub.API.Controllers.V1;

/// <summary>Auxiliary read-only endpoints used to populate ticket dropdowns on the front-end.</summary>
[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketLookupsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketLookupsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet("types")]
    [ProducesResponseType(typeof(List<TicketTypeResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TicketTypeResponse>>> GetTypes(CancellationToken cancellationToken)
    {
        var result = await _ticketService.GetTypesAsync(cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("categories")]
    [ProducesResponseType(typeof(List<TicketCategoryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TicketCategoryResponse>>> GetCategories(
        [FromQuery] long? typeId,
        CancellationToken cancellationToken)
    {
        var result = await _ticketService.GetCategoriesAsync(typeId, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
