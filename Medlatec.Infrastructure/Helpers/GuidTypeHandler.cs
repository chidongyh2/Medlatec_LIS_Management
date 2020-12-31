using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Medlatec.Infrastructure.Helpers
{
    public class GuidTypeHandler : SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            OracleParameter oracleParameter = (OracleParameter)parameter;
            oracleParameter.OracleDbType = OracleDbType.Raw;
            parameter.Value = value;
        }

        public object Parse(Type destinationType, object value)
        {
            return new Guid((byte[])value);
        }
    }
}
