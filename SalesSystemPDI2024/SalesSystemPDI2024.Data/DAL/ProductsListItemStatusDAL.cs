using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class ProductsListItemStatusDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[ProductsListItemStatuses]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<ProductsListItemStatus> List()
        {
            var lista = new List<ProductsListItemStatus>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[ProductsListItemStatuses] ORDER BY StatusID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long statusID = Convert.ToInt64(dataReader["StatusID"]);
                string description = Convert.ToString(dataReader["Description"]);

                ProductsListItemStatus productsListItemStatus = new()
                {
                    StatusID = statusID,
                    Description = description
                };

                lista.Add(productsListItemStatus);
            }

            return lista;
        }

        public void Add(ProductsListItemStatus productsListItemStatus)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[ProductsListItemStatuses] values (@description);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@description", productsListItemStatus.Description);
            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public ProductsListItemStatus RetrieveByID(long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[ProductsListItemStatuses] where StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@statusIDToLookFor", statusIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long statusID = Convert.ToInt64(dataReader["StatusID"]);
            string description = Convert.ToString(dataReader["Description"]);

            ProductsListItemStatus productsListItemStatus = new()
            {
                StatusID = statusID,
                Description = description
            };

            return productsListItemStatus;
        }

        public void Update(ProductsListItemStatus productsListItemStatus, long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[ProductsListItemStatuses] SET Description = @description WHERE StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@statusIDToLookFor", statusIDToLookFor);
            command.Parameters.AddWithValue("@description", productsListItemStatus.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(ProductsListItemStatus productsListItemStatus)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[ProductsListItemStatuses] WHERE [StatusID] = @statusID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@statusID", productsListItemStatus.StatusID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
