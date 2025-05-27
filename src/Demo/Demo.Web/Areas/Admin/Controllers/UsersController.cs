using Demo.Infrastructure;
using Demo.Infrastructure.Identity;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy= "UserAddPermission")]
    public class UsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailStore = GetEmailStore();
        }
        public async Task<ActionResult> AddUserAsync()
        {
            var model = new AddUserModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUserAsync(AddUserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = CreateUser();
                    user.RegistrationDate = DateTime.Now;
                    await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                    var result = await _userManager.CreateAsync(user, model.Password);
                    await _userManager.AddToRoleAsync(user, model.Role);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "User Created",
                        Type = ResponseTypes.Success,
                    });
                }
            }
            catch (Exception ex)
            {
                var message = "User Created Filed";
                ModelState.AddModelError("User Created Failed",message);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = message,
                    Type = ResponseTypes.Danger,
                });
            }
           
            return View(model);
        }

       

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
