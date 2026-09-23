using System.ComponentModel.DataAnnotations.Schema;

namespace WEGManagement.Domain.Models;

public class Owner
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Contact { get; private set; } = string.Empty;
    public string BankConnection { get; private set; } = string.Empty;

    [NotMapped]
    public ICollection<Apartment> Apartments { get; private set; } = new List<Apartment>();

    private Owner() { }

    public Owner(string name, string contact, string bankConnection)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        Id = Guid.NewGuid();
        Name = name;
        Contact = contact;
        BankConnection = bankConnection;
    }
}
