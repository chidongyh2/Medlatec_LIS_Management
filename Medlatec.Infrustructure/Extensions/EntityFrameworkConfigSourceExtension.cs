using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Medlatec.Infrastructure.Extensions
{
    public class EntityFrameworkConfigSourceExtension<T> : IConfigurationSource where T : DbContext
    {
        private readonly IDbConnection _connection;

        public EntityFrameworkConfigSourceExtension(IDbConnection connection)
        {
            _connection = connection;
        }


        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EntityFrameworkConfigProvider<T>(_connection);
        }
    }
}
