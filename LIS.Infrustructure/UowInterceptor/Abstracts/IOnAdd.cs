using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Infrastructure.UowInterceptor.Abstracts
{
    public interface IOnAdd<T> where T : class
    {
        Task OnAdd(T entity);
    }
}
