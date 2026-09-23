using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;
using WEGManagement.UI.Mvc.Models;

namespace WEGManagement.UI.Mvc.Controllers;

public class ApartmentController : Controller
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

    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var apartments = await _apartmentRepository.GetAllAsync(
        cancellationToken);

        var model = apartments
            .Select(Map)
            .ToList();

        return View(model);
    }

    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        var apartment =
            await _apartmentRepository.GetByIdAsync(
                id,
                cancellationToken);

        if (apartment is null)
            return NotFound();

        return View(Map(apartment));
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken)
    {
        await LoadSelections(cancellationToken);

        return View(new CreateApartmentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateApartmentViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadSelections(cancellationToken);
            return View(model);
        }

        var building =
            await _buildingRepository.GetByIdAsync(
                model.BuildingId,
                cancellationToken);

        if (building is null)
        {
            ModelState.AddModelError(
                nameof(model.BuildingId),
                "Das Gebäude wurde nicht gefunden.");

            await LoadSelections(cancellationToken);
            return View(model);
        }

        var owner =
            await _ownerRepository.GetByIdAsync(
                model.OwnerId,
                cancellationToken);

        if (owner is null)
        {
            ModelState.AddModelError(
                nameof(model.OwnerId),
                "Der Eigentümer wurde nicht gefunden.");

            await LoadSelections(cancellationToken);
            return View(model);
        }

        var apartment = new Apartment(
            model.Address,
            model.Size,
            model.Floor,
            model.Status,
            model.NumberOfResidents,
            building,
            owner);

        await _apartmentRepository.AddAsync(
            apartment,
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id = apartment.Id });
    }

    [HttpGet]
    public async Task<IActionResult> ChangeOwner(
        Guid id,
        CancellationToken cancellationToken)
    {
        var apartment =
            await _apartmentRepository.GetByIdAsync(
                id,
                cancellationToken);

        if (apartment is null)
            return NotFound();

        var model = new ChangeApartmentOwnerViewModel
        {
            ApartmentId = apartment.Id,
            OwnerId = apartment.OwnerId
        };

        await LoadOwners(cancellationToken);

        ViewBag.ApartmentAddress = apartment.Address;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeOwner(
        ChangeApartmentOwnerViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadOwners(cancellationToken);
            return View(model);
        }

        var apartment =
            await _apartmentRepository.GetByIdAsync(
                model.ApartmentId,
                cancellationToken);

        if (apartment is null)
            return NotFound();

        var owner =
            await _ownerRepository.GetByIdAsync(
                model.OwnerId,
                cancellationToken);

        if (owner is null)
        {
            ModelState.AddModelError(
                nameof(model.OwnerId),
                "Der Eigentümer wurde nicht gefunden.");

            await LoadOwners(cancellationToken);
            return View(model);
        }

        apartment.ChangeOwner(owner);

        await _apartmentRepository.UpdateAsync(
            apartment,
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id = apartment.Id });
    }

    private async Task LoadSelections(
        CancellationToken cancellationToken)
    {
        await LoadOwners(cancellationToken);

        // Das aktuelle Repository besitzt noch kein GetAllAsync
        // für Gebäude. Daher kann die Gebäudeliste erst verwendet
        // werden, wenn IBuildingRepository entsprechend erweitert wird.

        ViewBag.Buildings = new SelectList(
            Enumerable.Empty<SelectListItem>());
    }

    private async Task LoadOwners(
        CancellationToken cancellationToken)
    {
        var owners = await _ownerRepository.GetAllAsync(cancellationToken);
        ViewBag.Owners = new SelectList(owners, nameof(Owner.Id), nameof(Owner.Name));
    }

    private static ApartmentViewModel Map(
        Apartment apartment)
    {
        return new ApartmentViewModel
        {
            Id = apartment.Id,
            Address = apartment.Address,
            Size = apartment.Size,
            Floor = apartment.Floor,
            Status = apartment.Status,
            NumberOfResidents = apartment.NumberOfResidents,
            BuildingId = apartment.BuildingId,
            BuildingAddress = apartment.Building?.Address ?? string.Empty,
            OwnerId = apartment.OwnerId,
            OwnerName = apartment.Owner?.Name ?? string.Empty
        };
    }


}
