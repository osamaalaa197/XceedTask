using Microsoft.AspNetCore.Identity;
using Product__Application.Models;
using Product__Application.ViewModel;

namespace Product__Application.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(UserApp newUser, string password);
        Task SignInAsync(UserApp userApp);
        Task<bool> CheckSignInDataAsync(LoginViewModel userData);
        Task SignOutAsync();
        UserApp ConvertRegisterVMtoAppUser(RegisterViewModel userData);
    }
}
