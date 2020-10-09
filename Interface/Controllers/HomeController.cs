using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Interface.Models;
using BusinessLogic.Models;

namespace Interface.Controllers
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
            return View();
        }

        [HttpGet]
        public IActionResult Getutilisateurs()
        {
            return Json(UtilisateurModel.GetAll());
        }

        [HttpGet]
        public IActionResult Getroles()
        {
            return Json(RoleModel.GetRoles());
        }

        [HttpGet]
        public IActionResult Getdepartements()
        {
            return Json(DepartementModel.GetDepartements());
        }

        [HttpPost]
        public IActionResult Createutilisateur(UtilisateurModel u,int id)
        {
            //u.DepartementId = departement;
            u.Save();
            u.Departement = DepartementModel.GetDepartement(u.DepartementId);
            return Json(u);
        }

        [HttpPost]
        public IActionResult Updateutilisateur(UtilisateurModel u)
        {
            u.Save();
            return Json(u);
        }

        [HttpPost]
        public IActionResult Deleteutilisateur(UtilisateurModel u)
        {
            u.Delete();
            return Json(u);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
