using LibraryApp.Models;
using LibraryApp.Models.Abstract;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.Controllers
{

    [Authorize(Roles = UserRoles.Role_Admin)]
    public class RentController : Controller
    {
        private readonly IRentOrderRepository _rentorderepo;
        private readonly IRentRepository _rentrepo;
        private readonly IBookRepository _bookrepo;

        public readonly IWebHostEnvironment _webHostEnviroment;

        public RentController(IRentRepository context, IBookRepository bookrepo, IWebHostEnvironment webHostEnviroment, IRentOrderRepository rentorderepo)
        {
            _rentrepo = context;
            _bookrepo = bookrepo;
            _webHostEnviroment = webHostEnviroment;
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
            List<Rent> RentList = _rentrepo.GetAll(includeprops: "Book").ToList();


            return View(RentList);
        }

        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> BookList = _bookrepo.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });

            ViewBag.Booklist = BookList;

            if (id == null || id == 0)
            {
                return View();//add
            }
            else
            {
                //update
                Rent? _rent = _rentrepo.Get(u => u.Id == id);
                if (_rent == null)
                {
                    return NotFound();
                }
                return View(_rent);
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Rent rent)
        {



            if (ModelState.IsValid)

            {





                if (rent.Id == null || rent.Id == 0)
                {
                    _rentrepo.Add(rent);

                    TempData["success"] = "New rent added succesfully:)";
                    _rentrepo.Save();

                }
                else
                {
                    _rentrepo.Update(rent);

                    TempData["success"] = "Rent edited succesfully:)";
                    _rentrepo.Save();

                }

                return RedirectToAction("Index", "Rent");
            }


            else
            {
                return View();
            }


        }

        public IActionResult Delete(int? id)
        {
            Rent _rent = _rentrepo.Get(u => u.Id == id);
            IEnumerable<SelectListItem> BookList = _bookrepo.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });
            ViewBag.BookList = BookList;

            return View(_rent);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Rent _rent)
        {
            _rentrepo.Delete(_rent);
            _rentrepo.Save();
            TempData["success"] = "Rent deleted succesfully:)";

            return RedirectToAction("Index", "Rent");

        }

        [HttpPost ("Create order")]
        public IActionResult AddFromOrder(string studentId,string bookId,int orderId)
        {
            Rent rent=new Rent();
            rent.StudentId = int.Parse(studentId);
            rent.BookId = int.Parse(bookId);
            RentOrder rentorder = _rentorderepo.Get(u => u.Id == orderId);
            rentorder.CreatedRent = true;
            _rentorderepo.Update(rentorder);

            _rentrepo.Add(rent);

            TempData["success"] = "New rent added succesfully:)";
            _rentrepo.Save();
            return RedirectToAction("Index", "Rent");

            
        }
    }
}
