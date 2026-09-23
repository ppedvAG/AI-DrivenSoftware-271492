using Microsoft.AspNetCore.Mvc;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;

namespace WEGManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnerController : ControllerBase
{
    private readonly IOwnerRepository _repository;

    public OwnerController(IOwnerRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Owner>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var owner = await _repository.GetByIdAsync(id, cancellationToken);

        if (owner is null)
            return NotFound();

        return Ok(owner);
    }

    [HttpPost]
    public async Task<ActionResult<Owner>> Create(
        CreateOwnerRequest request,
        CancellationToken cancellationToken)
    {
        var owner = new Owner(
            request.Name,
            request.Contact,
            request.BankConnection);

        await _repository.AddAsync(owner, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = owner.Id },
            owner);
    }
}

public record CreateOwnerRequest(
    string Name,
    string Contact,
    string BankConnection);
