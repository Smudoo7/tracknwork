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
            var token = GetTokenFromCookie();

            if (token == null)
                return RedirectToAction("Login", "Authentication");

            try
            {
                //var user = SupabaseAuthHelper.ValidateToken(token);

                var principal = SupabaseAuthHelper.ValidateToken(token);
                var userInfo = SupabaseUserInfo.FromClaimsPrincipal(principal);

                ViewBag.Email = userInfo.Email;
                ViewBag.BauhofId = userInfo.BauhofId;
                ViewBag.Role = userInfo.Role;
                ViewBag.DisplayName = userInfo.DisplayName;
                return View();
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        public ActionResult About()
        {
            var token = GetTokenFromCookie();

            if (token == null)
                return RedirectToAction("Login", "Authentication");

            try
            {
                var user = SupabaseAuthHelper.ValidateToken(token);

                var email = user.FindFirst("email")?.Value;
                var bauhofId = user.FindFirst("user_metadata.bauhof_id")?.Value;

                ViewBag.BauhofId = bauhofId;
                ViewBag.Email = email;

                return View();
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        public ActionResult Contact()
        {
            var token = GetTokenFromCookie();

            if (token == null)
                return RedirectToAction("Login", "Authentication");

            try
            {
                var user = SupabaseAuthHelper.ValidateToken(token);

                var email = user.FindFirst("email")?.Value;
                var bauhofId = user.FindFirst("user_metadata.bauhof_id")?.Value;

                ViewBag.BauhofId = bauhofId;
                ViewBag.Email = email;

                return View();
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        private string GetTokenFromCookie()
        {
            var cookie = Request.Cookies["supabase_token"];
            return cookie?.Value;
        }
    }
}