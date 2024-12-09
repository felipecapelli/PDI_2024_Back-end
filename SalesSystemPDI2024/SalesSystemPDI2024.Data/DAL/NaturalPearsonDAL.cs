using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class NaturalPearsonDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[NaturalPeople]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<NaturalPearson> List()
        {
            var lista = new List<NaturalPearson>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[NaturalPeople] ORDER BY CustomersSuppliersID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
                string identityCard = Convert.ToString(dataReader["IdentityCard"]);
                DateTime dateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"]);
                string occupation = Convert.ToString(dataReader["Occupation"]);

                NaturalPearson naturalPearson = new()
                {
                    CustomersSuppliersID = customersSuppliersID,
                    IdentityCard = identityCard,
                    DateOfBirth = dateOfBirth,
                    Occupation = occupation
                };

                lista.Add(naturalPearson);
            }

            return lista;
        }

        public void Add(NaturalPearson naturalPearson)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[NaturalPeople] values (@identityCard, @dateOfBirth, @occupation);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@identityCard", naturalPearson.IdentityCard);
            command.Parameters.AddWithValue("@dateOfBirth", naturalPearson.DateOfBirth);
            command.Parameters.AddWithValue("@occupation", naturalPearson.Occupation);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public NaturalPearson RetrieveByID(long customersSuppliersIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[NaturalPeople] where CustomersSuppliersID = @customersSuppliersIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@customersSuppliersIDToLookFor", customersSuppliersIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
            string identityCard = Convert.ToString(dataReader["IdentityCard"]);
            DateTime dateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"]);
            string occupation = Convert.ToString(dataReader["Occupation"]);

            NaturalPearson naturalPearson = new()
            {
                CustomersSuppliersID = customersSuppliersID,
                IdentityCard = identityCard,
                DateOfBirth = dateOfBirth,
                Occupation = occupation
            };

            return naturalPearson;
        }

        public void Update(NaturalPearson naturalPearson, long addressIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[NaturalPeople] SET IdentityCard = @identityCard, DateOfBirth = @dateOfBirth, Occupation = @occupation WHERE CustomersSuppliersID = @addressIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@addressIDToLookFor", addressIDToLookFor);
            command.Parameters.AddWithValue("@identityCard", naturalPearson.IdentityCard);
            command.Parameters.AddWithValue("@dateOfBirth", naturalPearson.DateOfBirth);
            command.Parameters.AddWithValue("@occupation", naturalPearson.Occupation);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(NaturalPearson naturalPearson)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[NaturalPeople] WHERE [CustomersSuppliersID] = @CustomersSuppliersID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CustomersSuppliersID", naturalPearson.CustomersSuppliersID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
