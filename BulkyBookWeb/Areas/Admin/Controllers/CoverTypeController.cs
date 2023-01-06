using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Commun.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
           IEnumerable<CoverType> coverTypelist= _unitOfWork.CoverType.GetAll();

            return View(coverTypelist);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
          
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Cover Type created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                TempData["Error"] = "Id Null Error";
                //return View("Error");
                return NotFound();
            }
            var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(x=>x.Id==id);
            if (coverTypeFromDb == null)
            {
                TempData["Error"] = "This Cover Type is not found";
                //return View("Error");
                return NotFound();
            }

            return View(coverTypeFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Cover Type updated successfully";
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
                //return View("Error");
                return NotFound();
            }
            var coverTypeFromDb= _unitOfWork.CoverType.GetFirstOrDefault(x=>x.Id==id);
            if(coverTypeFromDb == null)
            {
                TempData["Error"] = "This Cover Type is not found";
                //return View("Error");
                return NotFound();
            }
            return View(coverTypeFromDb);
        }

        //POST
        [HttpPost]
        public IActionResult Delete(CoverType obj)
        {
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Cover Type deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
