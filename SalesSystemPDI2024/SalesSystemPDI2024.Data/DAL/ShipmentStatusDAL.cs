using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class ShipmentStatusDAL
    {
        public IEnumerable<ShipmentStatus> List()
        {
            var lista = new List<ShipmentStatus>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[ShipmentStatuses]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long statusID = Convert.ToInt64(dataReader["StatusID"]);
                string description = Convert.ToString(dataReader["Description"]);

                ShipmentStatus shipmentStatus = new()
                {
                    StatusID = statusID,
                    Description = description
                };

                lista.Add(shipmentStatus);
            }

            return lista;
        }

        public void Add(ShipmentStatus shipmentStatus)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[ShipmentStatuses] values (@statusID, @description);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@statusID", shipmentStatus.StatusID);
            command.Parameters.AddWithValue("@description", shipmentStatus.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public ShipmentStatus RetrieveByID(long statusIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[ShipmentStatuses] where StatusID = @statusID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@statusID", statusIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long statusID = Convert.ToInt64(dataReader["StatusID"]);
            string description = Convert.ToString(dataReader["Description"]);

            ShipmentStatus shipmentStatus = new()
            {
                StatusID = statusID,
                Description = description
            };

            return shipmentStatus;
        }

        public void Update(ShipmentStatus shipmentStatus, long addressIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[ShipmentStatuses] SET Description = @description WHERE StatusID = @addressIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@addressIDToLookFor", addressIDToLookFor);
            command.Parameters.AddWithValue("@description", shipmentStatus.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(ShipmentStatus shipmentStatus)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[ShipmentStatuses] WHERE [StatusID] = @statusID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@statusID", shipmentStatus.StatusID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
