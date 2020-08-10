using System;
using System.Collections.Generic;
using System.Linq;
using dream_holiday.Data;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Models.EntityServices
{
    /// <summary>
    /// This class handles all task related to the table Order and OrderDetail.
    /// </summary>
    public class OrderService : BaseService
    {
        private readonly UserAccountService _userAccountManager;

        public OrderService(ApplicationDbContext context, UserResolverService userService)
      : base(context, userService)
        {
            _userAccountManager = new UserAccountService(_context, userService);
        }

        /// <summary>
        /// This method remove and order from the table Order and all details related in OrderDetail
        /// </summary>
        /// <param name="orderId"></param>
        public void DeleteOrder(int orderId)
        {
            var order = _context.Order.Find(orderId);
            _context.Order.Remove(order);

            var orderDetailList = _context.OrderDetail.Where(od => od.Order.Id == orderId);
            _context.OrderDetail.RemoveRange(orderDetailList);

            _context.SaveChanges();
        }

        /// <summary>
        /// This method all order details
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderDetailViewModel> FindOrderDetails(int orderId)
        {
            var order = (from od in _context.Order
                         where od.Id == orderId
                         select od)
                          .FirstOrDefault();
            // OrderDate is static field used to store the order date
            OrderDetailViewModel.OrderDate = order.Date;
            // find all orderDetail list
            var orderDetails =
                (from od in _context.OrderDetail
                 join tp in _context.TravelPackage
                 on od.TravelPackage.Id equals tp.Id
                 where od.Order.Id == orderId
                 select new OrderDetailViewModel
                 {
                     OrderDetail = od,
                     TravelPackage = tp
                 }).ToList();


            return orderDetails;
        }

        /// <summary>
        /// This method return all list of orders filtered by OrderIs and OrderStatus
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public List<OrderViewModel> FindOrders(int orderId, String orderStatus)
        {
            IQueryable<OrderViewModel> orders = null;
            // find the current user
            var user = base.GetCurrentUser();
            OrderViewModel.isAdmin = base._userManager.IsInRoleAsync(user, Roles.ADMIN).Result;

            // If the user IS NOT an Admin we find the orders of the specific current user
            if (!OrderViewModel.isAdmin)
            {
                var userAccount = _userAccountManager.GetCurrentUserAccountAsync().Result;

                orders = _context
                            .Order
                            .Where(o => o.Customer.Id == userAccount.Id)
                            .Select(o => new OrderViewModel { Order = o });
            }
            else
            {
                // If the user IS an Admin can see all orders of any user
                // We find all the order with the reference to evey user who placed the order
                // and ordered by UserName
                orders = _context
                    .Order
                    .Join(
                    _context.UserAccount,
                        o => o.Customer.Id,
                        c => c.Id,
                        (order, customer) =>
                        new OrderViewModel
                        {
                            Order = order,
                            Customer = customer
                        }
                    )
                    .Join(_context.ApplicationUser,
                            o => o.Customer.User.Id,
                            u => u.Id,
                            (ovm, user) => new OrderViewModel
                            {
                                Order = ovm.Order,
                                Customer = ovm.Customer,
                                User = user
                            })
                    .OrderBy(x => x.User.UserName);
            }

            // filter orders by oderId
            if (orderId > 0)
            {
                orders = orders.Where(item => item.Order.Id == orderId);
            }
            // filter orders by oderStatus
            else if (!String.IsNullOrEmpty(orderStatus))
            {
                orders = orders.Where(item => item.Order.Status.Equals(orderStatus));
            }

            return orders.ToList();
        }

    }
}
