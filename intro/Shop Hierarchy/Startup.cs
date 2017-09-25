using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ShopHierarchy
{
    //this project is a solution for every problem from 5 to 9
    internal class Startup
    {
        private static void Main()
        {
            using (var db = new ShopContext())
            {
                DropCreateDatabase(db);
                FillSalesmen(db);
                FillItems(db);
                FillDb(db);
//                PrintResult(db);
                PrintByOrdersReviews(db);

                int customerId = int.Parse(Console.ReadLine());
//                PrintDetailsAboutCustomer(db, customerId);
//                PrintDetails8(db, customerId);
                PrintDetails9(db, customerId);
            }
        }

        /// <summary>
        /// prints info as assigned in EX 9
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerId"></param>
        private static void PrintDetails9(ShopContext db, int customerId)
        {
            var customer = db.Customers
                .FirstOrDefault(c => c.Id == customerId);

            int ordersCount = db.Entry(customer)
                .Collection(b => b.Orders)
                .Query()
                .Count(o => o.Items.Count > 1);

            Console.WriteLine($"Orders: {ordersCount}");
        }

        /// <summary>
        /// prints info as assigned in EX 8
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerId"></param>
        private static void PrintDetails8(ShopContext db, int customerId)
        {
            var customer = db.Customers
                .FirstOrDefault(c => c.Id == customerId);

            Console.WriteLine($"Customer: {customer.Name}");

            db.Entry(customer)
                .Collection(b => b.Orders).Load();

            Console.WriteLine($"Orders count: {customer.Orders.Count}");

            db.Entry(customer)
                .Collection(b => b.Reviews).Load();

            Console.WriteLine($"Reviews: {customer.Reviews.Count}");

            db.Entry(customer)
                .Reference(c => c.Salesman).Load();

            Console.WriteLine($"Salesman: {customer.Salesman.Name}");
        }

        private static void PrintDetailsAboutCustomer(ShopContext db, int customerId)
        {
            var customer = db.Customers.FirstOrDefault(c => c.Id == customerId);

            //print orders
            foreach (var customerOrder in customer.Orders.OrderBy(o => o.Id))
            {
                Console.WriteLine($"order {customerOrder.Id}: {customerOrder.Items.Count} items");
            }

            //print review
            Console.WriteLine($"reviews: {customer.Reviews.Count}");
        }

        private static void FillItems(ShopContext db)
        {
            string cmd = string.Empty;

            while ((cmd = Console.ReadLine()) != "END")
            {
                var tokens = cmd.Split(";");
                var itemName = tokens[0];
                var itemPrice = decimal.Parse(tokens[1]);

                Item newItem = new Item(itemName, itemPrice);

                db.Add(newItem);
                db.SaveChanges();
            }
        }

        private static void PrintByOrdersReviews(ShopContext db)
        {
            var customers = db.Customers
                .Select(s => new
                {
                    name = s.Name,
                    orders = s.Orders.Count,
                    reviews = s.Reviews.Count
                })
                .OrderByDescending(s => s.orders)
                .ThenByDescending(s => s.reviews);

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.name}");
                Console.WriteLine($"Orders: {customer.orders}");
                Console.WriteLine($"Reviews: {customer.reviews}");
            }
        }

        private static void DropCreateDatabase(ShopContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        private static void PrintResult(ShopContext db)
        {
            //number of customers desc, then by name asc
            //"{salesman name} – {number of customers} customers
            var salesmen = db.Salesmen
                .Select(s => new
                {
                    numberOfCustomers = s.Customers.Count,
                    name = s.Name
                })
                .OrderByDescending(s => s.numberOfCustomers)
                .ThenBy(s => s.name);

            foreach (var salesman in salesmen)
                Console.WriteLine($"{salesman.name} - {salesman.numberOfCustomers} customers");
        }

        private static void FillDb(ShopContext db)
        {
            var cmd = string.Empty;
            var customersToRegister = new List<Customer>();

            while ((cmd = Console.ReadLine()) != "END")
            {
                var cmdTokens = cmd.Split("-");

                switch (cmdTokens[0])
                {
                    case "register":
                        RegisterCustomer(db, cmdTokens[1]);
                        break;
                    case "order":
                        MakeOrder(db, cmdTokens[1]);
                        break;
                    case "review":
                        MakeReview(db, cmdTokens[1]);
                        break;
                }
            }
        }

        private static void MakeReview(ShopContext db, string args)
        {
            var tokens = args.Split(";");

            var customer = db.Customers
                .FirstOrDefault(c => c.Id == int.Parse(tokens[0]));

            var newReview = new Review();

            //item for review
            if (tokens.Length > 1)
            {
                var itemId = int.Parse(tokens[1]);

                Item item = db.Items.FirstOrDefault(i => i.Id == itemId);
                item.Reviews.Add(newReview);
            }
            customer.Reviews.Add(newReview);

            db.Customers.Attach(customer);
            db.SaveChanges();
        }

        private static void MakeOrder(ShopContext db, string args)
        {
            var tokens = args.Split(";");

            var id = int.Parse(tokens[0]);

            var customer = db.Customers
                .FirstOrDefault(c => c.Id == id);

            var newOrder = new Order();

            //add items to order
            if (tokens.Length > 1)
            {
                for (int i = 1; i < tokens.Length; i++)
                {
                    var itemId = int.Parse(tokens[i]);
                    Item item = db.Items.FirstOrDefault(it => it.Id == itemId);

                    newOrder.Items.Add(new OrdersItems() {Item = item});
                }
            }

            customer.Orders.Add(newOrder);
            db.Customers.Attach(customer);
            db.SaveChanges();
        }

        private static void RegisterCustomer(ShopContext db, string cmd)
        {
            var tokens = cmd.Split(";");
            var name = tokens[0];
            var salesmanId = int.Parse(tokens[1]);

            var customer = new Customer()
            {
                Name = name
            };

            var result = db.Customers.Add(customer);
            var salesman = db.Salesmen.FirstOrDefault(s => s.Id == salesmanId);

            salesman.Customers.Add(result.Entity);
            db.Customers.Add(result.Entity);
            db.SaveChanges();
        }

        private static void FillSalesmen(ShopContext db)
        {
            var salesmen = Console.ReadLine().Split(";")
                .Select(s => new Salesman(s))
                .ToArray();

            db.AddRange(salesmen);
            db.SaveChanges();
        }
    }
}