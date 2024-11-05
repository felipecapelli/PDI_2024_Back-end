using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class VariableUnitOfMeasurementDAL
    {
        public IEnumerable<VariableUnitOfMeasurement> List()
        {
            var lista = new List<VariableUnitOfMeasurement>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[VariableUnitsOfMeasurement]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long variableUnitOfMeasurementID = Convert.ToInt64(dataReader["VariableUnitOfMeasurementID"]);
                string unitName = Convert.ToString(dataReader["UnitName"]);
                string unitAbbreviation = Convert.ToString(dataReader["UnitAbbreviation"]);
                long baseUnitOfMeasurementID = Convert.ToInt64(dataReader["BaseUnitOfMeasurementID"]);
                decimal quantityOfBaseUnits = Convert.ToDecimal(dataReader["QuantityOfBaseUnits"]);

                VariableUnitOfMeasurement variableUnitOfMeasurement = new()
                {
                    VariableUnitOfMeasurementID = variableUnitOfMeasurementID,
                    UnitName = unitName,
                    UnitAbbreviation = unitAbbreviation,
                    BaseUnitOfMeasurementID = baseUnitOfMeasurementID,
                    QuantityOfBaseUnits = quantityOfBaseUnits
                };

                lista.Add(variableUnitOfMeasurement);
            }

            return lista;
        }

        public void Add(VariableUnitOfMeasurement variableUnitOfMeasurement)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[VariableUnitsOfMeasurement] values (@unitName, @unitAbbreviation, @baseUnitOfMeasurementID, @quantityOfBaseUnits);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@unitName", variableUnitOfMeasurement.UnitName);
            command.Parameters.AddWithValue("@unitAbbreviation", variableUnitOfMeasurement.UnitAbbreviation);
            command.Parameters.AddWithValue("@baseUnitOfMeasurementID", variableUnitOfMeasurement.BaseUnitOfMeasurementID);
            command.Parameters.AddWithValue("@quantityOfBaseUnits", variableUnitOfMeasurement.QuantityOfBaseUnits);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public VariableUnitOfMeasurement RetrieveByID(long variableUnitOfMeasurementIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[VariableUnitsOfMeasurement] where VariableUnitOfMeasurementID = @variableUnitOfMeasurementIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@variableUnitOfMeasurementIDToLookFor", variableUnitOfMeasurementIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long variableUnitOfMeasurementID = Convert.ToInt64(dataReader["VariableUnitOfMeasurementID"]);
            string unitName = Convert.ToString(dataReader["UnitName"]);
            string unitAbbreviation = Convert.ToString(dataReader["UnitAbbreviation"]);
            long baseUnitOfMeasurementID = Convert.ToInt64(dataReader["BaseUnitOfMeasurementID"]);
            decimal quantityOfBaseUnits = Convert.ToDecimal(dataReader["QuantityOfBaseUnits"]);

            VariableUnitOfMeasurement variableUnitOfMeasurement = new()
            {
                VariableUnitOfMeasurementID = variableUnitOfMeasurementID,
                UnitName = unitName,
                UnitAbbreviation = unitAbbreviation,
                BaseUnitOfMeasurementID = baseUnitOfMeasurementID,
                QuantityOfBaseUnits = quantityOfBaseUnits
            };

            return variableUnitOfMeasurement;
        }

        public void Update(VariableUnitOfMeasurement variableUnitOfMeasurement, long variableUnitOfMeasurementIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[VariableUnitsOfMeasurement] SET UnitName = @unitName, UnitAbbreviation = @unitAbbreviation, BaseUnitOfMeasurementID = @baseUnitOfMeasurementID, QuantityOfBaseUnits = @quantityOfBaseUnits WHERE VariableUnitOfMeasurementID = @variableUnitOfMeasurementIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@variableUnitOfMeasurementIDToLookFor", variableUnitOfMeasurementIDToLookFor);
            command.Parameters.AddWithValue("@unitName", variableUnitOfMeasurement.UnitName);
            command.Parameters.AddWithValue("@unitAbbreviation", variableUnitOfMeasurement.UnitAbbreviation);
            command.Parameters.AddWithValue("@baseUnitOfMeasurementID", variableUnitOfMeasurement.BaseUnitOfMeasurementID);
            command.Parameters.AddWithValue("@quantityOfBaseUnits", variableUnitOfMeasurement.QuantityOfBaseUnits);


            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(VariableUnitOfMeasurement variableUnitOfMeasurement)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[VariableUnitsOfMeasurement] WHERE [VariableUnitOfMeasurementID] = @variableUnitOfMeasurement";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@variableUnitOfMeasurement", variableUnitOfMeasurement.VariableUnitOfMeasurementID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
