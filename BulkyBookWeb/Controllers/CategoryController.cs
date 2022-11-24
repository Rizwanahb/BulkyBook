using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db; // we need implementation of this Data

        public CategoryController(ApplicationDBContext db) //constructor
        {
            _db= db;
        } 

        //Get
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Cateogries;
            return View(objCategoryList);
        }
        //Get
        public IActionResult Create()
        { 
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken] //help and prevent the cross site request forgery attack
        public IActionResult Create(Category obj)
        {
            if(obj.Name== obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display Order cannot exactly match the Name."); //to add the custome error message
            
            }
            if (ModelState.IsValid)
            {
                _db.Cateogries.Add(obj); //added to the database
                _db.SaveChanges(); //saved the changes to database 
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");  //see the updated view by redirecting to the action name
            }
            return View(obj);
        }

		//Get
		public IActionResult Edit(int? id)
		{
            if(id == null || id ==0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Cateogries.Find(id);
            //var categoryFromDbFirst = _db.Cateogries.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbSingle = _db.Cateogries.SingleOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb); //will return category to the view
		}
		//Post
		[HttpPost]
		[ValidateAntiForgeryToken] //help and prevent the cross site request forgery attack
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "Display Order cannot exactly match the Name."); //to add the custome error message

			}
			if (ModelState.IsValid)
			{
				_db.Cateogries.Update(obj); //make update to the database
				_db.SaveChanges(); //saved the changes to database 
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");  //see the updated view by redirecting to the action name
			}
			return View(obj);
		}

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Cateogries.Find(id);
            //var categoryFromDbFirst = _db.Cateogries.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbSingle = _db.Cateogries.SingleOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb); //will return category to the view
        }
        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken] //help and prevent the cross site request forgery attack
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Cateogries.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Cateogries.Remove(obj);// remove item from the database

             _db.SaveChanges(); //saved the changes to database 
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");  //see the updated view by redirecting to the action name
            }
            
        }
    }



//https://www.youtube.com/watch?v=hZ1DASYd9rk