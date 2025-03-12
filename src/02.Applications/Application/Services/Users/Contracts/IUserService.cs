using Application.Services.Users.Contracts.Dtos;
using Common.Interfaces;

namespace Application.Services.Users.Contracts;

public interface IUserService : IService
{
    Task Register(AddUserDto dto);
}
