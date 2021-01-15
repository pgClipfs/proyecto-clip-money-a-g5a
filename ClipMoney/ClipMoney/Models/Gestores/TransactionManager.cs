using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClipMoney.Models.Gestores
{
    public class TransactionManager
    {
        public string StrConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<Transactions> GetTransactions(string UserId)
        {
            SqlConnection oSql = new SqlConnection(StrConn);

            oSql.Open();

            SqlCommand oCommand = oSql.CreateCommand();

            oCommand.CommandText = @"
                DROP TABLE IF EXISTS TMP_TRANSACCIONES 
                SELECT 
                'TRANSFERENCIA' AS TIPO_TRANSACCION,
                FECHA,
                CUENTAS.CVU AS CUENTA,
                MONTO,
                CONCEPTO,
                ID_TRANSFERENCIA AS NUMERO_COMPROBANTE
                INTO TMP_TRANSACCIONES FROM TRANSFERENCIAS
                INNER JOIN CUENTAS ON ID_CUENTA_ORIGEN = CUENTAS.ID_CUENTA
                INNER JOIN USUARIOS USR ON CUENTAS.ID_USUARIO = USR.ID_USUARIO
                WHERE USR.ID_USUARIO=@UserId

                INSERT INTO TMP_TRANSACCIONES(TIPO_TRANSACCION, FECHA, CUENTA, MONTO, CONCEPTO, NUMERO_COMPROBANTE)
                SELECT 'DEPOSITO' AS TIPO_TRANSACCION, FEC_DEPOSITO AS FECHA, CUENTAS.CVU, MONTO, '' AS CONCEPTO, ID_DEPOSITO AS NUMERO_COMRPOBANTE
                FROM DEPOSITOS
                INNER JOIN CUENTAS ON DEPOSITOS.ID_CUENTA = CUENTAS.ID_CUENTA
                WHERE CUENTAS.ID_USUARIO=@UserId


                SELECT TOP 10 TIPO_TRANSACCION, FECHA, CUENTA, MONTO, CONCEPTO, NUMERO_COMPROBANTE FROM TMP_TRANSACCIONES ORDER BY FECHA DESC ;
            ";

            oCommand.Parameters.AddWithValue("@UserId", UserId);

            SqlDataReader oReader = oCommand.ExecuteReader();

            List<Transactions> oTransactions = new List<Transactions>();

            while (oReader.Read())
            {
                Transactions oTransaction = new Transactions();

                oTransaction.TransactionType = oReader.GetString(0);
                oTransaction.DateTime = oReader.GetDateTime(1);
                oTransaction.Account = oReader.GetString(2);
                oTransaction.Amount = oReader.GetDecimal(3);
                oTransaction.Concept = oReader.GetString(4);
                oTransaction.VoucherNumber = oReader.GetInt32(5);

                oTransactions.Add(oTransaction);
            }

            oSql.Close();

            return oTransactions;
        }
    }
}