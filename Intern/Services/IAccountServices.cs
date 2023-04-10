using Microsoft.AspNetCore.Identity;
using Intern.ViewModels;
using Intern.ViewModels.Result;

namespace Intern.Services
{
    public interface IAccountServices
    {
        Task<IdentityResult> Register(SignUpRequest request);
        Task<ApiResult<string>> Login(SignInRequest request);
    }
}
