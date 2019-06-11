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


            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee {Id = 1, Name = "Liam"},
                new Employee {Id = 2, Name = "Warren"}
            };
            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee {Id = 3, Name = "Dec"}
            };

            foreach (var employee in developers.Where(e => e.Name.Length >= 3)
                                               .OrderBy(e => e.Name))
            {
                Console.WriteLine(employee.Name);
            }
        }

    }
}
