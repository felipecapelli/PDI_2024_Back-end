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
        public IEnumerable<Address> Listar()
        {
            var lista = new List<Address>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[address]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string adressNickname = Convert.ToString(dataReader["AdressNickname"]);
                string adressType = Convert.ToString(dataReader["AdressType"]);
                string street = Convert.ToString(dataReader["Street"]);
                string number = Convert.ToString(dataReader["Number"]);
                string city = Convert.ToString(dataReader["City"]);
                string stateOrDistrict = Convert.ToString(dataReader["StateOrDistrict"]);
                string postalCode = Convert.ToString(dataReader["PostalCode"]);
                string country = Convert.ToString(dataReader["Country"]);

                Address address = new() 
                {
                    AdressNickname = adressNickname,
                    AdressType = adressType,
                    Street = street,
                    Number = number,
                    City = city,
                    StateOrDistrict = stateOrDistrict,
                    PostalCode = postalCode,
                    Country = country
                };

                lista.Add(address);
            }

            return lista;
        }

        public void Adicionar(Address address)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[address] values (@adressNickname, @adressType, @street, @number, @city, @stateOrDistrict, @postalCode, @country);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@adressNickname", address.AdressNickname);
            command.Parameters.AddWithValue("@adressType", address.AdressType);
            command.Parameters.AddWithValue("@street", address.Street);
            command.Parameters.AddWithValue("@number", address.Number);
            command.Parameters.AddWithValue("@city", address.City);
            command.Parameters.AddWithValue("@stateOrDistrict", address.StateOrDistrict);
            command.Parameters.AddWithValue("@postalCode", address.PostalCode);
            command.Parameters.AddWithValue("@country", address.Country);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public Address Buscar(string enderecoParaBuscar)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[address] where AdressNickname = @adressNickname";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@adressNickname", enderecoParaBuscar);

            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            string adressNickname = Convert.ToString(dataReader["AdressNickname"]);
            string adressType = Convert.ToString(dataReader["AdressType"]);
            string street = Convert.ToString(dataReader["Street"]);
            string number = Convert.ToString(dataReader["Number"]);
            string city = Convert.ToString(dataReader["City"]);
            string stateOrDistrict = Convert.ToString(dataReader["StateOrDistrict"]);
            string postalCode = Convert.ToString(dataReader["PostalCode"]);
            string country = Convert.ToString(dataReader["Country"]);
            Address address = new() 
            {
                AdressNickname = adressNickname,
                AdressType = adressType,
                Street = street,
                Number = number,
                City = city,
                StateOrDistrict = stateOrDistrict,
                PostalCode = postalCode,
                Country = country
            };

            return address;
        }

        public void Atualizar(Address address)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[address] SET AdressNickname = @adressNickname, AdressType = @adressType, Street = @street, Number = @number, City = @city, StateOrDistrict = @stateOrDistrict, PostalCode = @postalCode, Country = @country WHERE AdressNickname = @adressNickname";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@adressNickname", address.AdressNickname);
            command.Parameters.AddWithValue("@adressType", address.AdressType);
            command.Parameters.AddWithValue("@street", address.Street);
            command.Parameters.AddWithValue("@number", address.Number);
            command.Parameters.AddWithValue("@city", address.City);
            command.Parameters.AddWithValue("@stateOrDistrict", address.StateOrDistrict);
            command.Parameters.AddWithValue("@postalCode", address.PostalCode);
            command.Parameters.AddWithValue("@country", address.Country);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Excluir(Address address)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[address] WHERE AdressNickname = @adressNickname";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@adressNickname", address.AdressNickname);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }

    }
}
