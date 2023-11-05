using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.Shared.Persistence.Contexts;

namespace BackendXComponent.Shared.Persistence.Repositories;

public class UnitOfWork: ImplUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
    
}