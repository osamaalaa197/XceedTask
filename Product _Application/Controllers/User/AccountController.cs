using Microsoft.AspNetCore.Mvc;
using Product__Application.Repository.UserRepository;
using Product__Application.ViewModel;
using System.Threading.Tasks;

namespace Product__Application.Controllers.User
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRegister(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.ConvertRegisterVMtoAppUser(registerViewModel);
                var result = await _userRepository.CreateUserAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await _userRepository.SignInAsync(user);
                    return RedirectToAction("GetAllProduct", "Product");
                }

                // Handle registration errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("Register", registerViewModel);
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLogin(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var isSignInSuccessful = await _userRepository.CheckSignInDataAsync(loginViewModel);
                if (isSignInSuccessful)
                {
                    return RedirectToAction("GetAllProduct", "Product");
                }

                // Handle login errors
                ModelState.AddModelError("", "Username or password is not correct.");
            }

            return View("LogIn", loginViewModel);
        }

        public async Task<IActionResult> SignOut()
        {
            await _userRepository.SignOutAsync();
            return RedirectToAction("GetAllProduct", "Product");
        }
    }
}
