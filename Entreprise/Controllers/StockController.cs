using Entreprise.Data;
using Entreprise.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;

namespace Entreprise.Controllers
{
    public class StockController : Controller
    {
        private Unit UnitOfWork;
        public StockController()
        {
            this.UnitOfWork = new Unit();
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Status") == "logged")
            {
                ViewData["logged"] = "true";
                return View((ICollection<Stock>)this.UnitOfWork.Stock.GetAll());

            }
            return Redirect("/login");
            
        } 
      

        public IActionResult AllProduct(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged")
            {

                ViewData["role"] = this.UnitOfWork.User.getByID(Convert.ToInt32(HttpContext.Session.GetString("pid"))).ROLE;
                ViewBag.Category = this.UnitOfWork.Category.GetAll();
                ViewData["logged"] = "true";
                return View(this.UnitOfWork.Product.GetbyStock(this.UnitOfWork.Stock.GetById(id)));
      
            }
            return Redirect("/login");
        }

        

        [HttpGet]
        public IActionResult CreateProduct()
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                
                ViewBag.Vendor = this.UnitOfWork.Vendor.GetAll();
                ViewBag.Stock = this.UnitOfWork.Stock.GetAll();
                ViewBag.Category = this.UnitOfWork.Category.GetAll();
                ViewData["logged"] = "true";
                return View();

            }
            return Redirect("/login");
        }

        [HttpPost]
        public IActionResult CreateProduct(MyViewModelProduct p)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                p.product.Vendor = this.UnitOfWork.Vendor.GetById(p.vendor);
                p.product.Category = this.UnitOfWork.Category.GetById(p.category);
                p.product.Stock = this.UnitOfWork.Stock.GetById(p.stock);
                this.UnitOfWork.Product.Add(p.product);
                this.UnitOfWork.Vendor.GetById(p.vendor).Products.Add(p.product);
                this.UnitOfWork.Stock.GetById(p.stock).Products.Add(p.product);
                this.UnitOfWork.Category.GetById(p.category).Products.Add(p.product);
                this.UnitOfWork.complete();
                return RedirectToAction("AllProduct", new {id = p.stock });

            }
            return Redirect("/login");
        }


        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {

                ViewBag.Vendor = this.UnitOfWork.Vendor.GetAll();
                ViewBag.Stock = this.UnitOfWork.Stock.GetAll();
                ViewBag.Category = this.UnitOfWork.Category.GetAll();
                ViewData["logged"] = "true";
                Product p= new Product();
                p = this.UnitOfWork.Product.GetById(id); 
                return View(p);

            }
            return Redirect("/login");
        }

        [HttpPost]
        public IActionResult EditProduct(Product p)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                Product pn=  new Product();
                pn =  this.UnitOfWork.Product.GetById(p.Id);
                pn.Id = p.Id;

                pn.Name = p.Name;

                pn.Description = p.Description;

                pn.Reference = p.Reference;
                pn.Bought_Price = p.Bought_Price;
                pn.Sold_Price = p.Sold_Price;
                pn.Sold = p.Sold;
                pn.Left = p.Left; 
                this.UnitOfWork.complete(); 
                return RedirectToAction("AllProduct", new { id =1 });

            }
            return Redirect("/login");
        }

        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged")
            {
                ViewData["role"] = this.UnitOfWork.User.getByID(Convert.ToInt32(HttpContext.Session.GetString("pid"))).ROLE;

                ViewData["logged"] = "true"; 
                return View(this.UnitOfWork.Product.GetById(id));

            }
            return Redirect("/login");
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                int idstock = this.UnitOfWork.Product.GetById(id).Stock.Id;
                this.UnitOfWork.Product.Remove((Product)this.UnitOfWork.Product.GetById(id));

                ViewData["logged"] = "true";
                this.UnitOfWork.complete();
                return RedirectToAction("AllProduct", new { id = idstock });
            }
                return Redirect("/login");

        }
        public IActionResult Vendor()
        {
            if (HttpContext.Session.GetString("Status") == "logged")
            {
                ViewData["role"] = this.UnitOfWork.User.getByID(Convert.ToInt32(HttpContext.Session.GetString("pid"))).ROLE;
                ViewData["logged"] = "true";
                return View(this.UnitOfWork.Vendor.GetAll());

            }
            return Redirect("/login");
        }
        public IActionResult VendorProducts(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged")
            {
                ViewData["role"] = this.UnitOfWork.User.getByID(Convert.ToInt32(HttpContext.Session.GetString("pid"))).ROLE;
                ViewData["logged"] = "true";
                return View(this.UnitOfWork.Product.GetbyVendor(this.UnitOfWork.Vendor.GetById(id)));
                //return View(this.UnitOfWork.Vendor.GetById(id).Products);
            }
            return Redirect("/login");
        }

        public IActionResult DeleteVendor(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {

                this.UnitOfWork.Vendor.Remove((Vendor)this.UnitOfWork.Vendor.GetById(id));
                 ViewData["logged"] = "true";
                this.UnitOfWork.complete();
                return RedirectToAction("Vendor");
            }
            return Redirect("/login");

        }
        [HttpGet]
        public IActionResult AddVendor()
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                
                ViewData["logged"] = "true";
                this.UnitOfWork.complete();
                return View();
            }
            return Redirect("/login");
        }
        [HttpPost]
        public IActionResult AddVendor(Vendor v)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                this.UnitOfWork.Vendor.Add(v);
                ViewData["logged"] = "true";
                this.UnitOfWork.complete();
                return RedirectToAction("Vendor");
            }
            return Redirect("/login");
        }
        [HttpGet]
        public IActionResult EditVendor(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {

                ViewData["logged"] = "true";
         
                return View(this.UnitOfWork.Vendor.GetById(id));

            }
            return Redirect("/login");
        }

        [HttpPost]
        public IActionResult EditVendor(Vendor v)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                this.UnitOfWork.Vendor.UpdateVendor(v);
                this.UnitOfWork.complete();
                return RedirectToAction("Vendor");

            }
            return Redirect("/login");
        }

        public IActionResult Category()
        {
            if (HttpContext.Session.GetString("Status") == "logged")
            {
                ViewData["role"] = this.UnitOfWork.User.getByID(Convert.ToInt32(HttpContext.Session.GetString("pid"))).ROLE;
                ViewData["logged"] = "true";
                return View(this.UnitOfWork.Category.GetAll());

            }
            return Redirect("/login");
        }
        public IActionResult CategoryProducts(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged")
            {
                ViewBag.Category = this.UnitOfWork.Category.GetAll();
                ViewData["role"] = this.UnitOfWork.User.getByID(Convert.ToInt32(HttpContext.Session.GetString("pid"))).ROLE;
                ViewData["logged"] = "true";
                ViewData["currentcategory"] = this.UnitOfWork.Category.GetById(id).Name;
                return View(this.UnitOfWork.Product.GetbyCategory(this.UnitOfWork.Category.GetById(id)));
                //return View(this.UnitOfWork.Category.GetById(id).Products);

            }
            return Redirect("/login");
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {

                ViewData["logged"] = "true";
                this.UnitOfWork.complete();
                return View();
            }
            return Redirect("/login");
        }
        [HttpPost]
        public IActionResult AddCategory(Category c)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                this.UnitOfWork.Category.Add(c);
                ViewData["logged"] = "true";
                this.UnitOfWork.complete();
                return RedirectToAction("Category");
            }
            return Redirect("/login");
        }

        public IActionResult DeleteCategory(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {

                this.UnitOfWork.Category.Remove((Category)this.UnitOfWork.Category.GetById(id));
                ViewData["logged"] = "true";
                this.UnitOfWork.complete();
                return RedirectToAction("Category");
            }
            return Redirect("/login");

        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {

                ViewData["logged"] = "true";

                return View(this.UnitOfWork.Category.GetById(id));

            }
            return Redirect("/login");
        }

        [HttpPost]
        public IActionResult EditCategory(Category c)
        {
            if (HttpContext.Session.GetString("Status") == "logged" && HttpContext.Session.GetString("role") == "CEO")
            {
                this.UnitOfWork.Category.UpdateCategory(c);
                this.UnitOfWork.complete();
                return RedirectToAction("Category");

            }
            return Redirect("/login");
        }
    }
}
