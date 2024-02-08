using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(ObjCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Server side validation(The page refreshes)
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //In the Find method you can pass only the primary key of the model
            Category? categoryfromdb = _unitOfWork.Category.Get(u => u.Id == id);
            //Best method to get a value from db, also you can use if u=>u.Name.Contains() method
            //Category? categoryfromdb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //If you need some extra calculations you should use this one
            //Category? categoryfromdb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryfromdb == null)
            {
                return NotFound();
            }

            return View(categoryfromdb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryfromdb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }

            return View(categoryfromdb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (ModelState.IsValid)
            {
                Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
