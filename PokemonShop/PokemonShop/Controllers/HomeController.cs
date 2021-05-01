using Microsoft.AspNetCore.Mvc;

namespace PokemonShop.Controllers
{
    [Route("{action=index}")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}