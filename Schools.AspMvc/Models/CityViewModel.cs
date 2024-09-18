using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Schools.MVC.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Code")]
        public string Code { get; set; } = string.Empty;
        [DisplayName("Is capitol")]
        public bool IsCapitol { get; set; }
    }
}
