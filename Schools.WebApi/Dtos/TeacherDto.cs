using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Dtos
{
    public record TeacherDto
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Must be longer than 3 characters")]
        [MaxLength(50, ErrorMessage = "Must be shorter than 50 characters")]
        [Required]
        public required string Name { get; set; }
    }
}
