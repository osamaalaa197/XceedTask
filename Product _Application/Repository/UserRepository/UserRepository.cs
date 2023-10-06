using Microsoft.AspNetCore.Identity;
using Product__Application.Constants;
using Product__Application.Models;
using Product__Application.ViewModel;
using System;
using System.Threading.Tasks;

namespace Product__Application.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly SignInManager<UserApp> _signInManager;

        public UserRepository(UserManager<UserApp> userManager, SignInManager<UserApp> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CheckSignInDataAsync(LoginViewModel userData)
        {
            var user = await _userManager.FindByNameAsync(userData.UserName);
            if (user == null)
                return false;

            var signInResult = await _signInManager.PasswordSignInAsync(user, userData.Password, userData.RememberMe, lockoutOnFailure: false);
            return signInResult.Succeeded;
        }

        public async Task<IdentityResult> CreateUserAsync(UserApp newUser, string password)
        {
            var identityResult = await _userManager.CreateAsync(newUser, password);
            if (identityResult.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return identityResult;
        }

        public UserApp ConvertRegisterVMtoAppUser(RegisterViewModel userData)
        {
            return new UserApp
            {
                UserName = userData.UserName,
                PhoneNumber = userData.PhoneNumber,
                Email = userData.Email
            };
        }

        public async Task SignInAsync(UserApp userApp)
        {
            await _signInManager.SignInAsync(userApp, isPersistent: false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
