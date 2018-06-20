using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDelegate
{
    class Program
    {
        public delegate int MyDelegate(int x, int y);
        public delegate void MyDelegate2(string msg);

        static void Main(string[] args)
        {
            Action<string> action = new Action<string>(Display);
            action("Hello!");

            Func<int, double> func = new Func<int, double>(CalculateHra);
            Console.WriteLine(func(50000));

            List<Customer> custList = new List<Customer>();
            custList.Add(new Customer
            {
                Id = 1,
                FirstName = "Stephen",
                LastName = "Oscar",
                State = "China",
                City = "Shenzhen",
                Addresss = "Manshan0",
                country = "China"
            });

            custList.Add(new Customer
            {
                Id = 2,
                FirstName = "Lisahn",
                LastName = "Salah",
                State = "HUBEI",
                City = "Shenzhen",
                Addresss = "Manshan0",
                country = "China"
            });

            Predicate<Customer> hydCustomer = x => x.Id == 1;
            Customer customer = custList.Find(hydCustomer);
            Console.WriteLine(customer.FirstName);
            Console.Read();



            MyDelegate d = new MyDelegate(Sum);
            int result = d.Invoke(12, 15);
            Console.WriteLine(result);
            Console.ReadLine();

            MyDelegate2 d2 = new MyDelegate2(ShowText);
            d2("Hello world...");
            Console.ReadLine();


        }

        static void Display(string message)
        {
            Console.WriteLine(message);
        }

        static double CalculateHra(int basic) {
            return (double)(basic * 4);
        }

        static int Sum(int x, int y)
        {
            return x + y;
        }


        public static void ShowText(string text)
        {
            Console.WriteLine(text);
        }

    }

    class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Addresss { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string country { get; set; }
    }
}
