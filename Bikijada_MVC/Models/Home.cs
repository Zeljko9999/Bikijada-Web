using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Xml.Linq;

namespace Bikijada_MVC.Models
{
    public class Home
    {
        public int ID { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Netočan oblik email adrese")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Naslov mora može imati max 50 znakova")]
        public string Naslov { get; set; }

        [StringLength(300, MinimumLength = 2, ErrorMessage = "Poruka mora biti do 300 znakova")]
        public string Poruka { get; set; }

    }
}
