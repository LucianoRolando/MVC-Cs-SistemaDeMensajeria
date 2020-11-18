using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelos_y_ConexionDB;

namespace TestUnitarios
{
    [TestClass]
    public class PruebasConexionDB
    {
        [TestMethod]
        public void Validacion()
        {
            var nic = "Lucho";
            var con = "contra";
            Modelos_y_ConexionDB.Modelos.Usuario user = new Modelos_y_ConexionDB.Modelos.Usuario();
            user.ID = 5;
            user.Nick = "Lucho";
            user.Nombre = "Luciano Rolando";
            user.Contraseña = "contra";
            user.Descripcion = "Estudiante de Desarrollo de Software";
            user.Enlace = "/Imagenes/cover_bg_1.jpg";
            Modelos_y_ConexionDB.ConexionDB.Logueo log = new Modelos_y_ConexionDB.ConexionDB.Logueo();


            var ResultadoEsperado = log.Validacion(nic, con);


            Assert.AreEqual(user.ID, ResultadoEsperado.ID);
            Assert.AreEqual(user.Nick, ResultadoEsperado.Nick);
            Assert.AreEqual(user.Contraseña, ResultadoEsperado.Contraseña);
            Assert.AreEqual(user.Nombre, ResultadoEsperado.Nombre);
            Assert.AreEqual(user.Descripcion, ResultadoEsperado.Descripcion);
            Assert.AreEqual(user.Enlace, ResultadoEsperado.Enlace);

        }

        //public void Agregar()
        //{
        //    Modelos_y_ConexionDB.Modelos.Usuario user1 = new Modelos_y_ConexionDB.Modelos.Usuario();
        //    user1.Nick = "cris";
        //    user1.Nombre = "Cristian";
        //    user1.Contraseña = "cont";
        //    user1.Descripcion = "PruebaPrueba";
        //    user1.Enlace = "/Imagen";

            
        //    Modelos_y_ConexionDB.ConexionDB.Logueo log = new Modelos_y_ConexionDB.ConexionDB.Logueo();
        //    int ResultadoEsperado = log.Agregar(user1);


        //    Assert.AreEqual(user1.ID, ResultadoEsperado);
            

        //}

    }
}
