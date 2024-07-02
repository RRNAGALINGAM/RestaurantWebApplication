using RestaurantWebApplication.Models;
using RestaurantWebApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebApplication.Repositories
{
    public class OrderRepository
    {
        private readonly Db_RestaurantEntities objRestaurantEntities;
        public OrderRepository()
        {
            objRestaurantEntities = new Db_RestaurantEntities();
        }
        public bool AddOrder(OrderViewModel objOrderViewModel)
        {
            Order objOrder = new Order
            {
                CustomerId = objOrderViewModel.CustomerId,
                FinalTotal = objOrderViewModel.FinalTotal,
                OrderDate = DateTime.Now,
                OrderNumber = string.Format("{0:ddmmmyyyhhmmss}", DateTime.Now),
                PaymentTypeId = objOrderViewModel.PaymentTypeId
            };
            objRestaurantEntities.Orders.Add(objOrder);
            objRestaurantEntities.SaveChanges();
            int OrderId = objOrder.OrderId;

            foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail
                {
                    OrderId = OrderId,
                    Discount = item.Discount,
                    ItemId = item.ItemId,
                    Total = item.Total,
                    UnitPrice = item.Unitprice,
                    Quantity = item.Quantity
                };
                objRestaurantEntities.OrderDetails.Add(objOrderDetail);
                objRestaurantEntities.SaveChanges();

                Transaction objTransaction = new Transaction
                {
                    Itemid = item.ItemId,
                    Quantity = (-1) * item.Quantity,
                    TransactionDate = DateTime.Now,
                    TypeId = 2
                };
                objRestaurantEntities.Transactions.Add(objTransaction);
                objRestaurantEntities.SaveChanges();
            }
            return true;
        }
    }
}