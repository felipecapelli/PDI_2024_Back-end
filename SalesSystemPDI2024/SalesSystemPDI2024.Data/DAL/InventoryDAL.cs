using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class InventoryDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[Inventory]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<Inventory> List()
        {
            var lista = new List<Inventory>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[Inventory] ORDER BY InventoryID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long inventoryID = Convert.ToInt64(dataReader["InventoryID"]);
                long productID = Convert.ToInt64(dataReader["ProductID"]);
                decimal quantityPerBaseUnityOfMeasurement = Convert.ToDecimal(dataReader["QuantityPerBaseUnityOfMeasurement"]);
                long parentOrSubsidiaryStokID = Convert.ToInt64(dataReader["ParentOrSubsidiaryStokID"]);

                Inventory inventory = new()
                {
                    InventoryID = inventoryID,
                    ProductID = productID,
                    QuantityPerBaseUnityOfMeasurement = quantityPerBaseUnityOfMeasurement,
                    ParentOrSubsidiaryStokID = parentOrSubsidiaryStokID
                };

                lista.Add(inventory);
            }

            return lista;
        }

        public void Add(Inventory inventory)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[Inventory] values (@productID, @quantityPerBaseUnityOfMeasurement, @parentOrSubsidiaryStokID);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productID", inventory.ProductID);
            command.Parameters.AddWithValue("@quantityPerBaseUnityOfMeasurement", inventory.QuantityPerBaseUnityOfMeasurement);
            command.Parameters.AddWithValue("@parentOrSubsidiaryStokID", inventory.ParentOrSubsidiaryStokID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public Inventory RetrieveByID(long inventoryIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[Inventory] where InventoryID = @inventoryIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@inventoryIDToLookFor", inventoryIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long inventoryID = Convert.ToInt64(dataReader["InventoryID"]);
            long productID = Convert.ToInt64(dataReader["ProductID"]);
            decimal quantityPerBaseUnityOfMeasurement = Convert.ToDecimal(dataReader["QuantityPerBaseUnityOfMeasurement"]);
            long parentOrSubsidiaryStokID = Convert.ToInt64(dataReader["ParentOrSubsidiaryStokID"]);

            Inventory inventory = new()
            {
                InventoryID = inventoryID,
                ProductID = productID,
                QuantityPerBaseUnityOfMeasurement = quantityPerBaseUnityOfMeasurement,
                ParentOrSubsidiaryStokID = parentOrSubsidiaryStokID
            };

            return inventory;
        }

        public void Update(Inventory inventory, long inventoryIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[Inventory] SET ProductID = @productID, QuantityPerBaseUnityOfMeasurement = @quantityPerBaseUnityOfMeasurement, ParentOrSubsidiaryStokID = @parentOrSubsidiaryStokID WHERE InventoryID = @inventoryIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@inventoryIDToLookFor", inventoryIDToLookFor);
            command.Parameters.AddWithValue("@productID", inventory.ProductID);
            command.Parameters.AddWithValue("@quantityPerBaseUnityOfMeasurement", inventory.QuantityPerBaseUnityOfMeasurement);
            command.Parameters.AddWithValue("@parentOrSubsidiaryStokID", inventory.ParentOrSubsidiaryStokID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(Inventory inventory)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[Inventory] WHERE [InventoryID] = @inventoryID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@inventoryID", inventory.InventoryID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
