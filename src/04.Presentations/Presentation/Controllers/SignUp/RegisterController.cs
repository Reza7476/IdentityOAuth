using Application.Services.Users.Contracts;
using Application.Services.Users.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers.SignUp
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {

            var user = new AddUserDto()
            {
                CreateDate = DateTime.Now,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Mobile = dto.Mobile,
                Password = dto.Password,
                UserName = dto.UserName,
            };

            await _userService.Register(user);
            return View();
        }
    }
}
