using Microsoft.AspNetCore.Mvc;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;

namespace WEGManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingController : ControllerBase
{
    private readonly IBuildingRepository _repository;

    public BuildingController(IBuildingRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Building>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var building = await _repository.GetByIdAsync(id, cancellationToken);

        if (building is null)
            return NotFound();

        return Ok(building);
    }

    [HttpPost]
    public async Task<ActionResult<Building>> Create(
        CreateBuildingRequest request,
        CancellationToken cancellationToken)
    {
        var building = new Building(request.Address);

        await _repository.AddAsync(building, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = building.Id },
            building);
    }
}

public record CreateBuildingRequest(string Address);

public record UpdateBuildingRequest(string Address);