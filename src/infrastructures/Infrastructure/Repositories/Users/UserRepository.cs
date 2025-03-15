using Application.Services.Users.Contracts;
using Domain.Entities.Users;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    
    public UserRepository(EFDataContext context)
    {
        _users = context.Set<User>();
    }

    public async Task Add(User user)
    {
        await _users.AddAsync(user);
    }
}
