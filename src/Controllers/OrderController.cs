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

        private readonly ILogger<OrderController> _logger;
        private readonly OrderService _orderService;

        public OrderController(ILogger<OrderController> logger,
                               OrderService orderService)
        {            
            _logger = logger;
            _orderService = orderService;
        }

        public IActionResult Index(int id, String Status)
        {
            try
            {
                var orders = _orderService.FindOrders(id, Status);
                return View(orders);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("OrderController => Index", ex);
                throw ex;
            }           
        }


        public IActionResult Delete(int orderId)
        { 
            try
            {
                _orderService.DeleteOrder(orderId);

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("OrderController => Delete", ex);
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
                _logger.LogError("OrderController => Detail", ex);
                throw ex;
            }             
        }

    }
}
