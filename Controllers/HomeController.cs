using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackNWork.Helpers;
using TrackNWork.Models;

namespace TrackNWork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string token = Request.Cookies["supabase_token"]?.Value;

            if (string.IsNullOrEmpty(token))
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/token-log.txt"), $"{DateTime.Now}: Kein Token im HomeController\n");
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/token-log.txt"), $"{DateTime.Now}: Token empfangen im HomeController: {token}\n");
            }

            if (token == null)
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/token-log.txt"), $"{DateTime.Now}: Token ist null: \n" + token);

                return RedirectToAction("Login", "Authentication");
            }
               
               
            try
            {
                //var user = SupabaseAuthHelper.ValidateToken(token);

                var principal = SupabaseAuthHelper.ValidateToken(token);
                var userInfo = SupabaseUserInfo.FromClaimsPrincipal(principal);
                Session["UserInfo"] = userInfo;


                ViewBag.Email = userInfo.Email;
                ViewBag.BauhofId = userInfo.BauhofId;
                ViewBag.Role = userInfo.Role;
                ViewBag.DisplayName = userInfo.DisplayName;
                return View();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/token-log.txt"), $"{DateTime.Now}: Fehler bei der Validierung: \n" + ex.Message);
                return RedirectToAction("Login", "Authentication");
            }
        }

        public ActionResult ProjektZeit()
        {
            string token = Request.Cookies["supabase_token"]?.Value;

            if (string.IsNullOrEmpty(token))
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/token-log.txt"), $"{DateTime.Now}: Kein Token im HomeController\n");
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/token-log.txt"), $"{DateTime.Now}: Token empfangen im HomeController: {token}\n");
            }

            if (token == null)
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/token-log.txt"), $"{DateTime.Now}: Token ist null: \n" + token);

                return RedirectToAction("Login", "Authentication");
            }


            try
            {
                //var user = SupabaseAuthHelper.ValidateToken(token);

                var principal = SupabaseAuthHelper.ValidateToken(token);
                var userInfo = SupabaseUserInfo.FromClaimsPrincipal(principal);
                Session["UserInfo"] = userInfo;


                ViewBag.Email = userInfo.Email;
                ViewBag.BauhofId = userInfo.BauhofId;
                ViewBag.Role = userInfo.Role;
                ViewBag.DisplayName = userInfo.DisplayName;
                ViewBag.UserId = userInfo.UserId;
                return View();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/token-log.txt"), $"{DateTime.Now}: Fehler bei der Validierung: \n" + ex.Message);
                return RedirectToAction("Login", "Authentication");
            }
           
        }

        public ActionResult Contact()
        {
            //var token = GetTokenFromCookie();

            //if (token == null)
            //    return RedirectToAction("Login", "Authentication");

            //try
            //{
            //    //var user = SupabaseAuthHelper.ValidateToken(token);

            //    var principal = SupabaseAuthHelper.ValidateToken(token);
            //    var userInfo = SupabaseUserInfo.FromClaimsPrincipal(principal);

            //    ViewBag.Email = userInfo.Email;
            //    ViewBag.BauhofId = userInfo.BauhofId;
            //    ViewBag.Role = userInfo.Role;
            //    ViewBag.DisplayName = userInfo.DisplayName;
            //    return View();
            //}
            //catch
            //{
            //    return RedirectToAction("Login", "Authentication");
            //}
            return View();
        }
        private string GetTokenFromCookie()
        {
            var cookie = Request.Cookies["supabase_token"];
            return cookie?.Value;
        }

        [HttpGet]
        public JsonResult GetTagesuebersicht(Guid userId, DateTime datum)
        {
            var supaUser = Session["UserInfo"] as SupabaseUserInfo;

            if (supaUser.Role == "admin")
            {
                using (var db = new SupabaseDBContext())
                {
                    var eintraege = db.VwZeiteintraegeTagesansicht
                        .Where(z => z.datum == datum)
                        .ToList();

                    return Json(eintraege, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string s_guid = userId.ToString();
                using (var db = new SupabaseDBContext())
                {
                    var eintraege = db.VwZeiteintraegeTagesansicht
                        .Where(z => z.datum == datum && z.mitarbeiter_id == s_guid)
                        .ToList();

                    return Json(eintraege, JsonRequestBehavior.AllowGet);
                }
            }

        }
    }
}