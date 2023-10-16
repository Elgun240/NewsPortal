using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practice_3.DAL;
using Practice_3.Helpers;
using Practice_3.Models;
using Practice_3.ViewModels;

namespace Practice_3.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        public AccountController(RoleManager<IdentityRole> rolemanager,
            AppDbContext db,
            IWebHostEnvironment env,
            UserManager<AppUser> usermanager,
            SignInManager<AppUser> signInManager)
        {
            _roleManager = rolemanager;
            _userManager = usermanager;
            _signInManager = signInManager;
            _env= env;
            _db= db;
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
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, false, true);
            if (loginVM.rememberMe == true)
            {
                 signInResult = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, true);
            }
            
                
            
            
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account locked out for 1 min ");
                return View();
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is wrong! ");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            if (registerVM.Photo != null)
            {
                if (registerVM.Photo == null)
                {

                    return View(registerVM);
                }
                if (!registerVM.Photo.IsImage())
                {

                    return View(registerVM);
                }
                if (registerVM.Photo.IsMore4mb())
                {

                    return View(registerVM);
                }
            }
            AppUser newUser = new AppUser
            {
                Name = "Elgun",
                Surname = "Hemzeyev",
                Email = registerVM.Email,
                UserName = registerVM.UserName,


            };
            if (registerVM.Photo != null)
            {
                
            string path = Path.Combine(_env.WebRootPath, @"admin\assets\images\users");
            registerVM.Image = await registerVM.Photo.SaveImageAsync(path);
           
            }
            else
            {
                newUser.Image = "avatar-1.jpg";
            }

            var identityResult = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());
            await _signInManager.SignInAsync(newUser, true);
            var newProfilePhoto = new ProfilePhoto();
            newProfilePhoto.UserId = newUser.Id;
            newProfilePhoto.ImagePath = registerVM.Photo == null ? newUser.Image : registerVM.Photo.FileName;
            _db.ProfilePhotos.Add(newProfilePhoto);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task CreateRole()
        {
            if (!(await _roleManager.RoleExistsAsync(Roles.Admin.ToString())))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
            }
            else if (!(await _roleManager.RoleExistsAsync(Roles.Member.ToString())))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });
            }
        }
        public async Task<IActionResult> ChangePassword()
        {


            ChangePasswordVM cpVM = new ChangePasswordVM()
            {
                User = await _userManager.FindByNameAsync(User.Identity.Name),
            };
            return View(cpVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM cpVM)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user,cpVM.Password , cpVM.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }


    }
}
