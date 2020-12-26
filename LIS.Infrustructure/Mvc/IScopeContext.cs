namespace LIS.Infrastructure.Mvc
{
    public interface IScopeContext
    {
        string UserId { get; }
        string UserFullName { get; }
        string Email { get; }
        string UserName { get; }
    }
}
