using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using LinqFeatures.MyLinq;

namespace LinqFeatures
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<int, int> square = x => x*x;
            Func<int, int, int> add = (x, y) => x + y;
            Action<int> write = x => Console.WriteLine(x);
            write(square(add(3, 5)));


            var developers = new Employee[]
            {
                new Employee {Id = 1, Name = "Liam"},
                new Employee {Id = 2, Name = "Warren"}
            };
            var sales = new List<Employee>()
            {
                new Employee {Id = 3, Name = "Dec"}
            };

            var quers = developers.Where(e => e.Name.Length > 3)
                                  .OrderByDescending(e => e.Name)
                                  .Select(e => e);

            var query2 =
                from developer in developers
                where developer.Name.Length > 3
                orderby developer.Name descending
                select developer;

            foreach (var employee in query2)
            {
            Console.WriteLine(employee.Name);
            }

            
        }

    }
}
