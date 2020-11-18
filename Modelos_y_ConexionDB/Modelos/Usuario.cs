using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_y_ConexionDB.Modelos
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nick { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Enlace { get; set; }
    }
}
