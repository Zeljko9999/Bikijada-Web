using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bikijada_MVC.Models
{
    public class Vlasnik
    {
        public int ID { get; set; }

        [Required, StringLength(128, MinimumLength = 2)]
        [Display(Name = "Vlasnik bika")]
        public string Ime { get; set; }

        [Required, StringLength(128, MinimumLength = 2)]
        [Display(Name = "Mjesto vlasnika")]
        public string Mjesto { get; set; }

    }
}