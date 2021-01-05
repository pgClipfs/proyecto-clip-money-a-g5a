using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Gestores
{
    public class AccountManager
    {
        private string StrConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static Random random = new Random();

        public Account GetAccountById(int AccountID)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT ID_CUENTA, " +
                "TC.ID_TIPO_CUENTA, " +
                "TC.TIPO_CUENTA, " +
                "DIV.ID_DIVISA, " +
                "DIV.DIVISA, " +
                "DIV.COMISION, " +
                "DIV.PRECIO_COMPRA, " +
                "DIV.PRECIO_VENTA, " +
                "CVU, " +
                "SALDO, " +
                "ALIAS, " +
                "FECHA_APERTURA " +
                "FROM CUENTAS " +
                "INNER JOIN TIPO_CUENTAS TC ON CUENTAS.ID_TIPO_CUENTA = TC.ID_TIPO_CUENTA " +
                "INNER JOIN DIVISAS DIV ON CUENTAS.ID_DIVISA = DIV.ID_DIVISA " +
                "WHERE ID_CUENTA=@IdCuenta";

            comm.Parameters.Add(new SqlParameter("@IdCuenta", AccountID));


            SqlDataReader dr = comm.ExecuteReader();

            Account account = new Account();
            if (dr.Read())
            {
                account.AccountId = dr.GetInt32(0);
                account.TypeAccount = new AccountType() { AccountTypeId = dr.GetByte(1), AccountTypeName = dr.GetString(2) };
                account.Currency = new Currency() { CurrencyId = dr.GetByte(3), CurrencyName = dr.GetString(4), Fee = dr.GetDouble(5), PurchasePrice = dr.GetDecimal(6), SalePrice = dr.GetDecimal(7) };
                account.CVU = dr.GetString(8);
                account.Balance = dr.GetDecimal(9);
                account.Alias = dr[10] as string;
                account.OpeningDate = dr.GetDateTime(11);

            }

            dr.Close();
            conn.Close();

            return account;
        }

        public List<Account> GetUserAccountsByUserId(int UserID)
        {
            User usuario = new User();

            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT ID_CUENTA, " +
                "TC.ID_TIPO_CUENTA, " +
                "TC.TIPO_CUENTA, " +
                "DIV.ID_DIVISA, " +
                "DIV.DIVISA, " +
                "DIV.COMISION, " +
                "DIV.PRECIO_COMPRA, " +
                "DIV.PRECIO_VENTA, " +
                "CVU, " +
                "SALDO, " +
                "ALIAS, " +
                "FECHA_APERTURA " +
                "FROM CUENTAS " +
                "INNER JOIN TIPO_CUENTAS TC ON CUENTAS.ID_TIPO_CUENTA = TC.ID_TIPO_CUENTA " +
                "INNER JOIN DIVISAS DIV ON CUENTAS.ID_DIVISA = DIV.ID_DIVISA " +
                "WHERE ID_USUARIO=@IdUsuario";

            comm.Parameters.Add(new SqlParameter("@IdUsuario", UserID));


            SqlDataReader dr = comm.ExecuteReader();

            List<Account> AccountList = new List<Account>();
            while (dr.Read())
            {
                Account account = new Account();
                account.AccountId = dr.GetInt32(0);
                account.TypeAccount = new AccountType() { AccountTypeId = dr.GetByte(1), AccountTypeName = dr.GetString(2) };
                account.Currency = new Currency() { CurrencyId = dr.GetByte(3), CurrencyName = dr.GetString(4), Fee = dr.GetDouble(5), PurchasePrice = dr.GetDecimal(6), SalePrice = dr.GetDecimal(7) };
                account.CVU = dr.GetString(8);
                account.Balance = dr.GetDecimal(9);
                account.Alias = dr[10] as string;
                account.OpeningDate = dr.GetDateTime(11);

                AccountList.Add(account);
            }

            dr.Close();
            conn.Close();

            return AccountList;

        }

        public Account GetAccountByCVU(string CVU)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT ID_CUENTA, " +
                "TC.ID_TIPO_CUENTA, " +
                "TC.TIPO_CUENTA, " +
                "DIV.ID_DIVISA, " +
                "DIV.DIVISA, " +
                "DIV.COMISION, " +
                "DIV.PRECIO_COMPRA, " +
                "DIV.PRECIO_VENTA, " +
                "USR.ID_USUARIO, " +
                "USR.CUIL, " +
                "USR.NOMBRE, " +
                "USR.APELLIDO, " +
                "USR.EMAIL, " +
                "USR.TELEFONO, " +
                "USR.PRIVILEGIOS, " +
                "CVU, " +
                "SALDO, " +
                "ALIAS, " +
                "FECHA_APERTURA " +
                "FROM CUENTAS " +
                "INNER JOIN TIPO_CUENTAS TC ON CUENTAS.ID_TIPO_CUENTA = TC.ID_TIPO_CUENTA " +
                "INNER JOIN DIVISAS DIV ON CUENTAS.ID_DIVISA = DIV.ID_DIVISA " +
                "INNER JOIN USUARIOS USR ON CUENTAS.ID_USUARIO = USR.ID_USUARIO " +
                "WHERE CVU=@Cvu";

            comm.Parameters.AddWithValue("@Cvu", CVU);

            SqlDataReader dr = comm.ExecuteReader();

            Account oAccount = new Account();
            if (dr.Read())
            {
                oAccount.AccountId = dr.GetInt32(0);
                oAccount.TypeAccount = new AccountType() { AccountTypeId = dr.GetByte(1), AccountTypeName = dr.GetString(2) };
                oAccount.Currency = new Currency() { CurrencyId = dr.GetByte(3), CurrencyName = dr.GetString(4), Fee = dr.GetDouble(5), PurchasePrice = dr.GetDecimal(6), SalePrice = dr.GetDecimal(7) };
                oAccount.User = new User() { UserId = dr.GetInt32(8), Cuil = dr.GetString(9), Name = dr.GetString(10), Surname = dr.GetString(11), Email = dr.GetString(12), PhoneNumber = dr.GetString(13), Privileges = dr.GetString(14) };
                oAccount.CVU = dr.GetString(16);
                oAccount.Balance = dr.GetDecimal(17);
                oAccount.Alias = dr[18] as string; ;
                oAccount.OpeningDate = dr.GetDateTime(19);

            }

            dr.Close();
            conn.Close();

            return oAccount;
        }

        public void CreateNewAccount(int TypeAccountId, int CurrencyId, int UserId, decimal Balance, string Alias)
        {
            SqlConnection oSql = new SqlConnection(StrConn);
            oSql.Open();

            SqlCommand oCommand = oSql.CreateCommand();

            oCommand.CommandText = "INSERT INTO CUENTAS( " +
                "ID_TIPO_CUENTA, " +
                "ID_DIVISA, " +
                "ID_USUARIO, " +
                "CVU, " +
                "SALDO, " +
                "ALIAS, " +
                "FECHA_APERTURA) " +
                "VALUES(" +
                "@TypeAccountId, " +
                "@CurrencyId, " +
                "@UserId, " +
                "@Cvu, " +
                "@Balance, " +
                "@Alias," +
                "GETDATE() )";

            oCommand.Parameters.AddWithValue("@TypeAccountId", TypeAccountId);
            oCommand.Parameters.AddWithValue("@CurrencyId", CurrencyId);
            oCommand.Parameters.AddWithValue("@UserId", UserId);
            oCommand.Parameters.AddWithValue("@Cvu", RandomDigits(22));
            oCommand.Parameters.AddWithValue("@Balance", Balance);
            oCommand.Parameters.AddWithValue("@Alias", (object)Alias ?? DBNull.Value);

            oCommand.ExecuteNonQuery();

            oSql.Close();
        }

        public void UpdateAccountBalance(int accountID, decimal amount)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "UPDATE CUENTAS " +
                "SET SALDO=(SELECT SALDO FROM CUENTAS WHERE ID_CUENTA=@IdCuenta)+@Monto " +
                "WHERE ID_CUENTA=@IdCuenta";

            comm.Parameters.Add(new SqlParameter("@IdCuenta", accountID));
            comm.Parameters.Add(new SqlParameter("@Monto", amount));

            comm.ExecuteNonQuery();
            conn.Close();
        }

        public string RandomDigits(int length)
        {
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

    }
}