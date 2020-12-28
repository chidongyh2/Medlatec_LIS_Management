using Dapper;
using LIS.Infrastructure.Domain.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace LIS.Infrastructure.Extensions
{
    public class EntityFrameworkConfigProvider<T> : ConfigurationProvider where T : DbContext
    {
        private readonly IDbConnection _connection;

        public EntityFrameworkConfigProvider(IDbConnection connection)
        {
            _connection = connection;
        }

        public override void Load()
        {
            try
            {
                _connection.Open();
                var items = _connection.Execute(@"select * from Persons");
            }
            catch (MissingMethodException)
            {
                Console.WriteLine($"Type {typeof(T).Name} must has a constructor with only DbContextOptions parameter to use as config value store");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"Type {typeof(T).Name} must has a Configuration DbSet");
            }
            catch (Exception e)
            {
                Console.WriteLine("[FATAL] Please run dotnet ef database update first");
            }
            finally
            {
                _connection?.Close();
            }
        }
    }
}
