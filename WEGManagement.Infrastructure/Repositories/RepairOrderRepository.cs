using Microsoft.EntityFrameworkCore;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;
using WEGManagement.Infrastructure.Persistance;

namespace WEGManagement.Infrastructure.Repositories;

public class RepairOrderRepository : IRepairOrderRepository
{
    private readonly WegDbContext _context;


    public RepairOrderRepository(WegDbContext context)
    {
        _context = context;
    }

    public async Task<RepairOrder?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.RepairOrders
            .Include(x => x.Building)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        RepairOrder repairOrder,
        CancellationToken cancellationToken = default)
    {
        await _context.RepairOrders.AddAsync(
            repairOrder,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        RepairOrder repairOrder,
        CancellationToken cancellationToken = default)
    {
        _context.RepairOrders.Update(repairOrder);
        await _context.SaveChangesAsync(cancellationToken);
    }

}
