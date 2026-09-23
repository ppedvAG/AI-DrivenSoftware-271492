using Microsoft.EntityFrameworkCore;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;
using WEGManagement.Infrastructure.Persistance;

namespace WEGManagement.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly WegDbContext _context;


    public OwnerRepository(WegDbContext context)
    {
        _context = context;
    }

    public async Task<Owner?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Owners
            .Include(x => x.Apartments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        Owner owner,
        CancellationToken cancellationToken = default)
    {
        await _context.Owners.AddAsync(owner, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Owner owner,
        CancellationToken cancellationToken = default)
    {
        _context.Owners.Update(owner);
        await _context.SaveChangesAsync(cancellationToken);
    }


}
