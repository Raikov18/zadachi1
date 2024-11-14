using FluffyCats.Data;
using FluffyCats.Data.Models;
using FluffyCats.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluffyCats.Controllers
{
    public class CatController : Controller
    {
        private readonly ApplicationDbContext db;

        public CatController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CatViewModel model)
        {
            Cats cat = new Cats
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImgUrl = model.ImgUrl,
            };
            db.Cats.Add(cat);
            db.SaveChanges();
            return Redirect("/Home/Index");

        }
        public IActionResult Details(int id)
        {
            Cats cat = db.Cats.Find(id);
            CatViewModel model = new CatViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImgUrl = cat.ImgUrl,
            };
            return View(model);

        }
        public IActionResult All()     
        {
            List<CatViewModel> model = db.Cats.Select(x => new CatViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                Breed = x.Breed,
                ImgUrl = x.ImgUrl,
            }).ToList();
                return View(model);
        }
        public IActionResult Edit(int id) //update
        {
            Cats cat = db.Cats.FirstOrDefault(x => x.Id == id);
            CatViewModel model = new CatViewModel
            {
                 Id = cat.Id,
                 Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImgUrl = cat.ImgUrl,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CatViewModel model) 
        {
             Cats cat =db.Cats.FirstOrDefault(x=>x.Id == model.Id);
            cat.Name = model.Name;
            cat.Age = model.Age;
            cat.Breed = model.Breed;
            cat.ImgUrl = model.ImgUrl;
            db.SaveChanges();
            return Redirect("/Cat/All");
        }
        public IActionResult Delete(int Id)
        {
            Cats cat =db.Cats.FirstOrDefault(X =>X.Id == Id);
            db.Cats.Remove(cat);
            db.SaveChanges();
            return Redirect("/Cat/All");
        }
    }
}
