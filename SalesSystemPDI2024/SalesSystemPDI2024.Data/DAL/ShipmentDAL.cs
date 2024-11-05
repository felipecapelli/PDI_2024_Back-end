using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class ShipmentDAL
    {
        public IEnumerable<Shipment> List()
        {
            var lista = new List<Shipment>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[Shipments]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long shipmentID = Convert.ToInt64(dataReader["ShipmentID"]);
                long productListID = Convert.ToInt64(dataReader["ProductListID"]);
                DateTime expectedDateOfShipment = Convert.ToDateTime(dataReader["ExpectedDateOfShipment"]);
                DateTime shipmentDepartureDate = Convert.ToDateTime(dataReader["ShipmentDepartureDate"]);
                DateTime shipmentArrivalDate = Convert.ToDateTime(dataReader["ShipmentArrivalDate"]);

                Shipment shipment = new()
                {
                    ShipmentID = shipmentID,
                    ProductListID = productListID,
                    ExpectedDateOfShipment = expectedDateOfShipment,
                    ShipmentDepartureDate = shipmentDepartureDate,
                    ShipmentArrivalDate = shipmentArrivalDate
                };

                lista.Add(shipment);
            }

            return lista;
        }

        public void Add(Shipment shipment)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[Shipments] values (@productListID, @expectedDateOfShipment, @shipmentDepartureDate, @shipmentArrivalDate);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@productListID", shipment.ProductListID);
            command.Parameters.AddWithValue("@expectedDateOfShipment", shipment.ExpectedDateOfShipment);
            command.Parameters.AddWithValue("@shipmentDepartureDate", shipment.ShipmentDepartureDate);
            command.Parameters.AddWithValue("@shipmentArrivalDate", shipment.ShipmentArrivalDate);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public Shipment RetrieveByID(long shipmentIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[Shipments] where ShipmentID = @shipmentIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@shipmentIDToLookFor", shipmentIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long shipmentID = Convert.ToInt64(dataReader["ShipmentID"]);
            long productListID = Convert.ToInt64(dataReader["ProductListID"]);
            DateTime expectedDateOfShipment = Convert.ToDateTime(dataReader["ExpectedDateOfShipment"]);
            DateTime shipmentDepartureDate = Convert.ToDateTime(dataReader["ShipmentDepartureDate"]);
            DateTime shipmentArrivalDate = Convert.ToDateTime(dataReader["ShipmentArrivalDate"]);

            Shipment shipment = new()
            {
                ShipmentID = shipmentID,
                ProductListID = productListID,
                ExpectedDateOfShipment = expectedDateOfShipment,
                ShipmentDepartureDate = shipmentDepartureDate,
                ShipmentArrivalDate = shipmentArrivalDate
            };

            return shipment;
        }

        public void Update(Shipment shipment, long shipmentIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[Shipments] SET ProductListID = @productListID, ExpectedDateOfShipment = @expectedDateOfShipment, ShipmentDepartureDate = @shipmentDepartureDate, ShipmentArrivalDate = @shipmentArrivalDate WHERE ShipmentID = @shipmentIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@shipmentIDToLookFor", shipmentIDToLookFor);
            command.Parameters.AddWithValue("@productListID", shipment.ProductListID);
            command.Parameters.AddWithValue("@expectedDateOfShipment", shipment.ExpectedDateOfShipment);
            command.Parameters.AddWithValue("@shipmentDepartureDate", shipment.ShipmentDepartureDate);
            command.Parameters.AddWithValue("@shipmentArrivalDate", shipment.ShipmentArrivalDate);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(Shipment shipment)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[Shipments] WHERE [ShipmentID] = @shipmentID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@shipmentID", shipment.ShipmentID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
