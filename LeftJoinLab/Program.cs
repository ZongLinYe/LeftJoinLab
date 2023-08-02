namespace LeftJoinLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 假設已經有 orders 和 customers 的集合資料
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerId = 101, ProductName = "Product A" },
                new Order { OrderId = 2, CustomerId = 102, ProductName = "Product B" },
                new Order { OrderId = 3, CustomerId = 101, ProductName = "Product C" },
                new Order { OrderId = 4, CustomerId = null, ProductName = "Product D" },
            };

            List<Customer> customers = new List<Customer>
            {
                new Customer { CustomerId = 101, Name = "Alice" },
                new Customer { CustomerId = 102, Name = "Bob" },
                new Customer { CustomerId = 103, Name = "Charlie" },
                new Customer { CustomerId = 104, Name = "John" },

            };


            var leftJoinResult = from order in orders
                                 join customer in customers on order.CustomerId equals customer.CustomerId into customerGroup
                                 from customer in customerGroup.DefaultIfEmpty(new Customer { Name = default(string) })
                                 select new LeftJoinResult
                                 {
                                     OrderId = order.OrderId,
                                     CustomerId = customer.CustomerId,
                                     ProductName = order.ProductName,
                                     CustomerName = customer.Name
                                 };

            var leftJoinResult2 = from order in orders
                                 join customer in customers on order.CustomerId equals customer.CustomerId into customerGroup
                                 from customer in customerGroup.DefaultIfEmpty(new Customer { Name = null })
                                 select new LeftJoinResult
                                 {
                                     OrderId = order.OrderId,
                                     CustomerId = customer.CustomerId,
                                     ProductName = order.ProductName,
                                     CustomerName = customer.Name
                                 };

            var leftJoinResult3 = from order in orders
                                  join customer in customers on order.CustomerId equals customer.CustomerId into customerGroup
                                  from customer in customerGroup.DefaultIfEmpty(new Customer { Name = "N/A" })
                                  select new
                                  {
                                      OrderId = order.OrderId,
                                      CustomerId = customer.CustomerId,
                                      ProductName = order.ProductName,
                                      CustomerName = customer.Name
                                  };

            var leftJoinResult4 = from order in orders
                                  join customer in customers on order.CustomerId equals customer.CustomerId into customerGroup
                                  from customer in customerGroup.DefaultIfEmpty()
                                  select new
                                  {
                                      OrderId = order.OrderId,
                                      CustomerId = (customer == null ? "N/A" : customer.Name),
                                      ProductName = order.ProductName,
                                      CustomerName = (customer == null ? "N/A" : customer.Name)
                                  };

            var leftJoinResult5 = from order in orders
                                  join customer in customers on order.CustomerId equals customer.CustomerId into customerGroup
                                  from customer in customerGroup.DefaultIfEmpty(new Customer())
                                  select new
                                  {
                                      OrderId = order.OrderId,
                                      CustomerId = customer.CustomerId,
                                      ProductName = order.ProductName,
                                      CustomerName = customer.Name
                                  };

            foreach (var result in leftJoinResult5)
            {
                Console.WriteLine($"OrderId: {result.OrderId}, Product: {result.ProductName}, Customer: {result.CustomerName}, CustomerId: {result.CustomerId}");
            }


            Console.WriteLine("Hello, World!");

            Console.ReadKey();
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string ProductName { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }
     
    public class LeftJoinResult
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
    }
}