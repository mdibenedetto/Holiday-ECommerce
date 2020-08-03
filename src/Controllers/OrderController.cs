using System;
using System.Collections.Generic; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using dream_holiday.Models.EntityServices;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Controllers
{
 
    [Authorize]
    public class OrderController : Controller
    {
        // todo: remove all code after test are done
        //private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderController> _logger;
        private readonly OrderService _orderService;

        public OrderController(
            //ApplicationDbContext context,
                                ILogger<OrderController> logger,
                               OrderService orderService)
        {
            //_context = context;
             _logger = logger;
            _orderService = orderService;
        }

        public IActionResult Index(int id, String Status)
        {
            try
            {
               var orders =  _orderService.FindOrders(id, Status);
               return View(orders);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Order => Index", ex);
                throw ex;
            }
            // todo: remove all code after test are done
            ////here is list
            //List<Order> oders;

            ////if id is not 
            //if (id > 0)
            //{
            //    oders = _context.Order
            //        .Where(item => item.Id == id)
            //        .ToList();
            //}
            ////if the status is not empty
            //else if (!String.IsNullOrEmpty(Status))
            //{
            //    oders = _context.Order
            //         .Where(item => item.Status.Equals(Status))
            //           .ToList();
            //}
            //else
            //{
            //    oders = _context.Order.ToList();
            //}

            //return View(oders);
        }


        public IActionResult Delete(int orderId)
        {
            // todo: remove all code after test are done
            //var order = _context.Order.Find(orderId);
            //_context.Order.Remove(order);

            //var orderDetailList = _context.OrderDetail.Where(od => od.Order.Id == orderId);
            //_context.OrderDetail.RemoveRange(orderDetailList);

            //_context.SaveChanges();orderId
            try
            {
                _orderService.DeleteOrder(orderId);

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Order => Delete", ex);
                throw ex;
            }
                     

            return RedirectToAction(nameof(Index), new
            {
                id = 0,
                Status = ""
            });
        }

        public IActionResult Detail(int orderId)
        {

            try
            { 
              List<OrderDetailViewModel> orderDetails =
                    _orderService.FindOrderDetails(orderId);

                ViewBag.orderId = orderId;
                ViewBag.orderDate = OrderDetailViewModel.OrderDate;

                return View(orderDetails);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Order => Detail", ex);
                throw ex;
            }

            // todo: remove all code after test are done
            //var order = (from od in _context.Order
            //             where od.Id == orderId
            //             select od)
            //             .FirstOrDefault();

            //ViewBag.orderId = orderId;
            //ViewBag.orderDate = order?.Date;

            //var orderDetails =
            //    (from od in _context.OrderDetail
            //     join tp in _context.TravelPackage
            //     on od.TravelPackage.Id equals tp.Id
            //     where od.Order.Id == orderId
            //     select new OrderDetailModel
            //     {
            //         OrderDetail = od,
            //         TravelPackage = tp
            //     }).ToList();

            //return View(orderDetails);
        }

    }
}
