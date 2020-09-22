using MVC_Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Fashion.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ProductDBContext context;

        public OrderRepository(ProductDBContext context)
        {
            this.context = context;
        }
        public int CreateOder(Order order)
        {
            context.Add(order);
            return context.SaveChanges();
        }

        public int DeleteOrder(int id)
        {
            context.Remove(GetOrder(id));
            return context.SaveChanges();
        }

        public int EditOder(Order order)
        {
            context.Orders.Update(order);
            return context.SaveChanges();
        }

        public List<Order> GetListOrder()
        {
            return context.Orders.ToList();
        }

        public Order GetOrder(int id)
        {
            return context.Orders.FirstOrDefault(c => c.OrderId == id);
        }

    }
}
