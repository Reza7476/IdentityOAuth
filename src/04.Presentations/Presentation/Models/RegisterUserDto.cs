namespace Presentation.Models;

public class RegisterUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Mobile { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public string? Email { get; set; }
}
