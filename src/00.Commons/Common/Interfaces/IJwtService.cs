namespace Common.Interfaces;

public interface IJwtService: IService
{
    string GenerateToken(string userName, string role);
}
