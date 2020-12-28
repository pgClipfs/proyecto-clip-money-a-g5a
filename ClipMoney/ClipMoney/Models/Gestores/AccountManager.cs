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

        public Cuenta GetAccountById(int AccountID)
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

            Cuenta account = new Cuenta();
            if (dr.Read())
            {
                account.IdCuenta = dr.GetInt32(0);
                account.TipoCuenta = new TipoCuentas() { IdTipoCuenta = dr.GetByte(1), TipoCuenta = dr.GetString(2) };
                account.Divisa = new Divisas() { IdDivisa = dr.GetByte(3), Divisa = dr.GetString(4), Fee = dr.GetDouble(5), PurchasePrice = dr.GetDecimal(6), SalePrice = dr.GetDecimal(7) };
                account.CVU = dr.GetString(8);
                account.Saldo = dr.GetDecimal(9);
                account.Alias = dr.GetString(10);
                account.OpeningDate = dr.GetDateTime(11);

            }

            dr.Close();
            conn.Close();

            return account;
        }

        public List<Cuenta> GetUserAccountsByUserId(int UserID)
        {
            Usuarios usuario = new Usuarios();

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

            List<Cuenta> AccountList = new List<Cuenta>();
            while (dr.Read())
            {
                Cuenta account = new Cuenta();
                account.IdCuenta = dr.GetInt32(0);
                account.TipoCuenta = new TipoCuentas() { IdTipoCuenta = dr.GetByte(1), TipoCuenta = dr.GetString(2) };
                account.Divisa = new Divisas() { IdDivisa = dr.GetByte(3), Divisa = dr.GetString(4), Fee = dr.GetDouble(5), PurchasePrice = dr.GetDecimal(6), SalePrice = dr.GetDecimal(7) };
                account.CVU = dr.GetString(8);
                account.Saldo = dr.GetDecimal(9);
                account.Alias = dr.GetString(10);
                account.OpeningDate = dr.GetDateTime(11);

                AccountList.Add(account);
            }

            dr.Close();
            conn.Close();

            return AccountList;

        }

        public Cuenta GetAccountByCVU(string CVU)
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
                "USR.ID_SITUACION_CREDITICIA, " +
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

            Cuenta oAccount = new Cuenta();
            if (dr.Read())
            {
                oAccount.IdCuenta = dr.GetInt32(0);
                oAccount.TipoCuenta = new TipoCuentas() { IdTipoCuenta = dr.GetByte(1), TipoCuenta = dr.GetString(2) };
                oAccount.Divisa = new Divisas() { IdDivisa = dr.GetByte(3), Divisa = dr.GetString(4), Fee = dr.GetDouble(5), PurchasePrice = dr.GetDecimal(6), SalePrice = dr.GetDecimal(7) };
                oAccount.Usuario = new Usuarios() { IdUsuario = dr.GetInt32(8), Cuil = dr.GetString(9), Nombre = dr.GetString(10), Apellido = dr.GetString(11), Email = dr.GetString(12), Telefono = dr.GetString(13), IdSituacionCrediticia = dr.GetByte(14), Privilegios = dr.GetString(15) };
                oAccount.CVU = dr.GetString(16);
                oAccount.Saldo = dr.GetDecimal(17);
                oAccount.Alias = dr.GetString(18);
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