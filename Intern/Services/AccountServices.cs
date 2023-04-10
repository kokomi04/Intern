using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Result;

namespace Intern.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountServices(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        public async Task<ApiResult<string>> Login(SignInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) 
                return new ApiErrorResult<string>("Tài khoản không tồn tại!");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!result.Succeeded)
                return new ApiErrorResult<string>("Sai mật khẩu!");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: cred
                );

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<IdentityResult> Register(SignUpRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                throw new Exception("Email đã tồn tại");
            }

            var user = new AppUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DoB = request.DoB,
                UserName= request.Email
            };

            return await _userManager.CreateAsync(user, request.Password);
        }
    }
}
