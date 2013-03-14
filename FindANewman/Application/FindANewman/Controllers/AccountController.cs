using System.Web.Mvc;
using FindANewman.Models.Account;

namespace FindANewman.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
            //Use dependency injection here
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
