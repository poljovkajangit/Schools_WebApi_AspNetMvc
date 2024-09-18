using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWebApi.Account;
using SchoolWebApi.Dtos;
using SchoolWebApi.Models;
using SchoolWebApi.Service.Interfaces;

namespace SchoolWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly ITokenService _TokenService;
        private readonly SignInManager<AppUser> _SignInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _UserManager = userManager;
            _TokenService = tokenService;
            _SignInManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _UserManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            var result = await _SignInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password is incorrect");
            }

            return Ok(
               new LoginUserDto
               {
                   Email = user.Email,
                   Userneme = user.UserName,
                   Token = _TokenService.CreateToken(user)
               }
            );

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid || registerDto == null)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser = await _UserManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _UserManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Email = appUser.Email,
                                Userneme = appUser.UserName,
                                Token = _TokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest(createdUser.Errors.Select(e => e.Description + " | "));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
