using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelos_y_ConexionDB;

namespace UIMensajeria.Controllers
{
    public class LogueoController : Controller
    {
       
        // GET: Logueo
        public ActionResult Log()
        {
            
            Session["IDus"] = null;
            Session["Nickus"] = null;
            Session["Contus"] = null;
            Session["Nomus"] = null;
            Session["Descus"] = null;
            Session["Enlus"] = null;

            return View();
        }

        [HttpPost]
        public ActionResult Log(string nick, string cont)
        {
            Modelos_y_ConexionDB.ConexionDB.Logueo cn = new Modelos_y_ConexionDB.ConexionDB.Logueo();

            Modelos_y_ConexionDB.Modelos.Usuario usu = cn.Validacion(nick, cont);

            if (usu.ID != 0)
            {
                Session["IDus"] = usu.ID;
                Session["Nickus"] = usu.Nick;
                Session["Contus"] = usu.Contraseña;
                Session["Nomus"] = usu.Nombre;
                Session["Descus"] = usu.Descripcion;
                Session["Enlus"] = usu.Enlace;

                return RedirectToAction("MenuPrincipal", "Bandeja");
            }
            else

            { return View(); }

        }

        [HttpPost]

        public ActionResult Nuevo(string nick, string cont, string nom, string desc, HttpPostedFileBase foto)
        {
            Modelos_y_ConexionDB.Modelos.Usuario us = new Modelos_y_ConexionDB.Modelos.Usuario();

            if (nick == "")
            { return RedirectToAction("Log"); }
            else
            { us.Nick = nick; }
            us.Nombre = nom;
            us.Contraseña = cont;
            us.Descripcion = desc;

            if (foto != null)
            {//En la base se guarda un enlace relativo a la foto
                us.Enlace = "/Imagenes/" + foto.FileName;

                //La foto se guarda en una carpeta dentro del Proyecto
                string engu = Server.MapPath("~/Imagenes/") + foto.FileName;
                foto.SaveAs(engu);
            }
            else { us.Enlace = ""; }


            Modelos_y_ConexionDB.ConexionDB.Logueo cn = new Modelos_y_ConexionDB.ConexionDB.Logueo();

            int id = cn.Agregar(us);
            if (id != 0)
            {
                Session["IDus"] = id;
                Session["Nickus"] = us.Nick;
                Session["Contus"] = us.Contraseña;
                Session["Nomus"] = us.Nombre;
                Session["Descus"] = us.Descripcion;
                Session["Enlus"] = us.Enlace;

                return RedirectToAction("MenuPrincipal", "Bandeja");
            }
            else
            {
                return RedirectToAction("Log");
            }
        }
    }
}