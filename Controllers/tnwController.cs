using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

//using System.Web.Mvc;
using TrackNWork.Models;

namespace TrackNWork.Controllers
{
    public class tnwController : ApiController
    {
        // GET: Test
        [HttpGet]
        [System.Web.Http.Route("api/tnw/zeiteintraege")]
        public IHttpActionResult GetTestEintraege()
        {
            try
            {
                using (var db = new SupabaseDBContext())
                {
                    var eintraege = db.VwZeiteintraegeTagesansicht
                                      .OrderByDescending(z => z.datum)
                                      .Take(10)
                                      .ToList();

                    return Ok(eintraege);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Fehler beim Abrufen der Daten: " + ex.Message));
            }
        }
        
       
    }
}