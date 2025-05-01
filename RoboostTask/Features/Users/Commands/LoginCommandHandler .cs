using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RoboostTask.DTOs.User;
using RoboostTask.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoboostTask.Features.Users.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<LoginResultDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new UnauthorizedAccessException("Invalid email");

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                throw new UnauthorizedAccessException("Invalid password");

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Iss"],
                audience: _config["JWT:Aud"],
                expires: request.RememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new LoginResultDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Username = user.FirstName,
                Roles = userRoles
            };
        }
    }
}
