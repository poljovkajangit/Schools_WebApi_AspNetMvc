using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Dtos
{
    public record CityDto
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Must be longer than 3 characters")]
        [MaxLength(50, ErrorMessage = "Must be shorter than 50 characters")]
        [Required]
        public required string Name { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsCapitol { get; set; }
    }
}
