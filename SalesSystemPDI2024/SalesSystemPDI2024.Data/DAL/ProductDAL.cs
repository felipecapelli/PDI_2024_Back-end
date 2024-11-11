using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class ProductDAL
    {
        public IEnumerable<Product> List()
        {
            var lista = new List<Product>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[Products]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long productID = Convert.ToInt64(dataReader["ProductID"]);
                string productName = Convert.ToString(dataReader["ProductName"]);
                long baseUnitOfMeasurementID = Convert.ToInt64(dataReader["BaseUnitOfMeasurementID"]);
                long baseCurrencyID = Convert.ToInt64(dataReader["BaseCurrencyID"]);
                decimal price = Convert.ToDecimal(dataReader["Price"]);

                Product product = new()
                {
                    ProductID = productID,
                    ProductName = productName,
                    BaseUnitOfMeasurementID = baseUnitOfMeasurementID,
                    BaseCurrencyID = baseCurrencyID,
                    Price = price
                };

                lista.Add(product);
            }

            return lista;
        }

        public void Add(Product product)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[Products] values (@productName, @baseUnitOfMeasurementID, @baseCurrencyID, @price);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productName", product.ProductName);
            command.Parameters.AddWithValue("@baseUnitOfMeasurementID", product.BaseUnitOfMeasurementID);
            command.Parameters.AddWithValue("@baseCurrencyID", product.BaseCurrencyID);
            command.Parameters.AddWithValue("@price", product.Price);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public Product RetrieveByID(long productIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[Products] where ProductID = @productIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productIDToLookFor", productIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long productID = Convert.ToInt64(dataReader["ProductID"]);
            string productName = Convert.ToString(dataReader["ProductName"]);
            long baseUnitOfMeasurementID = Convert.ToInt64(dataReader["BaseUnitOfMeasurementID"]);
            long baseCurrencyID = Convert.ToInt64(dataReader["BaseCurrencyID"]);
            decimal price = Convert.ToDecimal(dataReader["Price"]);

            Product product = new()
            {
                ProductID = productID,
                ProductName = productName,
                BaseUnitOfMeasurementID = baseUnitOfMeasurementID,
                BaseCurrencyID = baseCurrencyID,
                Price = price
            };

            return product;
        }

        public void Update(Product product, long productIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[Products] SET ProductName = @productName, BaseUnitOfMeasurementID = @baseUnitOfMeasurementID, BaseCurrencyID = @baseCurrencyID, Price = @price WHERE ProductID = @productIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@productIDToLookFor", productIDToLookFor);
            command.Parameters.AddWithValue("@productName", product.ProductName);
            command.Parameters.AddWithValue("@baseUnitOfMeasurementID", product.BaseUnitOfMeasurementID);
            command.Parameters.AddWithValue("@baseCurrencyID", product.BaseCurrencyID);
            command.Parameters.AddWithValue("@price", product.Price);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(Product product)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[Products] WHERE [ProductID] = @productID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productID", product.ProductID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
