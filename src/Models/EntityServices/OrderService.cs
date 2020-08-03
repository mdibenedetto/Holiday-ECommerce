using System;
using System.Collections.Generic;
using System.Linq;
using dream_holiday.Data;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Models.EntityServices
{
    public class OrderService : BaseService
    {
        private readonly UserAccountService _userAccountManager;

        public OrderService(ApplicationDbContext context, UserResolverService userService)
      : base(context, userService)
        {
            _userAccountManager = new UserAccountService(_context, userService);
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Order.Find(orderId);
            _context.Order.Remove(order);

            var orderDetailList = _context.OrderDetail.Where(od => od.Order.Id == orderId);
            _context.OrderDetail.RemoveRange(orderDetailList);

            _context.SaveChanges();
        }

        internal List<OrderDetailViewModel> FindOrderDetails(int orderId)
        {
            var order = (from od in _context.Order
                         where od.Id == orderId
                         select od)
                          .FirstOrDefault();

            OrderDetailViewModel.OrderDate = order.Date;

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

        public List<OrderViewModel> FindOrders(int id, String status)
        {
            IQueryable<OrderViewModel> orders = null;


            var user = base.GetCurrentUser();
            OrderViewModel.isAdmin = base._userManager.IsInRoleAsync(user, Roles.ADMIN).Result;

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
                    .OrderBy(x=>  x.User.UserName);
            }

            // filter orders by oderId
            if (id > 0)
            {
                orders = orders.Where(item => item.Order.Id == id);
            }
            // filter orders by oderStatus
            else if (!String.IsNullOrEmpty(status))
            {
                orders = orders.Where(item => item.Order.Status.Equals(status));
            }

            return orders.ToList();
        }

    }
}
