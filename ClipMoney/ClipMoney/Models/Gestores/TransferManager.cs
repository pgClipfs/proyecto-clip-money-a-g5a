using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Gestores
{
    public class TransferManager
    {
        private string StrConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public bool MakeTransfer(Account DebitAccount, int IdCreditAccount, decimal Amount, string concept)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "INSERT INTO TRANSFERENCIAS(ID_CUENTA_ORIGEN, ID_CUENTA_DESTINO, ID_DIVISA, FECHA, CONCEPTO, MONTO) " +
                "VALUES(@IdCuentaOrigen, @IdCuentaDestino, @IdDivisa, GETDATE(), @Concepto, @Monto) ";


            comm.Parameters.Add(new SqlParameter("@IdCuentaOrigen", DebitAccount.AccountId));
            comm.Parameters.Add(new SqlParameter("@IdCuentaDestino", IdCreditAccount));
            comm.Parameters.Add(new SqlParameter("@IdDivisa", DebitAccount.Currency.CurrencyId));
            comm.Parameters.Add(new SqlParameter("@Monto", Amount));
            comm.Parameters.Add(new SqlParameter("@Concepto", concept));


            int Rows = comm.ExecuteNonQuery();

            conn.Close();

            return Rows > 0;
        }
    }
}