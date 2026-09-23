using WEGManagement.Domain.Enums;

namespace WEGManagement.UI.Mvc.Models;

public class ApartmentViewModel
{
    public Guid Id { get; set; }

    public string Address { get; set; } = string.Empty;

    public float Size { get; set; }

    public int Floor { get; set; }

    public ApartmentStatus Status { get; set; }

    public int NumberOfResidents { get; set; }

    public Guid BuildingId { get; set; }

    public string BuildingAddress { get; set; } = string.Empty;

    public Guid OwnerId { get; set; }

    public string OwnerName { get; set; } = string.Empty;


}

public class CreateApartmentViewModel
{
    public string Address { get; set; } = string.Empty;

    public float Size { get; set; }

    public int Floor { get; set; }

    public ApartmentStatus Status { get; set; }

    public int NumberOfResidents { get; set; }

    public Guid BuildingId { get; set; }

    public Guid OwnerId { get; set; }


}

public class ChangeApartmentOwnerViewModel
{
    public Guid ApartmentId { get; set; }

    public Guid OwnerId { get; set; }

}
