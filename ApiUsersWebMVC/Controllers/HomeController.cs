using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApiUsersWebMVC.Controllers;
using ApiUsersWebMVC.Models;
using Newtonsoft.Json;
using PagedList;

namespace ApiUsersWebMVC.Controllers
{
    public class HomeController : Controller
    {
        class Respuesta
        {
            public string status { get; set; }
            public string url { get; set; }

        }
        
        public ActionResult Index(int? page, string filtrar, string nombre)
        {
            
            if (filtrar==null||filtrar== "----------")
            {
                filtrar = "todos";
                ViewData["filtro"] = filtrar;
            }

            if(nombre == null )
            {
                nombre = "";
            }

            ViewData["filtro"] = filtrar;
            int pagesize = 6;
            int pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<UsuariosActivos> user = null;
            ApiUser apiUsers = new ApiUser();
            ListaUsers lista = new ListaUsers();
            lista.usuariosActivos = apiUsers.filtrarUsuario(filtrar, nombre);
            user = lista.usuariosActivos.ToPagedList(pageindex,pagesize);
            return View(user);
        }
        

        public JsonResult InicarVideoLlamada(string nombre, string apellido, string telefono, string email)
        {

            string responseFromServer;
            WebRequest request2 = WebRequest.Create("https://client3.whatchmenow.com/api/session_request.php?name=" + nombre + "&duration=60&key=ad967e8e-73f4-11ea-9ba4-d3a8a6bb4ea4&type=user&lastname=" + apellido + "&phone=" + telefono + "&email=" + email + "&session_type=video");
            // If required by the server, set the credentials.  
            request2.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse response = request2.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server. 
            // The using block ensures the stream is automatically closed. 
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                responseFromServer = reader.ReadToEnd();
                // Display the content.  

                response.Close();
            }

            var res = JsonConvert.DeserializeObject<Respuesta>(responseFromServer);
            return Json(res.url, JsonRequestBehavior.AllowGet);
        }

    }
}