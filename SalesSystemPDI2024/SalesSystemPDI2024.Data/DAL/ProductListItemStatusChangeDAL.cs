using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class ProductListItemStatusChangeDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[ProductsListItemStatusesChanges]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<ProductListItemStatusChange> List()
        {
            var lista = new List<ProductListItemStatusChange>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[ProductsListItemStatusesChanges] ORDER BY ProductListID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long productListID = Convert.ToInt64(dataReader["ProductListID"]);
                long statusID = Convert.ToInt64(dataReader["StatusID"]);
                string changeDate = Convert.ToString(dataReader["ChangeDate"]);
                string description = Convert.ToString(dataReader["Description"]);

                ProductListItemStatusChange productListItemStatusChange = new()
                {
                    ProductListID = productListID,
                    StatusID = statusID,
                    ChangeDate = changeDate,
                    Description = description
                };

                lista.Add(productListItemStatusChange);
            }

            return lista;
        }

        public void Add(ProductListItemStatusChange productListItemStatusChange)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[ProductsListItemStatusesChanges] values (@productListID, @statusID, @changeDate, @description);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productListID", productListItemStatusChange.ProductListID);
            command.Parameters.AddWithValue("@statusID", productListItemStatusChange.StatusID);
            command.Parameters.AddWithValue("@changeDate", productListItemStatusChange.ChangeDate);
            command.Parameters.AddWithValue("@description", productListItemStatusChange.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public ProductListItemStatusChange RetrieveByID(long productListIDToLookFor, long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[ProductsListItemStatusesChanges] where ProductListID = @productListIDToLookFor and StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productListIDToLookFor", productListIDToLookFor);
            command.Parameters.AddWithValue("@statusIDToLookFor", statusIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long productListID = Convert.ToInt64(dataReader["ProductListID"]);
            long statusID = Convert.ToInt64(dataReader["StatusID"]);
            string changeDate = Convert.ToString(dataReader["ChangeDate"]);
            string description = Convert.ToString(dataReader["Description"]);

            ProductListItemStatusChange productListItemStatusChange = new()
            {
                ProductListID = productListID,
                StatusID = statusID,
                ChangeDate = changeDate,
                Description = description
            };

            return productListItemStatusChange;
        }

        public void Update(ProductListItemStatusChange productListItemStatusChange, long productListIDToLookFor, long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[ProductsListItemStatusesChanges] SET ChangeDate = @changeDate, Description = @description WHERE ProductListID = @productListID and StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@productListID", productListIDToLookFor);
            command.Parameters.AddWithValue("@statusID", statusIDToLookFor);
            command.Parameters.AddWithValue("@changeDate", productListItemStatusChange.ChangeDate);
            command.Parameters.AddWithValue("@description", productListItemStatusChange.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(ProductListItemStatusChange productListItemStatusChange)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[ProductsListItemStatusesChanges] WHERE ProductListID = @productListIDToLookFor and StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productListIDToLookFor", productListItemStatusChange.ProductListID);
            command.Parameters.AddWithValue("@statusIDToLookFor", productListItemStatusChange.StatusID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
