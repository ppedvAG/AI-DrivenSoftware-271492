using Microsoft.AspNetCore.Mvc;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;

namespace WEGManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepairOrderController : ControllerBase
{
    private readonly IRepairOrderRepository _repository;
    private readonly IBuildingRepository _buildingRepository;

    public RepairOrderController(
        IRepairOrderRepository repository,
        IBuildingRepository buildingRepository)
    {
        _repository = repository;
        _buildingRepository = buildingRepository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RepairOrder>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var repairOrder =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (repairOrder is null)
            return NotFound();

        return Ok(repairOrder);
    }

    [HttpPost]
    public async Task<ActionResult<RepairOrder>> Create(
        CreateRepairOrderRequest request,
        CancellationToken cancellationToken)
    {
        var building =
            await _buildingRepository.GetByIdAsync(
                request.BuildingId,
                cancellationToken);

        if (building is null)
            return BadRequest("Building not found.");

        var repairOrder = new RepairOrder(
            request.Description,
            request.Priority,
            request.Cost,
            request.Date,
            request.Craftsman,
            building);

        await _repository.AddAsync(
            repairOrder,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = repairOrder.Id },
            repairOrder);
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> Start(
        Guid id,
        CancellationToken cancellationToken)
    {
        var repairOrder =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (repairOrder is null)
            return NotFound();

        try
        {
            repairOrder.Start();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }

        await _repository.UpdateAsync(
            repairOrder,
            cancellationToken);

        return Ok(repairOrder);
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var repairOrder =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (repairOrder is null)
            return NotFound();

        repairOrder.Complete();

        await _repository.UpdateAsync(
            repairOrder,
            cancellationToken);

        return Ok(repairOrder);
    }
}
public record CreateRepairOrderRequest(
    string Description,
    RepairOrderPriority Priority,
    decimal Cost,
    DateTime Date,
    string Craftsman,
    Guid BuildingId);