using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class OrderStatusChangeDAL
    {
        public IEnumerable<OrderStatusChange> List()
        {
            var lista = new List<OrderStatusChange>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[OrderStatusesChanges]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long orderID = Convert.ToInt64(dataReader["OrderID"]);
                long statusID = Convert.ToInt64(dataReader["StatusID"]);
                string changeDate = Convert.ToString(dataReader["ChangeDate"]);
                string description = Convert.ToString(dataReader["Description"]);

                OrderStatusChange orderStatusChange = new()
                {
                    OrderID = orderID,
                    StatusID = statusID,
                    ChangeDate = changeDate,
                    Description = description
                };

                lista.Add(orderStatusChange);
            }

            return lista;
        }

        public void Add(OrderStatusChange orderStatusChange)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[OrderStatusesChanges] values (@orderID, @statusID, @changeDate, @description);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@orderID", orderStatusChange.OrderID);
            command.Parameters.AddWithValue("@statusID", orderStatusChange.StatusID);
            command.Parameters.AddWithValue("@changeDate", orderStatusChange.ChangeDate);
            command.Parameters.AddWithValue("@description", orderStatusChange.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public OrderStatusChange RetrieveByID(long orderIDToLookFor, long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[OrderStatusesChanges] where OrderID = @orderID and StatusID = @statusID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@orderID", orderIDToLookFor);
            command.Parameters.AddWithValue("@statusID", statusIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long orderID = Convert.ToInt64(dataReader["OrderID"]);
            long statusID = Convert.ToInt64(dataReader["StatusID"]);
            string changeDate = Convert.ToString(dataReader["ChangeDate"]);
            string description = Convert.ToString(dataReader["Description"]);

            OrderStatusChange orderStatusChange = new()
            {
                OrderID = orderID,
                StatusID = statusID,
                ChangeDate = changeDate,
                Description = description
            };

            return orderStatusChange;
        }

        public void Update(OrderStatusChange orderStatusChange, long orderIDToLookFor, long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[OrderStatusesChanges] SET ChangeDate = @changeDate, Description = @description WHERE OrderID = @orderID and StatusID = @statusID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@orderID", orderStatusChange.OrderID);
            command.Parameters.AddWithValue("@statusID", orderStatusChange.StatusID);
            command.Parameters.AddWithValue("@changeDate", orderStatusChange.ChangeDate);
            command.Parameters.AddWithValue("@description", orderStatusChange.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(OrderStatusChange orderStatusChange)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[OrderStatusesChanges] WHERE OrderID = @orderID and StatusID = @statusID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@orderID", orderStatusChange.OrderID);
            command.Parameters.AddWithValue("@statusID", orderStatusChange.StatusID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
