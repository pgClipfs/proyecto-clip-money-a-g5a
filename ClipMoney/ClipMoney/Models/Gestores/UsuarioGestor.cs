using ClipMoney.Controllers;
using ClipMoney.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


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
            comm.CommandText = @"INSERT INTO USUARIOS(CUIL, NOMBRE, APELLIDO, CONTRASEÑA, EMAIL, TELEFONO, ID_SITUACION_CREDITICIA, PRIVILEGIOS) 
                                values(@Cuil, @Nombre, @Apellido, @Contraseña, @Email, @Telefono, @IdSituacion, @Privilegios)";

            comm.Parameters.Add(new SqlParameter("@Cuil", usuario.Cuil));
            comm.Parameters.Add(new SqlParameter("@Nombre", usuario.Nombre));
            comm.Parameters.Add(new SqlParameter("@Apellido", usuario.Apellido));
            comm.Parameters.Add(new SqlParameter("@Contraseña", encryptedPassword));
            comm.Parameters.Add(new SqlParameter("@Email", usuario.Email));
            comm.Parameters.Add(new SqlParameter("@Telefono", usuario.Telefono));
            comm.Parameters.Add(new SqlParameter("@IdSituacion", usuario.IdSituacionCrediticia));
            comm.Parameters.Add(new SqlParameter("@Privilegios", "NO ACTIVO"));

            comm.ExecuteNonQuery();

            conn.Close();

            return true;
        }

        public void ValidarDatosUsuario(Usuario user)
        {
            Regex soloLetras = new Regex(@"^[a-zA-Z]+$"); // solo letras y 1 solo espacio
            Regex soloNumeros = new Regex("^[0-9]*$"); // solo letras y 1 solo espacio
            Regex tieneNumeros = new Regex(@"[0-9]+");
            Regex tieneMayusculas = new Regex(@"[A-Z]+");
            Regex longitudMinimaMaxima = new Regex(@".{8,15}");
            Regex tieneMinisculas = new Regex(@"[a-z]+");
            Regex tieneSimbolos = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (user.Nombre.Length == 0 || !soloLetras.IsMatch(user.Nombre) )
            {
                throw new FormatException("Nombre de usuario incorrecto");
            }
            
            // validacion contraseña
            if (!tieneNumeros.IsMatch(user.Contraseña))
            {
                throw new FormatException("La contraseña debe poseer numeros");
            }
            else if (!tieneMayusculas.IsMatch(user.Contraseña))
            {
                throw new FormatException("La contraseña debe poseer letras mayusculas");
            }
            else if (!longitudMinimaMaxima.IsMatch(user.Contraseña))
            {
                throw new FormatException("La contraseña debe poseer longitud mayor a 8 y menor a 15");
            }
            else if (!tieneMinisculas.IsMatch(user.Contraseña))
            {
                throw new FormatException("La contraseña debe poseer letras minisculas");
            }

            else if (!tieneSimbolos.IsMatch(user.Contraseña))
            {
                throw new FormatException("La contraseña debe poseer simbolos");
            }
            // termina validacion contraseña

            if (!soloNumeros.IsMatch(user.Cuil))
            {
                throw new FormatException("El cuil solo debe poseer numeros");
            }

            if (!RegexUtilities.IsValidEmail(user.Email))
            {
                throw new FormatException("El mail tiene formato incorrecto");
            }

            if (!soloNumeros.IsMatch(user.Telefono) )
            {
                throw new FormatException("El numero de telefono solo debe poseer numeros");
            } else if (user.Telefono.Length < 8 || user.Telefono.Length > 11)
            {
                throw new FormatException("El numero de telefono posee longitud incorrecta");
            }

        }

    }
}