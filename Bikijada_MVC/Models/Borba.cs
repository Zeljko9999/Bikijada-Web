﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bikijada_MVC.Models
{
    public class Borba
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Odaberite datum i vrijeme")]
        [Display(Name = "Datum i vrijeme borbe")]
        [DataType(DataType.DateTime, ErrorMessage = "pimjer: gggg-mm-dd hh:mm:ss")]
        public DateTime Date { get; set; }

        [Display(Name = "Vlasnik prvog bika")]
        public string Vlasnik1 { get; set; }

        public int? Bik1Id { get; set; }
        [Display(Name = "Prvi bik")]
        public virtual Bik? Bik1 { get; set; }

        [Display(Name = "Vlasnik drugog bika")]
        public string Vlasnik2 { get; set; }

        public int Bik2Id { get; set; }
        [Display(Name = "Drugi bik")]
        public virtual Bik? Bik2 { get; set; }

    }
}