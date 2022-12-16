using Entreprise.Data;
using Entreprise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Entreprise.Controllers
{
    public class TeamsController : Controller
    {
       private Unit UnitOfWork;
        public TeamsController()
        {
            this.UnitOfWork = new Unit();
        }
        public IActionResult Index()
        { 
            return View((ICollection<Department>)this.UnitOfWork.Department.GetLIST());
        }
        public IActionResult Team(string id)
        {
            ViewData["Dep"]=id;
            return View((List<User>)this.UnitOfWork.User.Find(x => x.Department.Name == id));
        }
        public IActionResult AllTeam(string id)
        { 
            return View((List<User>)this.UnitOfWork.User.GetAll());
        }
        public IActionResult Member(int id)
        { 
            return View((User)this.UnitOfWork.User.GetById(id));
        }
    }
}
