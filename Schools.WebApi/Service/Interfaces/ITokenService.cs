using SchoolWebApi.Models;

namespace SchoolWebApi.Service.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
