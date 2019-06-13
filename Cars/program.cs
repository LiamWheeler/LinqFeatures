﻿using System;
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

            var query = cars.Where(c => c.Manufactorer == "BMW" && c.Year == 2016 )
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            return
            File.ReadAllLines(path)
                .Skip(1)  //skips the first line with the column headers
                .Where(line => line.Length > 1) //ignores any blank lines, the last line in this case
                .Select(Car.ParseFromCsv)
                .ToList();
        }
    }
}
