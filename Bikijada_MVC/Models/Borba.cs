using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bikijada_MVC.Models
{
    public class Borba
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Odaberite datum i vrijeme")]
        [Display(Name = "Datum i vrijeme borbe")]
        public DateTime Date { get; set; }

        [Display(Name = "Kategorija")]
        public string Kategorija { get; set; }

        [Display(Name = "Vlasnik 1. bika")]
        public string Vlasnik1 { get; set; }

        public int? Bik1Id { get; set; }
        [Display(Name = "1. bik")]
        public virtual Bik? Bik1 { get; set; }

        [Display(Name = "Vlasnik 2. bika")]
        public string Vlasnik2 { get; set; }

        public int? Bik2Id { get; set; }
        [Display(Name = "2. bik")]
        public virtual Bik? Bik2 { get; set; }

    }
}