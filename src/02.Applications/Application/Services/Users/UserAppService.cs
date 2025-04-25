using Application.Services.Users.Contracts;
using Application.Services.Users.Contracts.Dtos;
using Common.Interfaces;
using Domain.Entities.Users;

namespace Application.Services.Users;

public class UserAppService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserAppService(IUserRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task CheckPassword(string password, string userName)
    {
        if(!await _repository.IsExistUserByPassword(userName, password))
        {
            throw new Exception("password is wrong");
        }

    }

    public async Task IsExistByUserName(string userName)
    {

        if(!await _repository.IsExistByUserName(userName))
        {
            throw new Exception("User Name is wrong");
        }

    }

    public async Task Register(AddUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.UserName))
        {
            throw new Exception("User name must be filled");
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new Exception("Password must be filled");
        }

        if (string.IsNullOrWhiteSpace(dto.FirstName))
        {
            throw new Exception("First name must be filled");
        }

        if (string.IsNullOrWhiteSpace(dto.LastName))
        {
            throw new Exception("Last name must be filled");
        }

        if (string.IsNullOrWhiteSpace(dto.Mobile))
        {
            throw new Exception("Mobile must be filled");
        }

        if(await _repository.IsExistByUserName(dto.UserName))
        {
            throw new Exception("User name is exist");
        }

        if (await _repository.IsExistByMobile(dto.Mobile))
        {
            throw new Exception("User name is exist");
        }

        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Mobile = dto.Mobile,
            UserName = dto.UserName,
            Password = dto.Password,
            Email = dto.Email,
            CreateDate = dto.CreateDate,
        };

        await _repository.Add(user);
        await _unitOfWork.Complete();
    }
}
