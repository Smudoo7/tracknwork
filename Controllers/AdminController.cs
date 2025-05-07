using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TrackNWork.Models;
using System.Buffers.Text;

namespace TrackNWork.Controllers
{
    public class AdminController : Controller
    {
        private const string SupabaseUrl = "https://hhnzduydbzxuyusuujfc.supabase.co";
        private const string ServiceRoleKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImhobnpkdXlkYnp4dXl1c3V1amZjIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc0NjQ3NTk0NSwiZXhwIjoyMDYyMDUxOTQ1fQ.wGgeIP_RG2K3axUSjV4g5FxrYOYmJwjwIgbmYwc1SMw";

        [HttpGet]
        
        public ActionResult Dashboard()
        {
            return View("Dashboard", new SupabaseUserInfo());
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View(new SupabaseUserInfo());
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateUser(SupabaseUserInfo model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Bitte alle Felder korrekt ausfüllen.";
                return View("Dashboard", model);
            }

            var email = $"{model.Username.ToLower()}@{model.BauhofId.ToLower()}.tracknwork.de";


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ServiceRoleKey);
                client.DefaultRequestHeaders.Add("apikey", ServiceRoleKey);

                var payload = new
                {
                    email,
                    password = model.Password,
                    email_confirm = true
                };

                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{SupabaseUrl}/auth/v1/admin/users", content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Fehler: " + result;
                    return View("Dashboard", model);
                }

                dynamic user = JsonConvert.DeserializeObject(result);
                string userId = user.id;

                var meta = new
                {
                    user_metadata = new
                    {
                        role = model.Role.ToLower(),
                        bauhof_id = model.BauhofId.ToUpper(),
                        display_name = model.DisplayName
                    }
                };

                var metaContent = new StringContent(JsonConvert.SerializeObject(meta), Encoding.UTF8, "application/json");
                var metaRes = await client.PutAsync($"{SupabaseUrl}/auth/v1/admin/users/{userId}", metaContent);
                var metaResult = await metaRes.Content.ReadAsStringAsync();

                if (!metaRes.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Benutzer erstellt, aber Metadaten fehlgeschlagen: " + metaResult;
                    return View("Dashboard", model);
                }

                ViewBag.Success = $"Benutzer {email} erfolgreich erstellt.";
                return View("Dashboard", new SupabaseUserInfo()); // Formular leeren
            }
        }
    }
    
}