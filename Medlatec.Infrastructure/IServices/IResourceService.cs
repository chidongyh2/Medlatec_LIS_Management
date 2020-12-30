using System;
using System.Collections.Generic;
using System.Text;

namespace Medlatec.Infrastructure.IServices
{
    public interface IResourceService<T> where T : class
    {
        string GetString(string key);
        string GetString(string key, params object[] arguments);
    }
}
