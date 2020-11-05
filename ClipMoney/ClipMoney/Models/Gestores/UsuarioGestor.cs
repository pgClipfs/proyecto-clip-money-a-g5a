using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClipMoney.Models
{

    public class UsuarioGestor
    {
        private const string StrConn = "Server=192.168.0.27;Database=ClipMoney;User Id = Administrador; Password=12345";

        public Usuario ObtenerPorCUILPassword(string Cuil, string Password)
        {
            Usuario usuario = new Usuario();

            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();

            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM USUARIOS WHERE CUIL=@Cuil AND CONTRASEÑA=@Password";
            comm.Parameters.Add(new SqlParameter("@Cuil", Cuil));
            comm.Parameters.Add(new SqlParameter("@Password", Password));

            SqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                int id = dr.GetInt32(0);
                string nombre = dr.GetString(2);
                string apellido = dr.GetString(3);

               usuario = new Usuario(id, nombre, apellido);
            }

            dr.Close();
            conn.Close();

            return usuario;
        }

        }
}