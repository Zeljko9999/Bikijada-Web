using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bikijada_MVC.Models
{
    public class Oklada
    {
        public int ID { get; set; }

        [Required, StringLength(128, MinimumLength = 2)]
        [Display(Name = "Uplatitelj")]
        public string Ime { get; set; }

        [Required]
        [Display(Name = "Iznos (€)")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Vrijednost mora biti pozitivan broj")]
        public float Iznos { get; set; }

        [Display(Name = "Kategorija")]
        public string Kategorija { get; set; }

        [Display(Name = "Vlasnik")]
        public string Vlasnik { get; set; }

        public int BikId { get; set; }
        [Display(Name = "Bik")]
        public virtual Bik? Bik { get; set; }

    }
}
