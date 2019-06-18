﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateXml();
        }

        private static void CreateXml()
        {
            var records = ProcessCars("fuel.csv");
            var document = new XDocument();

            var cars = new XElement("Cars",
                from record in records
                select new XElement("Car",
                    new XAttribute("Name", record.Name),
                    new XAttribute("Combined", record.Combined),
                    new XAttribute("Manufacturer", record.Manufacturer)
                    ));

            document.Add(cars);
            document.Save("fuel.xml");
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
