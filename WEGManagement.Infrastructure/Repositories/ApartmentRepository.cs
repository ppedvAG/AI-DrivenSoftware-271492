using Microsoft.EntityFrameworkCore;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;
using WEGManagement.Infrastructure.Persistance;

namespace WEGManagement.Infrastructure.Repositories;

public class ApartmentRepository : IApartmentRepository
{
    private readonly WegDbContext _context;

    public ApartmentRepository(WegDbContext context)
    {
        _context = context;
    }

    public async Task<Apartment?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Apartments
            .Include(x => x.Owner)
            .Include(x => x.Building)
            .Include(x => x.CondoFees)
                .ThenInclude(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        Apartment apartment,
        CancellationToken cancellationToken = default)
    {
        await _context.Apartments.AddAsync(apartment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Apartment apartment,
        CancellationToken cancellationToken = default)
    {
        _context.Apartments.Update(apartment);
        await _context.SaveChangesAsync(cancellationToken);
    }


}
