
using Application.Services.Users.Contracts;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Presentation.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers.SignUp;

public class LoginController : Controller
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public LoginController(
        IUserService userService,
        IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        await _userService.IsExistByUserName(dto.UserName);
        await _userService.CheckPassword(dto.Password, dto.UserName);
        var token = _jwtService.GenerateToken(dto.UserName, "User");

        Response.Cookies.Append("jwtToken", token, new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(30)
        });


        return Ok(new { success = true, message = "ورود موفقیت‌آمیز بود!" });

    }

}
