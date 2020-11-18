using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Modelos_y_ConexionDB.ConexionDB
{
    public class BandejaMensajes
    {

        // Conexion a Base de datos usada por el controller BandejaMensajes,
        // lista contactos, muestra y envia mensajes y permite editar cuenta

        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2NNNOP4\\SQLEXPRESS; Initial Catalog=MensajeriaDB; Integrated Security=True;");

        public List<Modelos.Usuario> Usuarios(int IDus)
        {
            List<Modelos.Usuario> Liusu = new List<Modelos.Usuario>();

            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("exec ListarUsuarios @IDus;", conexion);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@IDus", IDus);
                SqlDataReader lector;

                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Modelos.Usuario pe = new Modelos.Usuario();

                    pe.ID = lector.GetInt32(0);
                    pe.Nick = lector.GetString(1);
                    pe.Contraseña = lector.GetString(2);
                    pe.Nombre = lector.GetString(3);
                    pe.Descripcion = lector.GetString(4);
                    pe.Enlace = lector.GetString(5);
                    Liusu.Add(pe);
                }
            }
            catch { }
            finally { conexion.Close(); }

            return Liusu;
        }

        public List<Modelos.Mensaje> Mensajes(int IDEM, int IDRE)
        {
            List<Modelos.Mensaje> Limen = new List<Modelos.Mensaje>();

            try
            {

                conexion.Open();
                SqlCommand comando = new SqlCommand("exec ListarMensajes @IDem,@IDre;", conexion);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@IDem", IDEM);
                comando.Parameters.AddWithValue("@IDre", IDRE);
                
                SqlDataReader lector;
                
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Modelos.Mensaje pe = new Modelos.Mensaje();

                    pe.ID = lector.GetInt32(0);
                    pe.EM = lector.GetInt32(1);
                    pe.RE = lector.GetInt32(2);
                    pe.FECHA = lector.GetDateTime(3);
                    pe.TEXTO = lector.GetString(4);

                    Limen.Add(pe);
                }
            }
            catch { }
            finally { conexion.Close(); }

            return Limen;

        }

        public void AgregarMensaje(Modelos.Mensaje mj)
        {
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("exec AgregarMensaje @em, @re, @fe, @tx;", conexion);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@em", mj.EM);
                comando.Parameters.AddWithValue("@re", mj.RE);
                comando.Parameters.AddWithValue("@fe", mj.FECHA);
                comando.Parameters.AddWithValue("@tx", mj.TEXTO);

                comando.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                conexion.Close();
            }
        }

        public void EditarUsuario(Modelos.Usuario pe)
        {
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("exec EditarUsuario @id, @nik, @con, @nom, @des, @en;", conexion);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@id", pe.ID);
                comando.Parameters.AddWithValue("@nik", pe.Nick);
                comando.Parameters.AddWithValue("@con", pe.Contraseña);
                comando.Parameters.AddWithValue("@nom", pe.Nombre);
                comando.Parameters.AddWithValue("@des", pe.Descripcion);
                comando.Parameters.AddWithValue("@en", pe.Enlace);

                comando.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                conexion.Close();
            }
        }
    }
}