using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =Commun.Role_Admin)]
    public class CategoryController : Controller
    {
        //Dependency injection
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            /*List<Category> objCategoryList = _unitOfWork.Categories.ToList();*/
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {

                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Id Null Error";
                //return View("Error");
                return NotFound();
            }
            //var categoryFromDb = _unitOfWork.Categories.Find(id);
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            //var categoryFromDb = _unitOfWork.Categories.SingleOrDefault(x => x.Id == id);

            if (categoryFromDb == null)
            {
                TempData["Error"] = "This Category is not found";
                //return View("Error");
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                //_unitOfWork.Add(obj);
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Id Null Error";
                return View("Error");
            }
            //var categoryFromDb = _unitOfWork.Categories.Find(id);
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (categoryFromDb == null)
            {
                TempData["Error"] = "This Category is not found";
                //return View("Error");
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }

}