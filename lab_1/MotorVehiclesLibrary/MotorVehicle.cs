using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorVehiclesLibrary
{
    public class MotorVehicle
    {
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public double EnginePower { get; set; }
        public int EngineCapacity { get; set; }
        public int Price { get; set; }
        public string Manufacturer { get; set; }

        public MotorVehicle (string articleNumber, string name, double enginePower, int engineCapacity, int price, string manufacturer)
        {
            ArticleNumber = articleNumber;
            Name = name;
            EnginePower = enginePower;
            EngineCapacity = engineCapacity;
            Price = price;
            Manufacturer = manufacturer;
        }

        public override string ToString()
        {
            var message = string.Format("{0} {1} {2} {3} {4} {5}", ArticleNumber, Name, EnginePower, EngineCapacity, Price, Manufacturer);
            return message;
        }
    }
}
