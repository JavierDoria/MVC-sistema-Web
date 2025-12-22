using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Managers;
using MoneyFlow.Models;

namespace MoneyFlow.Controllers
{
    public class AcountController(UserManager _userManager) : Controller
    {
        public IActionResult Login()
        {
            var viewModel = new LoginVM();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Login(LoginVM viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);
            var fount = _userManager.Login(viewModel);
            if (fount.UserId == 0)
            {
                ViewBag.message = " DATOS INCORRECTOS";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
       public IActionResult Register()
        {
            var viewModel = new UserVM();
            return View();  
        }
        [HttpPost]
        public IActionResult Register(UserVM viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var response = _userManager.Register(viewModel);
                if(response != 0)
                {
                    ViewBag.message = "Su cuenta esta registrada, intente logearse";
                    ViewBag.Class = "alert-success";
                    ViewBag.Redirect = true;
                    return View("Register");
                    //return RedirectToAction("Login", "Acount");
                }
                else
                {
                    ViewBag.message = "Su cuenta no puede ser registrada";
                    ViewBag.Class = "alert-danger";
                }
            }
            catch (Exception ex) {
                ViewBag.message =ex.Message;
                ViewBag.Class = "alert-danger";
            }
            return View();
        }
    }
}
