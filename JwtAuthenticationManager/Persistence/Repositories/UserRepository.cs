using JwtAuthenticationManager.Domain.Models;
using JwtAuthenticationManager.Domain.Repositories;
using JwtAuthenticationManager.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationManager.Persistence.Repositories;

public class UserRepository(SecurityDbContext context): IUserRepository
{
    private readonly SecurityDbContext _context = context;

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User> FindByIdAsync(long id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _context.Users
            .SingleOrDefaultAsync(p => p.Email == email);
    }
    
    public bool ExistsByEmail(string email)
    {
        return _context.Users.Any(p => p.Email == email);
    }

    public User FindById(long id)
    {
        return _context.Users.Find(id);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
}