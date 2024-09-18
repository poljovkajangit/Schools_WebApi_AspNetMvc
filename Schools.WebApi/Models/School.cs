using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Model
{
    public class School
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsPrimary { get; set; }
        public ICollection<Student>? Students { get; set; }
        public required City City { get; set; }
        public ICollection<SchoolTeacher>? SchoolTeachers { get; set; }
    }
}
