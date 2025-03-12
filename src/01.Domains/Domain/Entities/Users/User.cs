namespace Domain.Entities.Users;

public class User
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Mobile { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public DateTime CreateDate { get; set; }
    public string? Email { get; set; }
}
