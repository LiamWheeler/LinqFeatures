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

            var query = from car in cars
                        group car by car.Manufacturer into carGroup
                        select new
                        {
                            Name = carGroup.Key,
                            MaxEfficiency = carGroup.Max(c => c.Combined),
                            MinEfficiency = carGroup.Min(c => c.Combined),
                            AvgEfficiency = carGroup.Average(c => c.Combined)
                        } into result
                        orderby result.MaxEfficiency descending
                        select result;

            foreach (var carGroup in query)
            {
                Console.WriteLine($"{carGroup.Name} car efficiency:");
                Console.WriteLine($"\t Max: {carGroup.MaxEfficiency}, \r\n\t Min: {carGroup.MinEfficiency}, \r\n\t Average: {carGroup.AvgEfficiency:N2} \r\n" );
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
