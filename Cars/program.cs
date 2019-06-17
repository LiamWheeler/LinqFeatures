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
            var cars = ProcessCars("fuel.csv");
            var manufacturers = PreocessManufacturers("manufacturers.csv");

            //var query = from car in cars
            //            join manufacturer in manufacturers 
            //            on car.Manufacturer equals manufacturer.Name
            //            orderby car.Combined descending, car.Name ascending
            //            select new
            //            {
            //                manufacturer.Headquarters,
            //                car.Name,
            //                car.Combined
            //            };

            var query = from manufacturer in manufacturers
                        join car in cars on manufacturer.Name equals car.Manufacturer
                        into carGroup
                        orderby manufacturer.Name.ToUpper()
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carGroup
                        };

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} has efficiency {car.Combined}");
                }
            }


            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //} 
        }

        private static List<Manufacturer> PreocessManufacturers(string path)
        {
            var query = File.ReadAllLines(path)
                .Where(l => l.Length > 1)
                .Select(l => {
                    var columns = l.Split(',');
                    return new Manufacturer
                    {
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2])
                    };
                });
            return query.ToList();                    
        }

        private static List<Car> ProcessCars(string path)
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
