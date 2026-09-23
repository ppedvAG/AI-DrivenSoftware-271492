using Microsoft.AspNetCore.Mvc;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;

namespace WEGManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CondoFeeController : ControllerBase
{
    private readonly ICondoFeeRepository _repository;
    private readonly IApartmentRepository _apartmentRepository;

    public CondoFeeController(
        ICondoFeeRepository repository,
        IApartmentRepository apartmentRepository)
    {
        _repository = repository;
        _apartmentRepository = apartmentRepository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CondoFee>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var condoFee =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (condoFee is null)
            return NotFound();

        return Ok(condoFee);
    }

    [HttpPost]
    public async Task<ActionResult<CondoFee>> Create(
        CreateCondoFeeRequest request,
        CancellationToken cancellationToken)
    {
        var apartment =
            await _apartmentRepository.GetByIdAsync(
                request.ApartmentId,
                cancellationToken);

        if (apartment is null)
            return BadRequest("Apartment not found.");

        var condoFee = new CondoFee(
            request.Amount,
            request.DueDate,
            apartment);

        await _repository.AddAsync(
            condoFee,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = condoFee.Id },
            condoFee);
    }

    [HttpPost("{id:guid}/reminder")]
    public async Task<IActionResult> CreateReminder(
        Guid id,
        CancellationToken cancellationToken)
    {
        var condoFee =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (condoFee is null)
            return NotFound();

        condoFee.CreateReminder();

        await _repository.UpdateAsync(
            condoFee,
            cancellationToken);

        return Ok(condoFee);
    }
}

public record CreateCondoFeeRequest(
    decimal Amount,
    DateTime DueDate,
    Guid ApartmentId);