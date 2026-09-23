using Microsoft.AspNetCore.Mvc;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;

namespace WEGManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApartmentController : ControllerBase
{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IBuildingRepository _buildingRepository;
    private readonly IOwnerRepository _ownerRepository;

    public ApartmentController(
        IApartmentRepository apartmentRepository,
        IBuildingRepository buildingRepository,
        IOwnerRepository ownerRepository)
    {
        _apartmentRepository = apartmentRepository;
        _buildingRepository = buildingRepository;
        _ownerRepository = ownerRepository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Apartment>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var apartment =
            await _apartmentRepository.GetByIdAsync(id, cancellationToken);

        if (apartment is null)
            return NotFound();

        return Ok(apartment);
    }

    [HttpPost]
    public async Task<ActionResult<Apartment>> Create(
        CreateApartmentRequest request,
        CancellationToken cancellationToken)
    {
        var building =
            await _buildingRepository.GetByIdAsync(
                request.BuildingId,
                cancellationToken);

        if (building is null)
            return BadRequest("Building not found.");

        var owner =
            await _ownerRepository.GetByIdAsync(
                request.OwnerId,
                cancellationToken);

        if (owner is null)
            return BadRequest("Owner not found.");

        var apartment = new Apartment(
            request.Address,
            request.Size,
            request.Floor,
            request.Status,
            request.NumberOfResidents,
            building,
            owner);

        await _apartmentRepository.AddAsync(
            apartment,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = apartment.Id },
            apartment);
    }

    [HttpPut("{id:guid}/owner")]
    public async Task<IActionResult> ChangeOwner(
        Guid id,
        ChangeOwnerRequest request,
        CancellationToken cancellationToken)
    {
        var apartment =
            await _apartmentRepository.GetByIdAsync(
                id,
                cancellationToken);

        if (apartment is null)
            return NotFound();

        var owner =
            await _ownerRepository.GetByIdAsync(
                request.OwnerId,
                cancellationToken);

        if (owner is null)
            return BadRequest("Owner not found.");

        apartment.ChangeOwner(owner);

        await _apartmentRepository.UpdateAsync(
            apartment,
            cancellationToken);

        return NoContent();
    }
}

public record CreateApartmentRequest(
    string Address,
    float Size,
    int Floor,
    ApartmentStatus Status,
    int NumberOfResidents,
    Guid BuildingId,
    Guid OwnerId);

public record ChangeOwnerRequest(Guid OwnerId);
