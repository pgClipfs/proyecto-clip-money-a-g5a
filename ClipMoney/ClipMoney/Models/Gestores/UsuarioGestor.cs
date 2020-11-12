using ClipMoney.Controllers;
using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClipMoney.Models
{

    public class UsuarioGestor
    {
        private string StrConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public Usuario ObtenerPorCUILPassword(string Cuil, string Password)
        {
            Usuario usuario = new Usuario();

            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();

            string encryptedPassword = ServicioEncriptador.ComputeSha256Hash(Password);

            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM USUARIOS WHERE CUIL=@Cuil AND CONTRASEÑA=@Password";
            comm.Parameters.Add(new SqlParameter("@Cuil", Cuil));
            comm.Parameters.Add(new SqlParameter("@Password", encryptedPassword));

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

        public Usuario BuscarPersonaPorCuil(string cuil)
        {
            Usuario usuario = new Usuario();

            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM USUARIOS WHERE CUIL=@Cuil";
            comm.Parameters.Add(new SqlParameter("@Cuil", cuil));

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

        public bool RegistrarUsuario(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();

            string encryptedPassword = ServicioEncriptador.ComputeSha256Hash(usuario.Contraseña);

            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = @"INSERT INTO USUARIOS(CUIL, NOMBRE, APELLIDO, CONTRASEÑA, EMAIL, TELEFONO, ID_SITUACION_CREDITICIA) 
                                values(@Cuil, @Nombre, @Apellido, @Contraseña, @Email, @Telefono, @IdSituacion)";

            comm.Parameters.Add(new SqlParameter("@Cuil", usuario.Cuil));
            comm.Parameters.Add(new SqlParameter("@Nombre", usuario.Nombre));
            comm.Parameters.Add(new SqlParameter("@Apellido", usuario.Apellido));
            comm.Parameters.Add(new SqlParameter("@Contraseña", encryptedPassword));
            comm.Parameters.Add(new SqlParameter("@Email", usuario.Email));
            comm.Parameters.Add(new SqlParameter("@Telefono", usuario.Telefono));
            comm.Parameters.Add(new SqlParameter("@IdSituacion", usuario.IdSituacionCrediticia));

            comm.ExecuteNonQuery();

            conn.Close();

            return true;
        }

    }
}