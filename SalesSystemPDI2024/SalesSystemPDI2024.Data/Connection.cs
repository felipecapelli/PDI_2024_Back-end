using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace SalesSystemPDI2024.Data
{
    public class Connection
    {
        private string connectionString = "Data Source=\"192.168.1.39, 1433\";Initial Catalog=SalesSystemPDI2024;User ID=sa;Password=@Teste123;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; //The teacher asked to delete this part of the connection string: Connect Timeout=30;
        //private string connectionString = "Data Source=\"localhost, 1433\";Initial Catalog=SalesSystemPDI2024;User ID=sa;Password=@Teste123;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; //The teacher asked to delete this part of the connection string: Connect Timeout=30;

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(connectionString);
        }
    }
}
