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

    public async Task Register(AddUserDto dto)
    {
        var user = new User()
        {
            CreateDate = dto.CreateDate,
            Email = dto.Email,
            FirstName = dto.FirstName,
            Id =  Guid.NewGuid().ToString(),
            LastName = dto.LastName,
            Mobile = dto.Mobile,
            Password = dto.Password,
            UserName = dto.UserName,
        };

        await _repository.Add(user);
        await _unitOfWork.Complete();
    }
}
