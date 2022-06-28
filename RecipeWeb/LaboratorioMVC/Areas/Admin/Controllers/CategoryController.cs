using LaboratorioMVC.Models;
using LaboratorioMVC.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostEnvironment;

        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }



        public IActionResult Index()
        {
            IEnumerable<Category> objProductList = _unitOfWork.Category.GetAll();
            return View(objProductList);
        }


        //GET
        public IActionResult Upsert(int? id)
        {

            Category Category = new();


            if (id == null || id <= 0)
            {
                return View(Category);
            }
            else
            {
                Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
                return View(Category);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {

            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;


                if (obj.Id == 0)
                {
                    _unitOfWork.Category.Add(obj);
                }
                else
                {
                    _unitOfWork.Category.Update(obj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
            }
            return RedirectToAction("Index");
        }


        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Category.GetAll();
            return Json(new { data = companyList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error at deleting" });
            }


            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Company deleted successfully" });
        }
        #endregion
    }
}
