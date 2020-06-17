using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCategories.Models;

namespace ProductsCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet]
        [Route("")]


        public IActionResult Index()
        {
            ViewBag.AllProducts=dbContext.Products.ToList();
            return View();
        }
        [HttpGet]
        [Route("categories")]


        public IActionResult Categories()
        {
            ViewBag.AllCategories=dbContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        [Route("processnewproduct")]


        public IActionResult NewProduct(Product newbie)
        {
            if (ModelState.IsValid)
            {
                dbContext.Products.Add(newbie);
                dbContext.SaveChanges();
                return Redirect("/");

            }
            else {
                return View("Index"); 
            }

            
        }
        [HttpPost]
        [Route("processnewcategory")]

        public IActionResult NewCategory(Category newcategory)
        {
            if (ModelState.IsValid)
            {
                dbContext.Categories.Add(newcategory);
                dbContext.SaveChanges();
                return Redirect("/categories");

            }
            else {
                return View("Categories"); 
            }

            
        }

        [HttpGet]
        [Route("products/{productId}")]

        public IActionResult ViewProduct(int productId)
        {
            ViewBag.DisplayProduct=dbContext.Products.FirstOrDefault(p=>p.ProductId==productId);

            ViewBag.AllCategories=dbContext.Categories.ToList();

            ViewBag.ProductWithCategories=dbContext.Products.Include(p=>p.ProductCategories).ThenInclude(pc=>pc.Category).FirstOrDefault(p=>p.ProductId==productId);
            


            return View();
        }

        [HttpGet]
        [Route("categories/{categoryId}")]
        public IActionResult ViewCategory(int categoryId)
        {
            ViewBag.DisplayCategory=dbContext.Categories.FirstOrDefault(c=>c.CategoryId==categoryId);

            ViewBag.AllProducts=dbContext.Products.ToList();
            ViewBag.CategoryWithProducts=dbContext.Categories.Include(c=>c.CategoryProducts).ThenInclude(cp => cp.Product).FirstOrDefault(c=>c.CategoryId==categoryId);

            


            return View();
        }

        [HttpPost]
        [Route("addproductcategory/{productId}")]

        public IActionResult AddProductCategory(int categoryId, int productId)
        {
            Association newcategory=new Association();
            newcategory.CategoryId=categoryId;
            newcategory.ProductId=productId;
            dbContext.Associations.Add(newcategory);
            dbContext.SaveChanges();


            return Redirect($"/products/{productId}");
        }

        [HttpPost]
        [Route("addcategoryproduct/{categoryId}")]

        public IActionResult AddCategoryProduct(int categoryId, int productId)
        {
            Association newproduct=new Association();
            newproduct.CategoryId=categoryId;
            newproduct.ProductId=productId;
            dbContext.Associations.Add(newproduct);
            dbContext.SaveChanges();


            return Redirect($"/categories/{categoryId}");
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
