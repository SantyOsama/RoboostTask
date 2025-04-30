using MediatR;
using Microsoft.AspNetCore.Identity;
using RoboostTask.DTOs;
using RoboostTask.Models;

namespace RoboostTask.Features.Users.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResultDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RegisterResultDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                throw new InvalidOperationException("User already exists");

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            var roleName = request.Role.ToString();

            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));

            await _userManager.AddToRoleAsync(user, roleName);


            return new RegisterResultDTO { Message = "User registered successfully." };
        }
    }
}
