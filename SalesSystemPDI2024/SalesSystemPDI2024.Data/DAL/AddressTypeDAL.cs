using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class AddressTypeDAL
    {
        public IEnumerable<AddressType> List()
        {
            var lista = new List<AddressType>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[AddressTypes]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long addressTypeID = Convert.ToInt64(dataReader["AddressTypeID"]);
                string description = Convert.ToString(dataReader["Description"]);

                AddressType addressType = new()
                {
                    AddressTypeID = addressTypeID,
                    Description = description
                };

                lista.Add(addressType);
            }

            return lista;
        }

        public void Add(AddressType addressType)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[AddressTypes] values (@description);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@description", addressType.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public AddressType RetrieveByID(long addressTypeIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[AddressTypes] where AddressTypeID = @addressTypeIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@addressTypeIDToLookFor", addressTypeIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long addressTypeID = Convert.ToInt64(dataReader["AddressTypeID"]);
            string description = Convert.ToString(dataReader["Description"]);

            AddressType addressType = new()
            {
                AddressTypeID = addressTypeID,
                Description = description
            };

            return addressType;
        }

        public void Update(AddressType addressType, long addressTypeIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[AddressTypes] SET Description = @description WHERE AddressTypeID = @addressTypeIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@addressTypeIDToLookFor", addressTypeIDToLookFor);
            command.Parameters.AddWithValue("@description", addressType.Description);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(AddressType addressType)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[AddressTypes] WHERE [AddressTypeID] = @addressTypeID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@addressTypeID", addressType.AddressTypeID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
