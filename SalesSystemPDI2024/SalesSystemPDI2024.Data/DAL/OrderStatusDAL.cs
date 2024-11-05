using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class OrderStatusDAL
    {
        public IEnumerable<OrderStatus> List()
        {
            var lista = new List<OrderStatus>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[OrderStatuses]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long statusID = Convert.ToInt64(dataReader["StatusID"]);
                string description = Convert.ToString(dataReader["Description"]);

                OrderStatus orderStatus = new()
                {
                    StatusID = statusID,
                    Description = description
                };

                lista.Add(orderStatus);
            }

            return lista;
        }

        public void Add(OrderStatus orderStatus)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[OrderStatuses] values (@description);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@description", orderStatus.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public OrderStatus RetrieveByID(long StatusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[OrderStatuses] where StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@statusIDToLookFor", StatusIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long statusID = Convert.ToInt64(dataReader["StatusID"]);
            string description = Convert.ToString(dataReader["Description"]);

            OrderStatus orderStatus = new()
            {
                StatusID = statusID,
                Description = description
            };

            return orderStatus;
        }

        public void Update(OrderStatus orderStatus, long StatusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[OrderStatuses] SET Description = @description WHERE StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@statusIDToLookFor", StatusIDToLookFor);
            command.Parameters.AddWithValue("@description", orderStatus.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(OrderStatus OrderStatus)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[OrderStatuses] WHERE [StatusID] = @statusID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@statusID", OrderStatus.StatusID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
