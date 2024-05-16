using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        
        private readonly ApplicationDbContext _context;  //db context
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) //Example of custom validation
            {
                ModelState.AddModelError("Name", "Display Order should not match the Name.");
            }

            
			if (obj.Name != null && obj.Name.ToLower() == "test") 
			{
				ModelState.AddModelError("", obj.Name+" is an invalid value.");
			}
			if (ModelState.IsValid)
            {
				_context.Categories.Add(obj);
				_context.SaveChanges();
                TempData["success"] = "Category created successfully!";
				return RedirectToAction("Index", "Category");

			}
            return View();
		}
        public IActionResult Edit(int? categoryId)
        {
            if (categoryId==null || categoryId==0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _context.Categories.Find(categoryId);
            //Category? categoryFromDb1 = _context.Categories.FirstOrDefault(u=>u.Id==categoryId);
            //Category? categoryFromDb2 = _context.Categories.Where(u=>u.Id==categoryId).FirstOrDefault();

            if(categoryFromDb==null){
                return NotFound();
            }

            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
			if (ModelState.IsValid)
            {
				_context.Categories.Update(obj);
				_context.SaveChanges();
				TempData["success"] = "Category updated successfully!";

				return RedirectToAction("Index");

			}
            return View();
		}


		public IActionResult Delete(int? categoryId)
		{
			if (categoryId == null || categoryId == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _context.Categories.Find(categoryId);

			if (categoryFromDb == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? categoryId)
		{
            Category obj = _context.Categories.Find(categoryId);
            if (obj==null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
			_context.SaveChanges();
			TempData["success"] = "Category deleted successfully!";

			return RedirectToAction("Index");
		}

	}
}
