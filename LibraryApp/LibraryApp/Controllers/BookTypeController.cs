using LibraryApp.Models;
using LibraryApp.Models.Abstract;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{

    [Authorize(Roles = UserRoles.Role_Admin)]
    public class BookTypeController : Controller
    {
        private readonly IRentOrderRepository _rentorderepo;
        private readonly IBookTypeRepository _booktyperepo;

        public BookTypeController(IBookTypeRepository context, IRentOrderRepository rentorderepo)
        {
            _booktyperepo = context;
            _rentorderepo = rentorderepo;
        }
        public IActionResult Index()
        {
            int count = 0;
            List<RentOrder> orders = _rentorderepo.GetAll().Where(r => r.OrderState == false).ToList();
            foreach (RentOrder order in orders)
            {
                count++;
            }
            ViewBag.count = count;
            List<BookType> BookTypesList = _booktyperepo.GetAll().ToList() ;
            return View(BookTypesList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookType booktype) 
        {
            if (ModelState.IsValid)
            {
               _booktyperepo.Add(booktype);
                _booktyperepo.Save();
                TempData["success"] = "New book added succesfully:)";
                return RedirectToAction("Index", "BookType");

            }

            return View();

        }


        public IActionResult Edit(int? id)
        {
            if (id==null||id==0) { return NotFound(); }
            BookType? _booktype=_booktyperepo.Get(u => u.Id==id);
            if (_booktype==null)
            {
                return NotFound();
            }
            return View(_booktype);
        }

        [HttpPost,ActionName("Edit")]
        public IActionResult Edit(BookType booktype)
        {
            if (ModelState.IsValid)
            {
                _booktyperepo.Update(booktype);
                _booktyperepo.Save();
                TempData["success"] = "Book Type edited succesfully:)";
                return RedirectToAction("Index", "BookType");

            }

            return RedirectToAction("Index", "BookType");

        }

        
        public IActionResult Delete(int? id)
        {
            BookType _booktype = _booktyperepo.Get(u => u.Id == id);

            return View(_booktype);

        }
        [HttpPost,ActionName("Delete")]
        public IActionResult Delete(BookType _booktype)
        {
            _booktyperepo.Delete(_booktype);
            _booktyperepo.Save();
            TempData["success"] = "Book Type deleted succesfully:)";

            return RedirectToAction("Index", "BookType");

        }
    }
}
