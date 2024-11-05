using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class AddressDAL
    {
        public IEnumerable<Address> List()
        {
            var lista = new List<Address>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[Addresses]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                //int addressId = Convert.ToInt32(dataReader["AddressID"]);
                long addressID = Convert.ToInt64(dataReader["AddressID"]);
                string adressNickname = Convert.ToString(dataReader["AddressNickname"]);
                long adressTypeID = Convert.ToInt64(dataReader["AddressTypeID"]);
                string street = Convert.ToString(dataReader["Street"]);
                string number = Convert.ToString(dataReader["Number"]);
                string complement = Convert.ToString(dataReader["Complement"]);
                string city = Convert.ToString(dataReader["City"]);
                string stateOrDistrict = Convert.ToString(dataReader["StateOrDistrict"]);
                string postalCode = Convert.ToString(dataReader["PostalCode"]);
                string country = Convert.ToString(dataReader["Country"]);

                Address address = new() 
                {
                    AddressID = addressID,
                    AdressNickname = adressNickname,
                    AdressTypeID = adressTypeID,
                    Street = street,
                    Number = number,
                    Complement = complement,
                    City = city,
                    StateOrDistrict = stateOrDistrict,
                    PostalCode = postalCode,
                    Country = country
                };

                lista.Add(address);
            }

            return lista;
        }

        public void Add(Address address)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[Addresses] values (@adressNickname, @adressTypeID, @street, @number, @complement, @city, @stateOrDistrict, @postalCode, @country);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@adressNickname", address.AdressNickname);
            command.Parameters.AddWithValue("@adressTypeID", address.AdressTypeID);
            command.Parameters.AddWithValue("@street", address.Street);
            command.Parameters.AddWithValue("@number", address.Number);
            command.Parameters.AddWithValue("@complement", address.Complement);
            command.Parameters.AddWithValue("@city", address.City);
            command.Parameters.AddWithValue("@stateOrDistrict", address.StateOrDistrict);
            command.Parameters.AddWithValue("@postalCode", address.PostalCode);
            command.Parameters.AddWithValue("@country", address.Country);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public Address RetrieveByID(long addressIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[Addresses] where AddressID = @addressID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@addressID", addressIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long addressID = Convert.ToInt64(dataReader["AddressID"]);
            string adressNickname = Convert.ToString(dataReader["AddressNickname"]);
            long adressTypeID = Convert.ToInt64(dataReader["AddressTypeID"]);
            string street = Convert.ToString(dataReader["Street"]);
            string number = Convert.ToString(dataReader["Number"]);
            string complement = Convert.ToString(dataReader["Complement"]);
            string city = Convert.ToString(dataReader["City"]);
            string stateOrDistrict = Convert.ToString(dataReader["StateOrDistrict"]);
            string postalCode = Convert.ToString(dataReader["PostalCode"]);
            string country = Convert.ToString(dataReader["Country"]);
            Address address = new() 
            {
                AddressID = addressID,
                AdressNickname = adressNickname,
                AdressTypeID = adressTypeID,
                Street = street,
                Number = number,
                Complement = complement,
                City = city,
                StateOrDistrict = stateOrDistrict,
                PostalCode = postalCode,
                Country = country
            };

            return address;
        }

        public void Update(Address address, long addressIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[Addresses] SET AddressNickname = @addressNickname, AddressTypeID = @addressTypeID, Street = @street, Number = @number ,Complement = @complement, City = @city, StateOrDistrict = @stateOrDistrict, PostalCode = @postalCode, Country = @country WHERE AddressID = @addressID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@addressID", addressIDToLookFor);
            command.Parameters.AddWithValue("@addressNickname", address.AdressNickname);
            command.Parameters.AddWithValue("@addressTypeID", address.AdressTypeID);
            command.Parameters.AddWithValue("@street", address.Street);
            command.Parameters.AddWithValue("@number", address.Number);
            command.Parameters.AddWithValue("@complement", address.Complement);
            command.Parameters.AddWithValue("@city", address.City);
            command.Parameters.AddWithValue("@stateOrDistrict", address.StateOrDistrict);
            command.Parameters.AddWithValue("@postalCode", address.PostalCode);
            command.Parameters.AddWithValue("@country", address.Country);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(Address address)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[Addresses] WHERE [AddressID] = @addressID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@addressID", address.AddressID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }

    }
}
