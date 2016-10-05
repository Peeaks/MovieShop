using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AuthTest.Models;
using DLL;
using DLL.Entities;

namespace AuthTest.Controllers {
    [Authorize]
    public class ManageController : Controller {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private readonly IManager<ApplicationUser, string> _applicationUserManager = new DllFacade().GetApplicationUserManager();

        public ManageController() {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        //
        // GET: /Manage/Index
        public ActionResult Index(string message) {
            var userFound = _applicationUserManager.Read(User.Identity.GetUserId());

            return View(new IndexViewModel {ApplicationUser = userFound, Message = message});
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword() {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var result =
                await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded) {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null) {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = "Succesfully changed password" });
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult ChangeName() {
            var userFound = _applicationUserManager.Read(User.Identity.GetUserId());

            return View(new ChangeNameViewModel {FirstName = userFound.FirstName, LastName = userFound.LastName});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeName(ChangeNameViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var userFound = _applicationUserManager.Read(User.Identity.GetUserId());
            userFound.FirstName = model.FirstName;
            userFound.LastName = model.LastName;
            _applicationUserManager.Update(userFound);

            return RedirectToAction("Index", new {Message = "Succesfully changed name"});
        }

        public ActionResult ChangeAddress() {
            var userFound = _applicationUserManager.Read(User.Identity.GetUserId());

            return View(userFound.Address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAddress(Address address) {
            if (!ModelState.IsValid) {
                return View(address);
            }
            var userFound = _applicationUserManager.Read(User.Identity.GetUserId());
            userFound.Address = address;
            _applicationUserManager.Update(userFound);

            return RedirectToAction("Index", new { Message = "Succesfully changed address" });
        }


        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword() {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null) {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber() {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null) {
                return user.PhoneNumber != null;
            }
            return false;
        }

        #endregion
    }
}