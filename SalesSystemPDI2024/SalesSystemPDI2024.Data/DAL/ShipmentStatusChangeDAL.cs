using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class ShipmentStatusChangeDAL
    {
        public IEnumerable<ShipmentStatusChange> List()
        {
            var lista = new List<ShipmentStatusChange>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[ShipmentStatusesChanges]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long shipmentID = Convert.ToInt64(dataReader["ShipmentID"]);
                long statusID = Convert.ToInt64(dataReader["StatusID"]);
                string changeDate = Convert.ToString(dataReader["ChangeDate"]);
                string description = Convert.ToString(dataReader["Description"]);

                ShipmentStatusChange shipmentStatusChange = new()
                {
                    ShipmentID = shipmentID,
                    StatusID = statusID,
                    ChangeDate = changeDate,
                    Description = description
                };

                lista.Add(shipmentStatusChange);
            }

            return lista;
        }

        public void Add(ShipmentStatusChange shipmentStatusChange)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[ShipmentStatusesChanges] values (@shipmentID, @statusID, @changeDate, @description);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@shipmentID", shipmentStatusChange.ShipmentID);
            command.Parameters.AddWithValue("@statusID", shipmentStatusChange.StatusID);
            command.Parameters.AddWithValue("@changeDate", shipmentStatusChange.ChangeDate);
            command.Parameters.AddWithValue("@description", shipmentStatusChange.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public ShipmentStatusChange RetrieveByID(long shipmentIDToLookFor, long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[ShipmentStatusesChanges] where ShipmentID = @shipmentIDToLookFor and StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@shipmentIDToLookFor", shipmentIDToLookFor);
            command.Parameters.AddWithValue("@statusIDToLookFor", statusIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long shipmentID = Convert.ToInt64(dataReader["ShipmentID"]);
            long statusID = Convert.ToInt64(dataReader["StatusID"]);
            string changeDate = Convert.ToString(dataReader["ChangeDate"]);
            string description = Convert.ToString(dataReader["Description"]);

            ShipmentStatusChange shipmentStatusChange = new()
            {
                ShipmentID = shipmentID,
                StatusID = statusID,
                ChangeDate = changeDate,
                Description = description
            };

            return shipmentStatusChange;
        }

        public void Update(ShipmentStatusChange shipmentStatusChange, long shipmentIDToLookFor, long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[ShipmentStatusesChanges] SET ChangeDate = @changeDate, Description = @description WHERE ShipmentID = @shipmentIDToLookFor and StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@shipmentID", shipmentIDToLookFor);
            command.Parameters.AddWithValue("@statusID", statusIDToLookFor);
            command.Parameters.AddWithValue("@changeDate", shipmentStatusChange.ChangeDate);
            command.Parameters.AddWithValue("@description", shipmentStatusChange.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(ShipmentStatusChange shipmentStatusChange)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[ShipmentStatusesChanges] WHERE ShipmentID = @shipmentIDToLookFor and StatusID = @statusIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@shipmentIDToLookFor", shipmentStatusChange.ShipmentID);
            command.Parameters.AddWithValue("@statusIDToLookFor", shipmentStatusChange.StatusID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
