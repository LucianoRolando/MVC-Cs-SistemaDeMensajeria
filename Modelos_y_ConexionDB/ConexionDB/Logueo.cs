using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Modelos_y_ConexionDB.ConexionDB
{
    
    public class Logueo
    {

        // Conexion a Base de datos usada por el controller Logueo,
        // hace login de un usuario y crea cuentas nuevas

        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2NNNOP4\\SQLEXPRESS; Initial Catalog=MensajeriaDB; Integrated Security=True;");

        public Modelos.Usuario Validacion(string ni, string co)
        {
            Modelos.Usuario usua = new Modelos.Usuario();
            usua.ID = 0;

            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("exec Logueo @nic, @con;", conexion);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@nic", ni);
                comando.Parameters.AddWithValue("@con", co);
                
                SqlDataReader lector;
                
                lector = comando.ExecuteReader();
                while (lector.Read())
                {

                    usua.ID = lector.GetInt32(0);
                    usua.Nick = lector.GetString(1);
                    usua.Contraseña = lector.GetString(2);
                    usua.Nombre = lector.GetString(3);
                    usua.Descripcion = lector.GetString(4);
                    usua.Enlace = lector.GetString(5);

                }
            }
            catch { }
            finally
            {
                conexion.Close();
            }

            return usua;

        }

        public int Agregar(Modelos.Usuario pe)
        {
            int id=0;
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("Exec AgregarUsuario @nik,@con,@nom,@des,@en;", conexion);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@nik", pe.Nick);
                comando.Parameters.AddWithValue("@con", pe.Contraseña);
                comando.Parameters.AddWithValue("@nom", pe.Nombre);
                comando.Parameters.AddWithValue("@des", pe.Descripcion);
                comando.Parameters.AddWithValue("@en", pe.Enlace);

                SqlDataReader lector;
                
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    id = lector.GetInt32(0);
                }
            }
            catch { }
            finally
            {
                conexion.Close();
            }

            return id;
        }
    }
}