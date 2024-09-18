using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public bool IsOnBudget { get; set; }
        public int SchoolId { get; set; }
        public required School School { get; set; }
    }
}
