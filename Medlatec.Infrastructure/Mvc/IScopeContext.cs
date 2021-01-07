using Medlatec.Infrastructure.Models;

namespace Medlatec.Infrastructure.Mvc
{
    public interface IScopeContext
    {
        BriefUser CurrentUser { get; }
    }
}
