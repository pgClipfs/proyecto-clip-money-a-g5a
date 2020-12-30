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
using ClipMoney.Models.Request;
using BC = BCrypt.Net.BCrypt;


namespace ClipMoney.Models
{

    public class UserManager
    {
        private string StrConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        public User GetByCuil(string cuil)
        {
            User usuario = new User();

            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM USUARIOS WHERE CUIL=@Cuil";
            comm.Parameters.Add(new SqlParameter("@Cuil", cuil));

            SqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                usuario = new User() { 
                    UserId = dr.GetInt32(0), 
                    Cuil = cuil, 
                    Name = dr.GetString(2), 
                    Surname = dr.GetString(3), 
                    Password = dr.GetString(4),
                    Email = dr.GetString(5),
                    PhoneNumber = dr.GetString(6),
                    Privileges = dr.GetString(7)
            };
            }

            dr.Close();
            conn.Close();


            return usuario;
        }

        public User GetByUserId(string UserID)
        {
            User usuario = new User();

            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT ID_USUARIO, CUIL, NOMBRE, APELLIDO, CLAVE, EMAIL, TELEFONO, PRIVILEGIOS FROM USUARIOS WHERE ID_USUARIO=@UserId";
            comm.Parameters.Add(new SqlParameter("@UserId", UserID));

            SqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                usuario = new User()
                {
                    UserId = dr.GetInt32(0),
                    Cuil = dr.GetString(1),
                    Name = dr.GetString(2),
                    Surname = dr.GetString(3),
                    Password = dr.GetString(4),
                    Email = dr.GetString(5),
                    PhoneNumber = dr.GetString(6),
                    Privileges = dr.GetString(7)
                };
            }

            dr.Close();
            conn.Close();


            return usuario;
        }

        public User UpdateDataByUserId(string UserID, string PhoneNumber, string Email)
        {
            User usuario = new User();

            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();


            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "UPDATE USUARIOS SET TELEFONO=@PhoneNumber, EMAIL=@Email WHERE ID_USUARIO=@UserId";
            comm.Parameters.Add(new SqlParameter("@PhoneNumber", PhoneNumber));
            comm.Parameters.Add(new SqlParameter("@Email", Email));
            comm.Parameters.Add(new SqlParameter("@UserId", UserID));

            comm.ExecuteNonQuery();
            conn.Close();

            return usuario;
        }

        public int RegisterUser(RegistrationRequest usuario)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            conn.Open();

            //string encryptedPassword = ServicioEncriptador.ComputeSha256Hash(usuario.Contraseña);
            string passwordHash = BC.HashPassword(usuario.Password);

            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = @"INSERT INTO USUARIOS(CUIL, NOMBRE, APELLIDO, Clave, EMAIL, TELEFONO, PRIVILEGIOS)
                                output INSERTED.ID_USUARIO
                                values(@Cuil, @Nombre, @Apellido, @Clave, @Email, @Telefono, @Privilegios)";

            comm.Parameters.Add(new SqlParameter("@Cuil", usuario.Cuil));
            comm.Parameters.Add(new SqlParameter("@Nombre", usuario.Name));
            comm.Parameters.Add(new SqlParameter("@Apellido", usuario.Surname));
            comm.Parameters.Add(new SqlParameter("@Clave", passwordHash));
            comm.Parameters.Add(new SqlParameter("@Email", usuario.Email));
            comm.Parameters.Add(new SqlParameter("@Telefono", usuario.PhoneNumber));
            comm.Parameters.Add(new SqlParameter("@Privilegios", "NO ACTIVO"));

            int IdUsuario = (int)comm.ExecuteScalar();

            foreach (var photo in usuario.Images)
            {
                SqlCommand commPhoto = conn.CreateCommand();
                commPhoto.CommandText = @"INSERT INTO USUARIOSxIMAGENES(NOMBRE, ID_USUARIO, URL, TIPO)
                                        values(@Nombre, @UserId, @Url, @Type)";
                commPhoto.Parameters.Add(new SqlParameter("@Nombre", ""));
                commPhoto.Parameters.Add(new SqlParameter("@UserID", IdUsuario));
                commPhoto.Parameters.Add(new SqlParameter("@Url", photo));
                commPhoto.Parameters.Add(new SqlParameter("@Type", 1)); // Tipo 1 = DNI

                commPhoto.ExecuteNonQuery();

            }

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            conn.Close();

            return IdUsuario;
        }

        public void ValidateUserData(RegistrationRequest user)
        {
            Regex soloLetras = new Regex(@"^(\w+\s?)*\s*$"); // solo letras y 1 solo espacio
            Regex soloNumeros = new Regex("^[0-9]*$"); // solo letras y 1 solo espacio
            Regex tieneNumeros = new Regex(@"[0-9]+");
            Regex tieneMayusculas = new Regex(@"[A-Z]+");
            Regex longitudMinimaMaxima = new Regex(@".{8,15}");
            Regex tieneMinisculas = new Regex(@"[a-z]+");
            Regex tieneSimbolos = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (user.Name.Length == 0 || !soloLetras.IsMatch(user.Name) )
            {
                throw new FormatException("Nombre de usuario incorrecto");
            }
            
            // validacion contraseña
            //if (!tieneNumeros.IsMatch(user.Contraseña))
            //{
            //    throw new FormatException("La contraseña debe poseer numeros");
            //}
            //else if (!tieneMayusculas.IsMatch(user.Contraseña))
            //{
            //    throw new FormatException("La contraseña debe poseer letras mayusculas");
            //}
            //else if (!longitudMinimaMaxima.IsMatch(user.Contraseña))
            //{
            //    throw new FormatException("La contraseña debe poseer longitud mayor a 8 y menor a 15");
            //}
            //else if (!tieneMinisculas.IsMatch(user.Contraseña))
            //{
            //    throw new FormatException("La contraseña debe poseer letras minisculas");
            //}

            //else if (!tieneSimbolos.IsMatch(user.Contraseña))
            //{
            //    throw new FormatException("La contraseña debe poseer simbolos");
            //}
            // termina validacion contraseña

            if (!soloNumeros.IsMatch(user.Cuil))
            {
                throw new FormatException("El cuil solo debe poseer numeros");
            }

            if (!RegexUtilities.IsValidEmail(user.Email))
            {
                throw new FormatException("El mail tiene formato incorrecto");
            }

            if (!soloNumeros.IsMatch(user.PhoneNumber) )
            {
                throw new FormatException("El numero de telefono solo debe poseer numeros");
            } else if (user.PhoneNumber.Length < 8 || user.PhoneNumber.Length > 11)
            {
                throw new FormatException("El numero de telefono posee longitud incorrecta");
            }

        }

    }
}