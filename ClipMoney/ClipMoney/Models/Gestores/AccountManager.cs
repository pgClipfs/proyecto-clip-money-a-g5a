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

    }
}