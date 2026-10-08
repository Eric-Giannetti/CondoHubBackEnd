using CondoHub.API.Extensions;
using CondoHub.Domain.Dto.Ticket;
using CondoHub.Domain.Util;
using CondoHub.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CondoHub.API.Controllers.V1;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private const long MaxAttachmentSizeBytes = 10 * 1024 * 1024;

    private readonly ITicketService _ticketService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TicketsController(ITicketService ticketService, IWebHostEnvironment webHostEnvironment)
    {
        _ticketService = ticketService;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TicketResponse>> CreateTicket(
        [FromBody] CreateTicketRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryGetIdentity(out var condominiumId, out var userId, out var unauthorized))
            return unauthorized;

        var result = await _ticketService.CreateAsync(request, condominiumId, userId, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetTicketById), new { id = result.Value.Id }, result.Value);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<TicketListItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTickets(
        [FromQuery] TicketQueryParameters query,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetCondominiumId(out var condominiumId))
            return Unauthorized();

        var result = await _ticketService.GetTicketsAsync(query, condominiumId, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TicketResponse>> GetTicketById(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetCondominiumId(out var condominiumId))
            return Unauthorized();

        var result = await _ticketService.GetByIdAsync(id, condominiumId, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPatch("{id:long}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateTicketStatus(
        [FromRoute] long id,
        [FromBody] UpdateTicketStatusRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryGetIdentity(out var condominiumId, out var userId, out var unauthorized))
            return unauthorized;

        var result = await _ticketService.UpdateStatusAsync(id, condominiumId, request, userId, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateTicket(
        [FromRoute] long id,
        [FromBody] UpdateTicketRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryGetIdentity(out var condominiumId, out var userId, out var unauthorized))
            return unauthorized;

        var result = await _ticketService.UpdateAsync(id, condominiumId, request, userId, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPost("{id:long}/comments")]
    [ProducesResponseType(typeof(TicketCommentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TicketCommentResponse>> AddComment(
        [FromRoute] long id,
        [FromBody] CreateTicketCommentRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryGetIdentity(out var condominiumId, out var userId, out var unauthorized))
            return unauthorized;

        var result = await _ticketService.AddCommentAsync(id, condominiumId, userId, request, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetComments), new { id }, result.Value);
    }

    [HttpGet("{id:long}/comments")]
    [ProducesResponseType(typeof(List<TicketCommentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetComments([FromRoute] long id, CancellationToken cancellationToken)
    {
        if (!User.TryGetCondominiumId(out var condominiumId))
            return Unauthorized();

        var result = await _ticketService.GetCommentsAsync(id, condominiumId, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("{id:long}/attachments")]
    [ProducesResponseType(typeof(TicketAttachmentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [RequestSizeLimit(MaxAttachmentSizeBytes)]
    public async Task<ActionResult<TicketAttachmentResponse>> UploadAttachment(
        [FromRoute] long id,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (!TryGetIdentity(out var condominiumId, out var userId, out var unauthorized))
            return unauthorized;

        if (file is null || file.Length == 0)
            return BadRequest("A file is required.");

        if (file.Length > MaxAttachmentSizeBytes)
            return BadRequest("File exceeds the maximum allowed size of 10 MB.");

        var safeFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var ticketFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "ticket-attachments", id.ToString());
        Directory.CreateDirectory(ticketFolder);
        var destinationPath = Path.Combine(ticketFolder, safeFileName);

        await using (var stream = new FileStream(destinationPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        var result = await _ticketService.AddAttachmentAsync(
            id,
            condominiumId,
            userId,
            Path.GetFileName(file.FileName),
            file.ContentType,
            file.Length,
            destinationPath,
            cancellationToken);

        if (!result.IsSuccess)
        {
            System.IO.File.Delete(destinationPath);
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetTicketById), new { id }, result.Value);
    }

    private bool TryGetIdentity(out long condominiumId, out long userId, out ActionResult unauthorized)
    {
        unauthorized = Unauthorized();
        condominiumId = 0;
        userId = 0;

        if (!User.TryGetCondominiumId(out condominiumId) || !User.TryGetUserId(out userId))
            return false;

        return true;
    }
}
