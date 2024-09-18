using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWebApi.Model
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsCapitol { get; set; }

        [Column(TypeName = "decimal(2,2)")]
        [Range(0.0, 100, ErrorMessage = "Percentage of students must be between 0 and 100.")]
        public decimal PercentageOfStudents { get; set; }

        public ICollection<School>? Schools { get; set; }
    }
}
