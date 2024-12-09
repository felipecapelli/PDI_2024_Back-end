using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class OrderDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[Orders]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<Order> List()
        {
            var lista = new List<Order>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[Orders] ORDER BY OrderID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long orderID = Convert.ToInt64(dataReader["OrderID"]);
                long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
                bool isSale = Convert.ToBoolean(dataReader["IsSale"]);
                DateTime orderOpeningDate = Convert.ToDateTime(dataReader["OrderOpeningDate"]);
                DateTime orderClosingDate = Convert.ToDateTime(dataReader["OrderClosingDate"]);

                Order order = new()
                {
                    OrderID = orderID,
                    CustomersSuppliersID = customersSuppliersID,
                    IsSale = isSale,
                    OrderOpeningDate = orderOpeningDate,
                    OrderClosingDate = orderClosingDate
                };

                lista.Add(order);
            }

            return lista;
        }

        public void Add(Order order)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[Orders] values (@customersSuppliersID, @isSale, @orderOpeningDate, @orderClosingDate);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@customersSuppliersID", order.CustomersSuppliersID);
            command.Parameters.AddWithValue("@isSale", order.IsSale);
            command.Parameters.AddWithValue("@orderOpeningDate", order.OrderOpeningDate);
            command.Parameters.AddWithValue("@orderClosingDate", order.OrderClosingDate);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public Order RetrieveByID(long orderIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[Orders] where OrderID = @orderIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@orderIDToLookFor", orderIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long orderID = Convert.ToInt64(dataReader["OrderID"]);
            long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
            bool isSale = Convert.ToBoolean(dataReader["IsSale"]);
            DateTime orderOpeningDate = Convert.ToDateTime(dataReader["OrderOpeningDate"]);
            DateTime orderClosingDate = Convert.ToDateTime(dataReader["OrderClosingDate"]);

            Order order = new()
            {
                OrderID = orderID,
                CustomersSuppliersID = customersSuppliersID,
                IsSale = isSale,
                OrderOpeningDate = orderOpeningDate,
                OrderClosingDate = orderClosingDate
            };

            return order;
        }

        public void Update(Order order, long orderIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[Orders] SET CustomersSuppliersID = @customersSuppliersID, IsSale = @isSale, OrderOpeningDate = @orderOpeningDate, OrderClosingDate = @orderClosingDate WHERE OrderID = @orderIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@orderIDToLookFor", orderIDToLookFor);
            command.Parameters.AddWithValue("@customersSuppliersID", order.CustomersSuppliersID);
            command.Parameters.AddWithValue("@isSale", order.IsSale);
            command.Parameters.AddWithValue("@orderOpeningDate", order.OrderOpeningDate);
            command.Parameters.AddWithValue("@orderClosingDate", order.OrderClosingDate);


            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(Order order)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[Orders] WHERE [OrderID] = @orderID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@orderID", order.OrderID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
