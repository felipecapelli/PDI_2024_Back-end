using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class BaseUnitOfMeasurementDAL
    {
        public IEnumerable<BaseUnitOfMeasurement> List()
        {
            var lista = new List<BaseUnitOfMeasurement>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[BaseUnitsOfMeasurement]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long baseUnitOfMeasurementID = Convert.ToInt64(dataReader["BaseUnitOfMeasurementID"]);
                string unitName = Convert.ToString(dataReader["UnitName"]);
                string unitAbbreviation = Convert.ToString(dataReader["UnitAbbreviation"]);

                BaseUnitOfMeasurement baseUnitOfMeasurement = new()
                {
                    BaseUnitOfMeasurementID = baseUnitOfMeasurementID,
                    UnitName = unitName,
                    UnitAbbreviation = unitAbbreviation
                };

                lista.Add(baseUnitOfMeasurement);
            }

            return lista;
        }

        public void Add(BaseUnitOfMeasurement baseUnitOfMeasurement)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[BaseUnitsOfMeasurement] values (@unitName, @unitAbbreviation);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@unitName", baseUnitOfMeasurement.UnitName);
            command.Parameters.AddWithValue("@unitAbbreviation", baseUnitOfMeasurement.UnitAbbreviation);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public BaseUnitOfMeasurement RetrieveByID(long baseUnitOfMeasurementIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[BaseUnitsOfMeasurement] where BaseUnitOfMeasurementID = @baseUnitOfMeasurementIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@baseUnitOfMeasurementIDToLookFor", baseUnitOfMeasurementIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long baseUnitOfMeasurementID = Convert.ToInt64(dataReader["BaseUnitOfMeasurementID"]);
            string unitName = Convert.ToString(dataReader["UnitName"]);
            string unitAbbreviation = Convert.ToString(dataReader["UnitAbbreviation"]);

            BaseUnitOfMeasurement baseUnitOfMeasurement = new()
            {
                BaseUnitOfMeasurementID = baseUnitOfMeasurementID,
                UnitName = unitName,
                UnitAbbreviation = unitAbbreviation
            };


            return baseUnitOfMeasurement;
        }

        public void Update(BaseUnitOfMeasurement baseUnitOfMeasurement, long baseUnitOfMeasurementIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[BaseUnitsOfMeasurement] SET UnitName = @unitName, UnitAbbreviation = @unitAbbreviation WHERE BaseUnitOfMeasurementID = @baseUnitOfMeasurementIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@baseUnitOfMeasurementIDToLookFor", baseUnitOfMeasurementIDToLookFor);
            command.Parameters.AddWithValue("@unitName", baseUnitOfMeasurement.UnitName);
            command.Parameters.AddWithValue("@unitAbbreviation", baseUnitOfMeasurement.UnitAbbreviation);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(BaseUnitOfMeasurement baseUnitOfMeasurement)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[BaseUnitsOfMeasurement] WHERE [BaseUnitOfMeasurementID] = @baseUnitOfMeasurementID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@baseUnitOfMeasurementID", baseUnitOfMeasurement.BaseUnitOfMeasurementID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
