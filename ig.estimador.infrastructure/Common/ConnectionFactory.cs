using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ig.estimador.infrastructure.Common
{
    public class ConnectionFactory
    {
        public static SqlConnection Connection(IConfiguration configuration)
        {
            return new SqlConnection(configuration.GetConnectionString("Estimadordb"));
        }
    }
}
