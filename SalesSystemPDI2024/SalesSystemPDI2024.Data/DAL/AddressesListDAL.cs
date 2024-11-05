using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class AddressesListDAL
    {
        public IEnumerable<AddressesList> List()
        {
            var lista = new List<AddressesList>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[AddressesList]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long addressesListID = Convert.ToInt64(dataReader["AddressesListID"]);
                long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
                long addressID = Convert.ToInt64(dataReader["AddressID"]);

                AddressesList addressesList = new()
                {
                    AddressesListID = addressesListID,
                    CustomersSuppliersID = customersSuppliersID,
                    AddressID = addressID
                };

                lista.Add(addressesList);
            }

            return lista;
        }

        public void Add(AddressesList addressesList)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[AddressesList] values (@customersSuppliersID, @addressID);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@customersSuppliersID", addressesList.CustomersSuppliersID);
            command.Parameters.AddWithValue("@addressID", addressesList.AddressID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public AddressesList RetrieveByID(long addressesListIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[AddressesList] where AddressesListID = @addressesListID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@addressesListID", addressesListIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long addressesListID = Convert.ToInt64(dataReader["AddressesListID"]);
            long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
            long addressID = Convert.ToInt64(dataReader["AddressID"]);

            AddressesList addressesList = new()
            {
                AddressesListID = addressesListID,
                CustomersSuppliersID = customersSuppliersID,
                AddressID = addressID
            };

            return addressesList;
        }

        public void Update(AddressesList addressesList, long addressesListIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[AddressesList] SET CustomersSuppliersID = @customersSuppliersID, AddressID = @addressID WHERE AddressID = @addressesListID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@addressesListID", addressesListIDToLookFor);
            command.Parameters.AddWithValue("@customersSuppliersID", addressesList.CustomersSuppliersID);
            command.Parameters.AddWithValue("@addressID", addressesList.AddressID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(AddressesList addressesList)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[AddressesList] WHERE [AddressesListID] = @addressesListID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@addressesListID", addressesList.AddressesListID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
