using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");

            var query = from car in cars
                        where car.Manufacturer == "BMW" && car.Year == 2016
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            car.Manufacturer,
                            car.Name,
                            car.Combined
                        };

            var result = cars.Any(c => c.Manufacturer == "Ford");
            Console.WriteLine(result);

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            var query =
                File.ReadAllLines(path)
                .Skip(1)  //skips the first line with the column headers
                .Where(l => l.Length > 1) //ignores any blank lines, the last line in this case
                .ToCar();


                return query.ToList();
        }
    }
}
