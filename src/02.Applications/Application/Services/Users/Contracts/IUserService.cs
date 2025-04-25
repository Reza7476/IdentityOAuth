using Application.Services.Users.Contracts.Dtos;
using Common.Interfaces;

namespace Application.Services.Users.Contracts;

public interface IUserService : IService
{
    Task CheckPassword(string password, string userName);
    Task IsExistByUserName(string userName);
    Task Register(AddUserDto dto);
}
