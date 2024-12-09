using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class CurrencyQuotationDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[CurrencyQuotations]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<CurrencyQuotation> List()
        {
            var lista = new List<CurrencyQuotation>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[CurrencyQuotations] ORDER BY CurrencyQuotationID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long currencyQuotationID = Convert.ToInt64(dataReader["CurrencyQuotationID"]);
                string currencyName = Convert.ToString(dataReader["CurrencyName"]);
                string curencyAbbreviation = Convert.ToString(dataReader["CurencyAbbreviation"]);
                long baseCurrencyID = Convert.ToInt64(dataReader["BaseCurrencyID"]);
                decimal quantityOfBaseCurrency = Convert.ToDecimal(dataReader["QuantityOfBaseCurrency"]);
                DateTime quotationDate = Convert.ToDateTime(dataReader["QuotationDate"]);

                CurrencyQuotation currencyQuotation = new()
                {
                    CurrencyQuotationID = currencyQuotationID,
                    CurrencyName = currencyName,
                    CurencyAbbreviation = curencyAbbreviation,
                    BaseCurrencyID = baseCurrencyID,
                    QuantityOfBaseCurrency = quantityOfBaseCurrency,
                    QuotationDate = quotationDate
                };

                lista.Add(currencyQuotation);
            }

            return lista;
        }

        public void Add(CurrencyQuotation currencyQuotation)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[CurrencyQuotations] values (@currencyName, @curencyAbbreviation, @baseCurrencyID, @quantityOfBaseCurrency, @quotationDate);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@currencyName", currencyQuotation.CurrencyName);
            command.Parameters.AddWithValue("@curencyAbbreviation", currencyQuotation.CurencyAbbreviation);
            command.Parameters.AddWithValue("@baseCurrencyID", currencyQuotation.BaseCurrencyID);
            command.Parameters.AddWithValue("@quantityOfBaseCurrency", currencyQuotation.QuantityOfBaseCurrency);
            command.Parameters.AddWithValue("@quotationDate", currencyQuotation.QuotationDate);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public CurrencyQuotation RetrieveByID(long currencyQuotationIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[CurrencyQuotations] where CurrencyQuotationID = @currencyQuotationIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@currencyQuotationIDToLookFor", currencyQuotationIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long currencyQuotationID = Convert.ToInt64(dataReader["CurrencyQuotationID"]);
            string currencyName = Convert.ToString(dataReader["CurrencyName"]);
            string curencyAbbreviation = Convert.ToString(dataReader["CurencyAbbreviation"]);
            long baseCurrencyID = Convert.ToInt64(dataReader["BaseCurrencyID"]);
            decimal quantityOfBaseCurrency = Convert.ToDecimal(dataReader["QuantityOfBaseCurrency"]);
            DateTime quotationDate = Convert.ToDateTime(dataReader["QuotationDate"]);

            CurrencyQuotation currencyQuotation = new()
            {
                CurrencyQuotationID = currencyQuotationID,
                CurrencyName = currencyName,
                CurencyAbbreviation = curencyAbbreviation,
                BaseCurrencyID = baseCurrencyID,
                QuantityOfBaseCurrency = quantityOfBaseCurrency,
                QuotationDate = quotationDate
            };

            return currencyQuotation;
        }

        public void Update(CurrencyQuotation currencyQuotation, long currencyQuotationIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[CurrencyQuotations] SET CurrencyQuotationID = @currencyQuotationID, CurrencyName = @currencyName, CurencyAbbreviation = @curencyAbbreviation, BaseCurrencyID = @baseCurrencyID , QuantityOfBaseCurrency = @quantityOfBaseCurrency, QuotationDate = @quotationDate WHERE CurrencyQuotationID = @currencyQuotationID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@currencyQuotationID", currencyQuotationIDToLookFor);
            command.Parameters.AddWithValue("@currencyName", currencyQuotation.CurrencyName);
            command.Parameters.AddWithValue("@curencyAbbreviation", currencyQuotation.CurencyAbbreviation);
            command.Parameters.AddWithValue("@baseCurrencyID", currencyQuotation.BaseCurrencyID);
            command.Parameters.AddWithValue("@quantityOfBaseCurrency", currencyQuotation.QuantityOfBaseCurrency);
            command.Parameters.AddWithValue("@quotationDate", currencyQuotation.QuotationDate);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(CurrencyQuotation currencyQuotation)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[CurrencyQuotations] WHERE [CurrencyQuotationID] = @currencyQuotationID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@currencyQuotationID", currencyQuotation.CurrencyQuotationID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
