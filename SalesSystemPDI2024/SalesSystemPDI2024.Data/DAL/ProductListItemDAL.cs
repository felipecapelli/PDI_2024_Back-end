using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class ProductListItemDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[ProductListItems]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<ProductListItem> List()
        {
            var lista = new List<ProductListItem>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[ProductListItems] ORDER BY ProductListID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long productListID = Convert.ToInt64(dataReader["ProductListID"]);
                long orderID = Convert.ToInt64(dataReader["OrderID"]);
                long productID = Convert.ToInt64(dataReader["ProductID"]);
                long unitOfMeasurementUsedOnTheOrderID = Convert.ToInt64(dataReader["UnitOfMeasurementUsedOnTheOrderID"]);
                long quantityOfUnitMesasurement = Convert.ToInt64(dataReader["QuantityOfUnitMesasurement"]);
                long currencyUsedOnTheOrderID = Convert.ToInt64(dataReader["CurrencyUsedOnTheOrderID"]);
                decimal pricePerUnitOfMeasurement = Convert.ToDecimal(dataReader["PricePerUnitOfMeasurement"]);
                long shippingAddressID = Convert.ToInt64(dataReader["ShippingAddressID"]);
                long parentOrSubsidiaryStokID = Convert.ToInt64(dataReader["ParentOrSubsidiaryStokID"]);

                ProductListItem productListItem = new()
                {
                    ProductListID = productListID,
                    OrderID = orderID,
                    ProductID = productID,
                    UnitOfMeasurementUsedOnTheOrderID = unitOfMeasurementUsedOnTheOrderID,
                    QuantityOfUnitMesasurement = quantityOfUnitMesasurement,
                    CurrencyUsedOnTheOrderID = currencyUsedOnTheOrderID,
                    PricePerUnitOfMeasurement = pricePerUnitOfMeasurement,
                    ShippingAddressID = shippingAddressID,
                    ParentOrSubsidiaryStokID = parentOrSubsidiaryStokID
                };

                lista.Add(productListItem);
            }

            return lista;
        }

        public void Add(ProductListItem productListItem)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[ProductListItems] values (@orderID, @productID, @unitOfMeasurementUsedOnTheOrderID, @quantityOfUnitMesasurement, @currencyUsedOnTheOrderID, @pricePerUnitOfMeasurement, @shippingAddressID, @parentOrSubsidiaryStokID);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productListID", productListItem.ProductListID);
            command.Parameters.AddWithValue("@orderID", productListItem.OrderID);
            command.Parameters.AddWithValue("@productID", productListItem.ProductID);
            command.Parameters.AddWithValue("@unitOfMeasurementUsedOnTheOrderID", productListItem.UnitOfMeasurementUsedOnTheOrderID);
            command.Parameters.AddWithValue("@quantityOfUnitMesasurement", productListItem.QuantityOfUnitMesasurement);
            command.Parameters.AddWithValue("@currencyUsedOnTheOrderID", productListItem.CurrencyUsedOnTheOrderID);
            command.Parameters.AddWithValue("@pricePerUnitOfMeasurement", productListItem.PricePerUnitOfMeasurement);
            command.Parameters.AddWithValue("@shippingAddressID", productListItem.ShippingAddressID);
            command.Parameters.AddWithValue("@parentOrSubsidiaryStokID", productListItem.ParentOrSubsidiaryStokID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public ProductListItem RetrieveByID(long productListIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[ProductListItems] where ProductListID = @productListIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productListIDToLookFor", productListIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long productListID = Convert.ToInt64(dataReader["ProductListID"]);
            long orderID = Convert.ToInt64(dataReader["OrderID"]);
            long productID = Convert.ToInt64(dataReader["ProductID"]);
            long unitOfMeasurementUsedOnTheOrderID = Convert.ToInt64(dataReader["UnitOfMeasurementUsedOnTheOrderID"]);
            long quantityOfUnitMesasurement = Convert.ToInt64(dataReader["QuantityOfUnitMesasurement"]);
            long currencyUsedOnTheOrderID = Convert.ToInt64(dataReader["CurrencyUsedOnTheOrderID"]);
            decimal pricePerUnitOfMeasurement = Convert.ToDecimal(dataReader["PricePerUnitOfMeasurement"]);
            long shippingAddressID = Convert.ToInt64(dataReader["ShippingAddressID"]);
            long parentOrSubsidiaryStokID = Convert.ToInt64(dataReader["ParentOrSubsidiaryStokID"]);

            ProductListItem productListItem = new()
            {
                ProductListID = productListID,
                OrderID = orderID,
                ProductID = productID,
                UnitOfMeasurementUsedOnTheOrderID = unitOfMeasurementUsedOnTheOrderID,
                QuantityOfUnitMesasurement = quantityOfUnitMesasurement,
                CurrencyUsedOnTheOrderID = currencyUsedOnTheOrderID,
                PricePerUnitOfMeasurement = pricePerUnitOfMeasurement,
                ShippingAddressID = shippingAddressID,
                ParentOrSubsidiaryStokID = parentOrSubsidiaryStokID
            };

            return productListItem;
        }

        public void Update(ProductListItem productListItem, long productListIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[ProductListItems] SET ProductListIDToLookFor = @productListIDToLookFor, OrderID = @orderID, ProductID = @productID, UnitOfMeasurementUsedOnTheOrderID = @unitOfMeasurementUsedOnTheOrderID ,QuantityOfUnitMesasurement = @quantityOfUnitMesasurement, CurrencyUsedOnTheOrderID = @currencyUsedOnTheOrderID, PricePerUnitOfMeasurement = @pricePerUnitOfMeasurement, ShippingAddressID = @shippingAddressID, ParentOrSubsidiaryStokID = @parentOrSubsidiaryStokID WHERE ProductListID = @productListIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@productListIDToLookFor", productListIDToLookFor);
            command.Parameters.AddWithValue("@orderID", productListItem.OrderID);
            command.Parameters.AddWithValue("@productID", productListItem.ProductID);
            command.Parameters.AddWithValue("@unitOfMeasurementUsedOnTheOrderID", productListItem.UnitOfMeasurementUsedOnTheOrderID);
            command.Parameters.AddWithValue("@quantityOfUnitMesasurement", productListItem.QuantityOfUnitMesasurement);
            command.Parameters.AddWithValue("@currencyUsedOnTheOrderID", productListItem.CurrencyUsedOnTheOrderID);
            command.Parameters.AddWithValue("@pricePerUnitOfMeasurement", productListItem.PricePerUnitOfMeasurement);
            command.Parameters.AddWithValue("@shippingAddressID", productListItem.ShippingAddressID);
            command.Parameters.AddWithValue("@parentOrSubsidiaryStokID", productListItem.ParentOrSubsidiaryStokID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(ProductListItem productListItem)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[ProductListItems] WHERE [ProductListID] = @productListID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productListID", productListItem.ProductListID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
