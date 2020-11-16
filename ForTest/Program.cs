using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalDB;
using Entities;

namespace ForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new DbOrderItemDAO();

            var orderItem = new OrderItem()
            {
                Id = 2,
                Name = "8898988",
                OrderId = 1,
                Quantity = 22,
                Unit = "Test*****",
            };

            var test = provider.Remove(4);
            //var test = provider.GetById(-1);
        }

          
    }
}
