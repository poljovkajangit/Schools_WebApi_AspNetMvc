using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Model
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<SchoolTeacher>? SchoolTeachers { get; set; }
    }
}
