using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practice_3.Helpers;
using Practice_3.Models;
using Practice_3.ViewModels;

namespace Practice_3.Controllers
{
    public class AccountController : Controller
    {
        private readonly    RoleManager<IdentityRole> _roleManager;
        private readonly    UserManager<AppUser> _userManager;
        private readonly    SignInManager<AppUser> _signInManager;
        public AccountController(RoleManager<IdentityRole> rolemanager,
            UserManager<AppUser> usermanager,
            SignInManager<AppUser> signInManager)
        {
            _roleManager = rolemanager;
            _userManager = usermanager;
            _signInManager = signInManager;
            
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("News", "Email or password is wrong! ");
                return View();
            }
            if (appUser.IsDeactive)
            {
                ModelState.AddModelError("", "Your account has been blocked ");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult= await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true,true );
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account locked out for 1 min ");
                return View();
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Your account locked out for 1 min ");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM )
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser newUser = new AppUser
            {
                Name = "Elgun",
                Surname = "Hemzeyev",
                Email = registerVM.Email,
                UserName = registerVM.UserName,
            };
           var identityResult= await _userManager.CreateAsync(newUser, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            } 
            await _userManager.AddToRoleAsync(newUser , Roles.Member.ToString());
            await _signInManager.SignInAsync(newUser , true );
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
           return RedirectToAction("Index","Home");
        }
        public async Task CreateRole()
        {
            if(!(await _roleManager.RoleExistsAsync(Roles.Admin.ToString())))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name= Roles.Admin.ToString() });
            }
            else if (!(await _roleManager.RoleExistsAsync(Roles.Member.ToString())))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });
            }    
        }

    }
}
