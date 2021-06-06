using Microsoft.AspNetCore.Mvc;
using WebStore.WebApp.MVC.ViewModels;

namespace WebStore.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:lenght(3, 3)}")]
        public IActionResult Errors(int id)
        {
            var errorModel = new ErrorViewModel();

            if (id == 500)
            {
                errorModel.Message = "Something went wrong! Please try it later ot contact our support team.";
                errorModel.Title = "Something went wrong!";
                errorModel.ErrorCode = id;
            }
            else if (id == 404)
            {
                errorModel.Message = "Page you are looking for does not exists! <br/> Please contact our support team if needed.";
                errorModel.Title = "Oops! Page not found.";
                errorModel.ErrorCode = id;
            }
            else if (id == 403)
            {
                errorModel.Message = "You are not allowed to do this.";
                errorModel.Title = "Access Denied.";
                errorModel.ErrorCode = id;
            }
            else
            {
                return StatusCode(500);

            }
            return View("Error", errorModel);
        }
    }
}
