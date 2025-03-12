namespace Common.Interfaces;

public interface IUnitOfWork
{
    Task Begin();
    Task Commit();
    Task Complete();
    Task RoleBack();
}
