using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackNWork.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetToken()
        {
            try
            {
                var json = new StreamReader(Request.InputStream).ReadToEnd();
                var token = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json)?.token?.ToString();

                var logPath = Server.MapPath("~/App_Data/token-log.txt");
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Token empfangen: {token}\n");

                if (string.IsNullOrEmpty(token))
                {
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Kein gültiger Token\n");
                    return new HttpStatusCodeResult(400, "Kein gültiger Token");
                }

                var cookie = new HttpCookie("supabase_token", token)
                {
                    Path = "/",
                    Secure = true,
                    HttpOnly = false,
                    SameSite = SameSiteMode.Lax
                };

                Response.Cookies.Add(cookie);
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Cookie gesetzt\n");

                return new HttpStatusCodeResult(200);
            }
            catch (Exception ex)
            {
                var logPath = Server.MapPath("~/App_Data/token-log.txt");
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Fehler: {ex.Message}\n");

                return new HttpStatusCodeResult(500, "Serverfehler beim Setzen des Tokens");
            }
        }
    }

}