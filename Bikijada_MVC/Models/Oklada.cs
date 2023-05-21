using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bikijada_MVC.Models
{
    public class Oklada
    {
        public int ID { get; set; }

        [Required, StringLength(128, MinimumLength = 2)]
        [Display(Name = "Ime uplatitelja")]
        public string Ime { get; set; }

        [Required]
        [Display(Name = "Iznos u eurima")]
        public float Iznos { get; set; }

        public Borba Borba { get; set; }

        [Display(Name = "Ime bika")]
        public string Bik { get; set; }

        [Display(Name = "Ime vlasnika")]
        public string Vlasnik { get; set; }


    }
}
