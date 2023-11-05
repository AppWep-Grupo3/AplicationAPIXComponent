using BackendXComponent.Shared.Persistence.Contexts;

namespace BackendXComponent.Shared.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;
    
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}