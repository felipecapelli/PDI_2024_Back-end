using Microsoft.Data.SqlClient;
using SalesSystemPDI2024.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Data.DAL
{
    public class CustomersSupplierDAL
    {
        public IEnumerable<CustomersSupplier> List()
        {
            var lista = new List<CustomersSupplier>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM [SalesSystemPDI2024].[dbo].[CustomersSuppliers]";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
                string fiscalID = Convert.ToString(dataReader["FiscalID"]);
                string name = Convert.ToString(dataReader["Name"]);
                string email = Convert.ToString(dataReader["Email"]);
                string phone = Convert.ToString(dataReader["Phone"]);
                bool isCustomer = Convert.ToBoolean(dataReader["IsCustomer"]);
                bool isSupplier = Convert.ToBoolean(dataReader["IsSupplier"]);
                bool isCustomerRepresentative = Convert.ToBoolean(dataReader["IsCustomerRepresentative"]);
                bool isSuplierRepresentative = Convert.ToBoolean(dataReader["IsSuplierRepresentative"]);
                bool isParent = Convert.ToBoolean(dataReader["IsParent"]);
                bool isSubsidiary = Convert.ToBoolean(dataReader["IsSubsidiary"]);
                bool isParentRepresentative = Convert.ToBoolean(dataReader["IsParentRepresentative"]);
                bool isSubsidiaryRepresentative = Convert.ToBoolean(dataReader["IsSubsidiaryRepresentative"]);

                CustomersSupplier customersSupplier = new()
                {
                    CustomersSuppliersID = customersSuppliersID,
                    FiscalID = fiscalID,
                    Name = name,
                    Email = email,
                    Phone = phone,
                    IsCustomer = isCustomer,
                    IsSupplier = isSupplier,
                    IsCustomerRepresentative = isCustomerRepresentative,
                    IsSuplierRepresentative = isSuplierRepresentative,
                    IsParent = isParent,
                    IsSubsidiary = isSubsidiary,
                    IsParentRepresentative = isParentRepresentative,
                    IsSubsidiaryRepresentative = isSubsidiaryRepresentative
                };

                lista.Add(customersSupplier);
            }

            return lista;
        }

        public void Add(CustomersSupplier customersSupplier)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "insert into [SalesSystemPDI2024].[dbo].[CustomersSuppliers] values (@fiscalID, @name, @email, @phone, @isCustomer, @isSupplier, @isCustomerRepresentative, @isSuplierRepresentative, @isParent, @isSubsidiary, @isParentRepresentative, @isSubsidiaryRepresentative);";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@fiscalID", customersSupplier.FiscalID);
            command.Parameters.AddWithValue("@name", customersSupplier.Name);
            command.Parameters.AddWithValue("@email", customersSupplier.Email);
            command.Parameters.AddWithValue("@phone", customersSupplier.Phone);
            command.Parameters.AddWithValue("@isCustomer", customersSupplier.IsCustomer);
            command.Parameters.AddWithValue("@isSupplier", customersSupplier.IsSupplier);
            command.Parameters.AddWithValue("@isCustomerRepresentative", customersSupplier.IsCustomerRepresentative);
            command.Parameters.AddWithValue("@isSuplierRepresentative", customersSupplier.IsSuplierRepresentative);
            command.Parameters.AddWithValue("@isParent", customersSupplier.IsParent);
            command.Parameters.AddWithValue("@isSubsidiary", customersSupplier.IsSubsidiary);
            command.Parameters.AddWithValue("@isParentRepresentative", customersSupplier.IsParentRepresentative);
            command.Parameters.AddWithValue("@isSubsidiaryRepresentative", customersSupplier.IsSubsidiaryRepresentative);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Adicionar {retorno}");
        }

        public CustomersSupplier RetrieveByID(long customersSuppliersIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"SELECT * FROM [SalesSystemPDI2024].[dbo].[CustomersSuppliers] where CustomersSuppliersID = @customersSuppliersIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@customersSuppliersIDToLookFor", customersSuppliersIDToLookFor);

            using SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows) return null;

            dataReader.Read();
            long customersSuppliersID = Convert.ToInt64(dataReader["CustomersSuppliersID"]);
            string fiscalID = Convert.ToString(dataReader["FiscalID"]);
            string name = Convert.ToString(dataReader["Name"]);
            string email = Convert.ToString(dataReader["Email"]);
            string phone = Convert.ToString(dataReader["Phone"]);
            bool isCustomer = Convert.ToBoolean(dataReader["IsCustomer"]);
            bool isSupplier = Convert.ToBoolean(dataReader["IsSupplier"]);
            bool isCustomerRepresentative = Convert.ToBoolean(dataReader["IsCustomerRepresentative"]);
            bool isSuplierRepresentative = Convert.ToBoolean(dataReader["IsSuplierRepresentative"]);
            bool isParent = Convert.ToBoolean(dataReader["IsParent"]);
            bool isSubsidiary = Convert.ToBoolean(dataReader["IsSubsidiary"]);
            bool isParentRepresentative = Convert.ToBoolean(dataReader["IsParentRepresentative"]);
            bool isSubsidiaryRepresentative = Convert.ToBoolean(dataReader["IsSubsidiaryRepresentative"]);

            CustomersSupplier customersSupplier = new()
            {
                CustomersSuppliersID = customersSuppliersID,
                FiscalID = fiscalID,
                Name = name,
                Email = email,
                Phone = phone,
                IsCustomer = isCustomer,
                IsSupplier = isSupplier,
                IsCustomerRepresentative = isCustomerRepresentative,
                IsSuplierRepresentative = isSuplierRepresentative,
                IsParent = isParent,
                IsSubsidiary = isSubsidiary,
                IsParentRepresentative = isParentRepresentative,
                IsSubsidiaryRepresentative = isSubsidiaryRepresentative
            };

            return customersSupplier;
        }

        public void Update(CustomersSupplier customersSupplier, long customersSuppliersIDToLookFor)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"UPDATE [SalesSystemPDI2024].[dbo].[CustomersSuppliers] SET FiscalID = @fiscalID, Name = @name, Email = @email, Phone = @phone, IsCustomer = @isCustomer, IsSupplier = @isSupplier, IsCustomerRepresentative = @isCustomerRepresentative, IsSuplierRepresentative = @isSuplierRepresentative, IsParent = @isParent, IsSubsidiary = @isSubsidiary, IsParentRepresentative = @isParentRepresentative, IsSubsidiaryRepresentative = @isSubsidiaryRepresentative WHERE CustomersSuppliersID = @customersSuppliersIDToLookFor";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@customersSuppliersIDToLookFor", customersSuppliersIDToLookFor);
            command.Parameters.AddWithValue("@fiscalID", customersSupplier.FiscalID);
            command.Parameters.AddWithValue("@name", customersSupplier.Name);
            command.Parameters.AddWithValue("@email", customersSupplier.Email);
            command.Parameters.AddWithValue("@phone", customersSupplier.Phone);
            command.Parameters.AddWithValue("@isCustomer", customersSupplier.IsCustomer);
            command.Parameters.AddWithValue("@isSupplier", customersSupplier.IsSupplier);
            command.Parameters.AddWithValue("@isCustomerRepresentative", customersSupplier.IsCustomerRepresentative);
            command.Parameters.AddWithValue("@isSuplierRepresentative", customersSupplier.IsSuplierRepresentative);
            command.Parameters.AddWithValue("@isParent", customersSupplier.IsParent);
            command.Parameters.AddWithValue("@isSubsidiary", customersSupplier.IsSubsidiary);
            command.Parameters.AddWithValue("@isParentRepresentative", customersSupplier.IsParentRepresentative);
            command.Parameters.AddWithValue("@isSubsidiaryRepresentative", customersSupplier.IsSubsidiaryRepresentative);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Atualizar {retorno}");

        }

        public void Delete(CustomersSupplier customersSupplier)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = $"DELETE FROM [SalesSystemPDI2024].[dbo].[CustomersSuppliers] WHERE [CustomersSuppliersID] = @customersSuppliersID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@customersSuppliersID", customersSupplier.CustomersSuppliersID);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas Excluir {retorno}");
        }
    }
}
