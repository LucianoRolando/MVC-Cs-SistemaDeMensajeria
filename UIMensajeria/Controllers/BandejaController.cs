using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelos_y_ConexionDB;

namespace UIMensajeria.Controllers
{
    public class BandejaController : Controller
    {
        DateTime fecha = DateTime.Now;
        // GET: Bandeja
        public ActionResult MenuPrincipal()
        {
            Modelos_y_ConexionDB.ConexionDB.BandejaMensajes Band = new Modelos_y_ConexionDB.ConexionDB.BandejaMensajes();

            List<Modelos_y_ConexionDB.Modelos.Usuario> lius = new List<Modelos_y_ConexionDB.Modelos.Usuario>();
            int n = Convert.ToInt32(Session["IDus"].ToString());
            lius = Band.Usuarios(n);

            return View(lius);
        }


        public ActionResult Mensaje(int id, string nombre)
        {
            int idem = Convert.ToInt32(Session["IDus"].ToString());

            Session["IDRE"] = id;
            //Porque al ser redirigido desde Httpost(Mensaje) no mando parametro nombre -->
            if (nombre != null)
            { Session["NOMRE"] = nombre; }
            
            Modelos_y_ConexionDB.ConexionDB.BandejaMensajes men = new Modelos_y_ConexionDB.ConexionDB.BandejaMensajes();

            return View(men.Mensajes(idem, id));
        }


        [HttpPost]
        public ActionResult Mensaje(string mje)
        {
            Modelos_y_ConexionDB.Modelos.Mensaje men = new Modelos_y_ConexionDB.Modelos.Mensaje();
            Modelos_y_ConexionDB.ConexionDB.BandejaMensajes nm = new Modelos_y_ConexionDB.ConexionDB.BandejaMensajes();

            men.EM = Convert.ToInt32(Session["IDus"].ToString());
            men.RE = Convert.ToInt32(Session["IDRE"].ToString());
            men.TEXTO = mje;
            men.FECHA = fecha;

            nm.AgregarMensaje(men);

            return RedirectToAction("Mensaje");
            //return View(nm.Mensajes(men.EM, men.RE));
        }

        public ActionResult EditarCuenta()
        {
            return View();
        }


        [HttpPost]
        public ActionResult EditarCuenta(string nick, string cont, string nom, string desc, HttpPostedFileBase foto)
        {
                Modelos_y_ConexionDB.Modelos.Usuario us = new Modelos_y_ConexionDB.Modelos.Usuario();

                us.ID = Convert.ToInt32(Session["IDus"].ToString());
                us.Nick = nick;
                us.Nombre = nom;
                us.Contraseña = cont;
                us.Descripcion = desc;
                if (foto != null){ 
                us.Enlace = "/Imagenes/" + foto.FileName;
                string engu = Server.MapPath("~/Imagenes/") + foto.FileName;
                foto.SaveAs(engu);
                }
                else { us.Enlace = Session["Enlus"].ToString(); }
                

                Modelos_y_ConexionDB.ConexionDB.BandejaMensajes BanMe= new Modelos_y_ConexionDB.ConexionDB.BandejaMensajes();
                BanMe.EditarUsuario(us);

                Session["Nickus"] = us.Nick;
                Session["Contus"] = us.Contraseña;
                Session["Nomus"] = us.Nombre;
                Session["Descus"] = us.Descripcion;
                Session["Enlus"] = us.Enlace;

                return RedirectToAction("MenuPrincipal", "Bandeja");
        }

       

    }
}