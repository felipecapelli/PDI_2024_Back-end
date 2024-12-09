using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class CorporateEntityDAL
    {
        public long GetLastID()
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT IDENT_CURRENT('[SalesSystemPDI2024].[dbo].[CorporateEntities]') AS ID";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();
            long ID = Convert.ToInt64(dataReader["ID"]);

            return ID;
        }

        public IEnumerable<CorporateEntity> List()
        {
            var lista = new List<CorporateEntity>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[CorporateEntities] ORDER BY CustomersSuppliersID DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
                DateTime foundationDate = Convert.ToDateTime(dataReader["FoundationDate"]);
                string businessKindDescription = Convert.ToString(dataReader["BusinessKindDescription"]);
                long legalRepresentativeFiscalID = Convert.ToInt64(dataReader["LegalRepresentativeFiscalID"]);

                CorporateEntity corporateEntity = new()
                {
                    CustomersSuppliersID = customersSuppliersID,
                    FoundationDate = foundationDate,
                    BusinessKindDescription = businessKindDescription,
                    LegalRepresentativeFiscalID = legalRepresentativeFiscalID
                };

                lista.Add(corporateEntity);
            }

            return lista;
        }

        public void Add(CorporateEntity corporateEntity)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[CorporateEntities] values (@foundationDate, @businessKindDescription, @legalRepresentativeFiscalID);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@foundationDate", corporateEntity.FoundationDate);
            command.Parameters.AddWithValue("@businessKindDescription", corporateEntity.BusinessKindDescription);
            command.Parameters.AddWithValue("@legalRepresentativeFiscalID", corporateEntity.LegalRepresentativeFiscalID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public CorporateEntity RetrieveByID(long customersSuppliersIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[CorporateEntities] where CustomersSuppliersID = @customersSuppliersIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@customersSuppliersIDToLookFor", customersSuppliersIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
            DateTime foundationDate = Convert.ToDateTime(dataReader["FoundationDate"]);
            string businessKindDescription = Convert.ToString(dataReader["BusinessKindDescription"]);
            long legalRepresentativeFiscalID = Convert.ToInt64(dataReader["LegalRepresentativeFiscalID"]);

            CorporateEntity corporateEntity = new()
            {
                CustomersSuppliersID = customersSuppliersID,
                FoundationDate = foundationDate,
                BusinessKindDescription = businessKindDescription,
                LegalRepresentativeFiscalID = legalRepresentativeFiscalID
            };

            return corporateEntity;
        }

        public void Update(CorporateEntity corporateEntity, long customersSuppliersIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[CorporateEntities] SET FoundationDate = @FoundationDate, BusinessKindDescription = @businessKindDescription, LegalRepresentativeFiscalID = @legalRepresentativeFiscalID WHERE CustomersSuppliersID = @customersSuppliersIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@customersSuppliersIDToLookFor", customersSuppliersIDToLookFor);
            command.Parameters.AddWithValue("@foundationDate", corporateEntity.FoundationDate);
            command.Parameters.AddWithValue("@businessKindDescription", corporateEntity.BusinessKindDescription);
            command.Parameters.AddWithValue("@legalRepresentativeFiscalID", corporateEntity.LegalRepresentativeFiscalID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(CorporateEntity corporateEntity)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[CorporateEntities] WHERE [CustomersSuppliersID] = @customersSuppliersID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@customersSuppliersID", corporateEntity.CustomersSuppliersID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
