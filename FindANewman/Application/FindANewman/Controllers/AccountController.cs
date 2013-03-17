using System.Web.Mvc;
using FindANewman.Common.Security;
using FindANewman.Models.Account;

namespace FindANewman.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMembershipService _membershipService;

        public AccountController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        public ActionResult LogOn()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //Validate user here and redirect to home page
                var validationResult = _membershipService.ValidateUser(viewModel.EmailAddress, viewModel.Password);
            }

            return View(viewModel);
        }

        //TODO: Implement account actions

        //[HttpGet]
        //public ActionResult LogOff()
        //{
        
        //}

        //[HttpGet]
        //public ActionResult ResetPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ResetPassword()
        //{
        //    
        //    return Redirect("LogOff");
        //}
    }
}
