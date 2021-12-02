using DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _db;
        public ProductController()
        {
            _db = new AppDbContext();
        }
        public IActionResult Index()
        {
            var data = _db.Products.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel ViewModel)
        {
            ModelState.Remove("ProductId");
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    
                    CategoryId = ViewModel.CategoryId,
                    Name = ViewModel.Name,
                    UnitPrice = ViewModel.UnitPrice,
                    Description = ViewModel.Description,
                };
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            Product products = _db.Get_Product(id);
            ViewBag.Categories = _db.Categories.ToList();
            ProductViewModel data1 = JsonConvert.DeserializeObject<ProductViewModel>(JsonConvert.SerializeObject(products));
            return View("Create", data1); // dal.product
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    ProductId = ViewModel.ProductId,
                    CategoryId = ViewModel.CategoryId,
                    Name = ViewModel.Name,
                    UnitPrice = ViewModel.UnitPrice,
                    Description = ViewModel.Description,
                };
                
                _db.Products.Update(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }

        ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        public IActionResult Delete(int id)
        {
            Product Product = _db.Products.Find(id);
            if (Product != null)
            {
                _db.Products.Remove(Product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
