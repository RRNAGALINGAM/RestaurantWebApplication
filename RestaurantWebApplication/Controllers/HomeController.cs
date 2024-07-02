using RestaurantWebApplication.Models;
using RestaurantWebApplication.Repositories;
using RestaurantWebApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestaurantWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly Db_RestaurantEntities objRestaurantEntities;
        public HomeController()
        {
            objRestaurantEntities = new Db_RestaurantEntities();
        }

        // GET: Home
        public ActionResult Index()
        {
            CustomerRepository ObjCustomerRepository = new CustomerRepository();

            ItemRepository ObjItemRepository = new ItemRepository();

            PaymentTypeRepository ObjPaymentTypeRepository = new PaymentTypeRepository();

            var objectMultipleModels = new Tuple<IEnumerable<SelectListItem>,
                IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (ObjCustomerRepository.GetAllCustomers(), ObjItemRepository.GetAllItems(),
                ObjPaymentTypeRepository.GetAllPaymentType());

            return View(objectMultipleModels);
        }

        [HttpGet]
        public JsonResult GetItemUnitPrice(int itemId)
        {
            decimal UnitPrice = objRestaurantEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            objOrderRepository.AddOrder(objOrderViewModel);
            return Json("Your Order has been Successfully Placed.", JsonRequestBehavior.AllowGet);
        }
    }
}