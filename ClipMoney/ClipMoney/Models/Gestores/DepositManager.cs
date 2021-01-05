using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Gestores
{
    public class DepositManager
    {
        private string StrConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void DepositWithCreditCard(decimal Amount, int AccountId, string FullName, string LastNumbersCard, int DocumentNumber, string CardExpiration)
        {
            SqlConnection conn = new SqlConnection(StrConn);

            conn.Open();

            SqlCommand comm = conn.CreateCommand();

            comm.CommandText = "" +
                "INSERT INTO DEPOSITOS(" +
                "FEC_DEPOSITO, " +
                "TIPO_DEPOSITO, " +
                "MONTO, " +
                "ID_CUENTA, " +
                "TAR_NOM_COMPLETO, " +
                "TAR_NUM, " +
                "TAR_NUM_DOCUMENTO, " +
                "TAR_FEC_VEN ) " +
                "VALUES(" +
                "GETDATE(), " +
                "'TARJETA', " +
                "@Amount, " +
                "@AccountId, " +
                "@FullName, " +
                "@CardNum, " +
                "@DocumentNumber, " +
                "@CardExpiration " +
                ")";

            comm.Parameters.AddWithValue("@Amount", Amount);
            comm.Parameters.AddWithValue("@AccountId", AccountId);
            comm.Parameters.AddWithValue("@FullName", FullName);
            comm.Parameters.AddWithValue("@CardNum", LastNumbersCard);
            comm.Parameters.AddWithValue("@DocumentNumber", DocumentNumber);
            comm.Parameters.AddWithValue("@CardExpiration", CardExpiration);

            comm.ExecuteNonQuery();

            conn.Close();

        }
    }
}