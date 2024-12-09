using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class BaseCurrencyDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[BaseCurrencies]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<BaseCurrency> List()
        {
            var lista = new List<BaseCurrency>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[BaseCurrencies] ORDER BY BaseCurrencyID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long baseCurrencyID = Convert.ToInt64(dataReader["BaseCurrencyID"]);
                string currencyName = Convert.ToString(dataReader["CurrencyName"]);
                string currencyAbbreviation = Convert.ToString(dataReader["CurrencyAbbreviation"]);

                BaseCurrency baseCurrency = new()
                {
                    BaseCurrencyID = baseCurrencyID,
                    CurrencyName = currencyName,
                    CurrencyAbbreviation = currencyAbbreviation
                };

                lista.Add(baseCurrency);
            }

            return lista;
        }

        public void Add(BaseCurrency baseCurrency)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[BaseCurrencies] values (@currencyName, @currencyAbbreviation);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@currencyName", baseCurrency.CurrencyName);
            command.Parameters.AddWithValue("@currencyAbbreviation", baseCurrency.CurrencyAbbreviation);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public BaseCurrency RetrieveByID(long baseCurrencyIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[BaseCurrencies] where BaseCurrencyID = @baseCurrencyIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@baseCurrencyIDToLookFor", baseCurrencyIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long baseCurrencyID = Convert.ToInt64(dataReader["BaseCurrencyID"]);
            string currencyName = Convert.ToString(dataReader["CurrencyName"]);
            string currencyAbbreviation = Convert.ToString(dataReader["CurrencyAbbreviation"]);

            BaseCurrency baseCurrency = new()
            {
                BaseCurrencyID = baseCurrencyID,
                CurrencyName = currencyName,
                CurrencyAbbreviation = currencyAbbreviation
            };

            return baseCurrency;
        }

        public void Update(BaseCurrency baseCurrency, long baseCurrencyIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[BaseCurrencies] SET CurrencyName = @currencyName, CurrencyAbbreviation = @currencyAbbreviation WHERE BaseCurrencyID = @baseCurrencyIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@currencyName", baseCurrencyIDToLookFor);
            command.Parameters.AddWithValue("@currencyAbbreviation", baseCurrency.CurrencyAbbreviation);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(BaseCurrency baseCurrency)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[BaseCurrencies] WHERE [BaseCurrencyID] = @baseCurrencyID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@baseCurrencyID", baseCurrency.BaseCurrencyID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
