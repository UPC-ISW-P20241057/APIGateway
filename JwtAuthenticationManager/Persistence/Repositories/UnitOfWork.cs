using JwtAuthenticationManager.Domain.Repositories;
using JwtAuthenticationManager.Persistence.Context;

namespace JwtAuthenticationManager.Persistence.Repositories;

public class UnitOfWork(SecurityDbContext context): IUnitOfWork
{
    private readonly SecurityDbContext _context = context;

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}