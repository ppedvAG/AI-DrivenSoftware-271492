using Microsoft.EntityFrameworkCore;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;
using WEGManagement.Infrastructure.Persistance;

namespace WEGManagement.Infrastructure.Repositories;

public class BuildingRepository : IBuildingRepository
{
    private readonly WegDbContext _context;


    public BuildingRepository(WegDbContext context)
    {
        _context = context;
    }

    public async Task<Building?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Buildings
            .Include(x => x.Apartments) // Wir laden die Apartments explizit nach. Kann jedoch bei serialisieren zu Problemen fuehren. Deshalb ReferenceHandler.IgnoreCycles setzen
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        Building building,
        CancellationToken cancellationToken = default)
    {
        await _context.Buildings.AddAsync(building, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Building building,
        CancellationToken cancellationToken = default)
    {
        _context.Buildings.Update(building);
        await _context.SaveChangesAsync(cancellationToken);
    }


}
