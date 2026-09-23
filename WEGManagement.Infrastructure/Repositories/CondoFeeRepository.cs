using Microsoft.EntityFrameworkCore;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;
using WEGManagement.Infrastructure.Persistance;

namespace WEGManagement.Infrastructure.Repositories;

public class CondoFeeRepository : ICondoFeeRepository
{
    private readonly WegDbContext _context;


    public CondoFeeRepository(WegDbContext context)
    {
        _context = context;
    }

    public async Task<CondoFee?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.CondoFees
            .Include(x => x.Apartment)
                .ThenInclude(x => x.Owner)
            .Include(x => x.Apartment)
                .ThenInclude(x => x.Building)
            .Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        CondoFee condoFee,
        CancellationToken cancellationToken = default)
    {
        await _context.CondoFees.AddAsync(condoFee, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        CondoFee condoFee,
        CancellationToken cancellationToken = default)
    {
        _context.CondoFees.Update(condoFee);
        await _context.SaveChangesAsync(cancellationToken);
    }


}
