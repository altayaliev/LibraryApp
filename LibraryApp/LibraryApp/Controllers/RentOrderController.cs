using LibraryApp.Models;
using LibraryApp.Models.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.Controllers
{
    public class RentOrderController : Controller
    {
        private readonly IRentOrderRepository _rentorderepo;
        private readonly IBookRepository _bookrepo;
        public readonly IWebHostEnvironment _webHostEnviroment;

        public RentOrderController(IRentOrderRepository context,IBookRepository bookrepo,IWebHostEnvironment webHostEnviroment)
        {
            _rentorderepo = context;
            _bookrepo = bookrepo;
            _webHostEnviroment = webHostEnviroment;
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
            List<RentOrder> RentOrderlist = _rentorderepo.GetAll(includeprops: "Book").ToList();

            return View(RentOrderlist);
            
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
                RentOrder? _rentorder = _rentorderepo.Get(u => u.Id == id);
                if (_rentorder == null)
                {
                    return NotFound();
                }
                return View(_rentorder);
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(RentOrder rentorder)
        {



            if (ModelState.IsValid)

            {





                if (rentorder.Id == null || rentorder.Id == 0)
                {
                    _rentorderepo.Add(rentorder);

                    TempData["success"] = "New order sent succesfully:)";
                    _rentorderepo.Save();

                }
                else
                {
                    _rentorderepo.Update(rentorder);

                    TempData["success"] = "Order edited succesfully:)";
                    _rentorderepo.Save();

                }

                return RedirectToAction("Index", "RentOrder");
            }


            else
            {
                return View();
            }


        }


        public IActionResult Delete(int? id)
        {
            RentOrder _rentorder = _rentorderepo.Get(u => u.Id == id);
            IEnumerable<SelectListItem> BookList = _bookrepo.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });

            ViewBag.Booklist = BookList;

            return View(_rentorder);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(RentOrder _rentorder)
        {
            _rentorderepo.Delete(_rentorder);
            _rentorderepo.Save();
            TempData["success"] = "Order deleted succesfully:)";

            return RedirectToAction("Index", "RentOrder");

        }
        [HttpPost, ActionName("Confirm")]
        public IActionResult Confirm(int id)
        {
            RentOrder _rentorder = _rentorderepo.Get(u => u.Id == id);
            _rentorder.OrderState = true;
            _rentorderepo.Update(_rentorder);
            _rentorderepo.Save();
            TempData["success"] = "Order confirmed succesfully:)";

            return RedirectToAction("Index", "RentOrder");

        }

       
    }
}
