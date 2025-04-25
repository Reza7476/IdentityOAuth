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

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _users.AnyAsync(_ => _.UserName == userName);
    }

    public async Task<bool> IsExistUserByPassword(string userName, string password)
    {
        return await _users.AnyAsync(_=>_.Password==password && _.UserName==userName);
    }
}
