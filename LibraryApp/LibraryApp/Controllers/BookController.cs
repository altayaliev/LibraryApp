using LibraryApp.Models.Abstract;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using LibraryApp.Utility;
using System.Data;

namespace LibraryApp.Controllers
{
    
    public class BookController : Controller
    {
        private readonly IRentOrderRepository _rentorderepo;
        private readonly IBookRepository _bookrepo;
        private readonly IBookTypeRepository _booktyperepo;
        public readonly IWebHostEnvironment _webHostEnviroment ;

        public BookController(IBookRepository context, IBookTypeRepository booktyperepo, IWebHostEnvironment webHostEnviroment, IRentOrderRepository rentorderepo)
        {
            _bookrepo = context;
            _booktyperepo = booktyperepo;
            _webHostEnviroment = webHostEnviroment;
            _rentorderepo = rentorderepo;
        }

        [Authorize(Roles = "Admin,Student")]
        public IActionResult Index()
        {
            int count = 0;
            List<RentOrder> orders = _rentorderepo.GetAll().Where(r => r.OrderState == false).ToList();
            foreach (RentOrder order in orders)
            {
                count++;
            }
            ViewBag.count = count;
            List<Book> BookList = _bookrepo.GetAll(includeprops:"BookType").ToList();
           
            return View(BookList);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> BookTypeList = _booktyperepo.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });

            ViewBag.BookTypeList = BookTypeList;

            if (id==null || id==0)
            {
                return View();//add
            }
            else
            {
                //update
                Book? _book = _bookrepo.Get(u => u.Id == id);
                if (_book == null)
                {
                    return NotFound();
                }
                return View(_book); 
            }
        }
        
        [HttpPost]
        public IActionResult AddUpdate(Book book,IFormFile? file)
        {



            if (ModelState.IsValid)

            {
                string wwwRootpath = _webHostEnviroment.WebRootPath;
                string BookPath = Path.Combine(wwwRootpath, @"img");

                if (file!=null)
                {
                    using (var fileStream = new FileStream(Path.Combine(BookPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    book.PictureURL = @"\img\" + file.FileName;
                }

                


                if (book.Id == null || book.Id == 0)
                {
                    _bookrepo.Add(book);

                    TempData["success"] = "New book added succesfully:)";
                    _bookrepo.Save();
                    
                }
                else
                {
                    _bookrepo.Update(book);

                    TempData["success"] = "Book edited succesfully:)";
                    _bookrepo.Save();
                    
                }

                return RedirectToAction("Index", "Book");
            }


            else
            {
                return View();
            }
           









        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    { 
        //        return NotFound(); 
        //    }
        //    Book? _book = _bookrepo.Get(u => u.Id == id);
        //    if (_book == null)
        //    {
        //        return NotFound();
        //    }
        //    IEnumerable<SelectListItem> BookTypeList = _booktyperepo.GetAll().Select(k => new SelectListItem
        //    {
        //        Text = k.Name,
        //        Value = k.Id.ToString()
        //    });

        //    ViewBag.BookTypeList = BookTypeList;
        //    return View(_book);
        //}

        //[HttpPost, ActionName("Edit")]
        //public IActionResult Edit(Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _bookrepo.Update(book);
        //        _bookrepo.Save();
        //        TempData["success"] = "Book edited succesfully:)";
        //        return RedirectToAction("Index", "Book");

        //    }

        //    return RedirectToAction("Index", "Book");

        //}

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Delete(int? id)
        {
            Book _book = _bookrepo.Get(u => u.Id == id);
            IEnumerable<SelectListItem> BookTypeList = _booktyperepo.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });
            ViewBag.BookTypeList = BookTypeList;

            return View(_book);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Book _book)
        {
            _bookrepo.Delete(_book);
            _bookrepo.Save();
            TempData["success"] = "Book deleted succesfully:)";

            return RedirectToAction("Index", "Book");

        }
    }
}
