namespace WEGManagement.Domain.Repositories;

using WEGManagement.Domain.Models;

public interface IBuildingRepository
{
    Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Building building, CancellationToken cancellationToken = default);
    Task UpdateAsync(Building building, CancellationToken cancellationToken = default);
}

public interface IApartmentRepository
{
    Task<IReadOnlyList<Apartment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Apartment apartment, CancellationToken cancellationToken = default);
    Task UpdateAsync(Apartment apartment, CancellationToken cancellationToken = default);
}

public interface IOwnerRepository
{
    Task<IReadOnlyList<Owner>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Owner?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Owner owner, CancellationToken cancellationToken = default);
    Task UpdateAsync(Owner owner, CancellationToken cancellationToken = default);
}

public interface ICondoFeeRepository
{
    Task<CondoFee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(CondoFee condoFee, CancellationToken cancellationToken = default);
    Task UpdateAsync(CondoFee condoFee, CancellationToken cancellationToken = default);
}

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Payment payment, CancellationToken cancellationToken = default);
}

public interface IRepairOrderRepository
{
    Task<RepairOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(RepairOrder repairOrder, CancellationToken cancellationToken = default);
    Task UpdateAsync(RepairOrder repairOrder, CancellationToken cancellationToken = default);
}
