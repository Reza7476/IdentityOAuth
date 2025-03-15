using Common.Interfaces;
using Domain.Entities.Users;

namespace Application.Services.Users.Contracts;

public interface IUserRepository : IScope
{
    Task Add(User user);
}
