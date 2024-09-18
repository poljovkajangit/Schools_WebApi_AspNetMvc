using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Account
{
    public class LoginDto
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
