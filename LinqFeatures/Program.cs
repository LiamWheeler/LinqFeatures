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

            var query = developers.Where(e => e.Name.Length >= 3)
                                  .OrderBy(e => e.Name);

            foreach (var employee in query)
            {
                Console.WriteLine(employee.Name);
            }
        }

    }
}
