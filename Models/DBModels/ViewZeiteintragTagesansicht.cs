using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackNWork.Models.DBModels
{
    public class ViewZeiteintragTagesansicht
    {
        public Guid zeiteintrag_id { get; set; }
        public DateTime datum { get; set; }
        public int dauer_min { get; set; }
        public string vorname { get; set; }
        public string nachname { get; set; }
        public string personalnummer { get; set; }
        public string mitarbeiter_id { get; set; }
        public string bauhof_name { get; set; }
        public string stelle { get; set; }
        public string ha_nummer { get; set; }
        public string geraete { get; set; }
        public string fahrzeuge { get; set; }
    }
}