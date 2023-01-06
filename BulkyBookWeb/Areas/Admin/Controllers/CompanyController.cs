using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Commun.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
      
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new();
           
            if (id == null || id==0)
            {
                return View(company);
                //create company
            }
            else{

                company = _unitOfWork.Company.GetFirstOrDefault(x=>x.Id==id);
                return View(company);
                //update company
            }

        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
           
            if (ModelState.IsValid)
            {
                string operation = String.Empty;
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    operation = "created";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    operation = "updated";
                }

                _unitOfWork.Save();
                TempData["Success"] = "Company "+operation+ " successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        #region API CALLS
        //He we will be calling our API calls first
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();

            return Json(new { data = companyList });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            
            var obj = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
           
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
