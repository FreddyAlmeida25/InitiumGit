using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Initium.Models;

namespace Initium.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<DAL.Entity.ClienteEntity> model = BLL.ClienteOperation.GetList();
            ViewBag.cola1 = model.Where(i => i.idcola == 1).OrderByDescending(g => g.fechafin).ToList();
            ViewBag.cola2 = model.Where(i => i.idcola == 2).OrderByDescending(g => g.fechafin).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Guardar(string cedula, string nombre)
        {
            BLL.ClienteOperation.Create(cedula, nombre);
            return RedirectToAction("Index", "Home");
        }
        #region
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

    }
}
