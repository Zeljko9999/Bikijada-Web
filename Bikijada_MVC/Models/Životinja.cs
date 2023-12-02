using Bikijada_MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace Bikijada_MVC.Models
{
    public abstract class Životinja
    {
        [Required, StringLength(128, MinimumLength = 2)]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
    }
}


public class Bik : Životinja
{
    public int ID { get; set; }

    [Required]
    [Display(Name = "Kategorija")]
    public string Kategorija { get; set; }

    public int? VlasnikId { get; set; }
    [Display(Name = "Vlasnik")]
    public virtual Vlasnik? Vlasnik { get; set; }
}
