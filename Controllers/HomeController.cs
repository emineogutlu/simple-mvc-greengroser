using Greengrocer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Greengrocer.Models;
namespace Template.Controllers
{
    public class HomeController : Controller
    {
        GreengrocerEntities greengrocerEntities = new GreengrocerEntities();
      
        public ActionResult Index(string typeOfFood = "All of them")
        {
            if (typeOfFood.Equals("All of them"))
            {
                ViewBag.total = greengrocerEntities.Foods.Select(a => a.Price).Sum();
                return View(greengrocerEntities.Foods.OrderBy(a => a.Price).ToList());
            }
            else
            {
                ViewBag.total = greengrocerEntities.Foods.Where(a => a.TypeOfFood.Equals(typeOfFood)).Select(a => a.Price).Sum();
                return View(greengrocerEntities.Foods.Where(a => a.TypeOfFood.Equals(typeOfFood)).OrderBy(a => a.Price).ToList());
            }

        }
        public ActionResult Login()
        {
            ViewBag.error = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            User loginUser = greengrocerEntities.Users.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();
            if (loginUser != null)
                return RedirectToAction("Index");
            ViewBag.error = "Hatalı giriş";
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Food newFood)
        {

            greengrocerEntities.Foods.Add(newFood);
            greengrocerEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int FoodId)
        {
            Food deleted = greengrocerEntities.Foods.Find(FoodId);
            greengrocerEntities.Foods.Remove(deleted);
            greengrocerEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int FoodId)
        {
            Food updated = greengrocerEntities.Foods.Find(FoodId);
            return View(updated);
        }
        [HttpPost]
        public ActionResult Update(int FoodId, string FoodName, string TypeOfFood, double Price)
        {
            Food updated = greengrocerEntities.Foods.Find(FoodId);

            updated.FoodName = FoodName;
            updated.TypeOfFood = TypeOfFood;
            updated.Price = Price;
            greengrocerEntities.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}