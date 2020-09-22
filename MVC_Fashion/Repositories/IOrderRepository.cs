using MVC_Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Fashion.Repositories
{
    interface IOrderRepository
    {
        int CreateOder(Order order);
        int EditOder(Order order);
        List<Order> GetListOrder();
        Order GetOrder(int id);
        int DeleteOrder(int id);
    }
}
