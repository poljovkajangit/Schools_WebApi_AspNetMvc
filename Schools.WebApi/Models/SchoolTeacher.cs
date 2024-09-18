using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWebApi.Model
{
    public class SchoolTeacher
    {
        [Key]
        public int SchoolId { get; set; }
        [Key]
        public int TeacherId { get; set; }
        public int NumberOfSubjects { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal? Salary { get; set; }
        public required School School { get; set; }
        public required Teacher Teacher { get; set; }
    }
}
