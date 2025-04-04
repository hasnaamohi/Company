using Company.DAL.Models;
using Company.Mahmoud.PL.Controllers;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company.PL.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;

        public AccountController(UserManager<AppUsers> userManager,SignInManager<AppUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region signUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                     user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null) {
                        user = new AppUsers()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,

                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }


                }
                ModelState.AddModelError("", "Invaild SignUp !!");
                
            }

            return View(model);
        }

        #endregion


        #region signIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                   var flag=await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag) 
                    {
                        //sign in(token allow user to login the site)
                       var result=await _signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                       
                    }


                }
                ModelState.AddModelError("", "Invaild Login");

            }  

            return View();
        }


        #endregion


        #region signOut
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return Redirect(nameof(SignIn));
        }


        #endregion

    }
}
