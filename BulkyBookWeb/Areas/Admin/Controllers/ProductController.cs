using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Commun.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //To access wwwroot, we need that web hosting environment, so we would have to get
        //that using dependency injection
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {

            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                     x => new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.Id.ToString()
                     }
                  ),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                     x => new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.Id.ToString()
                     }
                  ),
            };
              
            if (id == null || id==0)
            {
                return View(productVM);
                //create product
            }
            else{
                
                productVM.Product=_unitOfWork.Product.GetFirstOrDefault(x=>x.Id==id);
                return View(productVM);
                //update product
            }

        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
           
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Generate a new file name
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName); //Rename the file,but I want to keep the same extension

                    if (obj.Product.ImageUrl != null) //Delete image when it exits
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
              
                    //And finally, I need to copy the file that was uploaded inside the product folder
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        //I want to copy that file to the location that we have inside the fileStreams
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                string operation = String.Empty;
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    operation = "created";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    operation = "updated";
                }

                _unitOfWork.Save();
                TempData["Success"] = "Product "+operation+ " successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        #region API CALLS
        //He we will be calling our API calls first
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");

            return Json(new { data = productList });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath)){
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            //TempData["Success"] = "Cover Type deleted successfully";
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
