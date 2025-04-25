using Common.Interfaces;
using Domain.Entities.Users;

namespace Application.Services.Users.Contracts;

public interface IUserRepository : IScope
{
    Task Add(User user);
    Task<bool> IsExistByMobile(string mobile);
    Task<bool> IsExistByUserName(string userName);
    Task<bool> IsExistUserByPassword(string userName, string password);
}
