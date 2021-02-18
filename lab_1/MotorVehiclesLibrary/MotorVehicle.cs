using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorVehiclesLibrary
{
    /// <summary>
    /// Class for storing information about motorcycles
    /// </summary>
    public class MotorVehicle
    {
        /// <summary>
        /// Article
        /// </summary>
        public string ArticleNumber { get; set; }
        /// <summary>
        /// Type of motorcycle
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Engine power in double
        /// </summary>
        public double EnginePower { get; set; }
        /// <summary>
        /// Engine capacity in int
        /// </summary>
        public int EngineCapacity { get; set; }
        /// <summary>
        /// Motorcycle price
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// The manufacturer of the motorcycle
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Filling in properties
        /// </summary>
        /// <param name="articleNumber">Article</param>
        /// <param name="name">Type of motorcycle</param>
        /// <param name="enginePower">Engine power in double</param>
        /// <param name="engineCapacity">Motorcycle price</param>
        /// <param name="price">The manufacturer of the motorcycle</param>
        /// <param name="manufacturer">The manufacturer of the motorcycle</param>
        public MotorVehicle (string articleNumber, string name, double enginePower, int engineCapacity, int price, string manufacturer)
        {
            ArticleNumber = articleNumber;
            Name = name;
            EnginePower = enginePower;
            EngineCapacity = engineCapacity;
            Price = price;
            Manufacturer = manufacturer;
        }
        /// <summary>
        /// Output of all information
        /// </summary>
        /// <returns>All properties</returns>
        public override string ToString()
        {
            var message = string.Format("{0} {1} {2} {3} {4} {5}", ArticleNumber, Name, EnginePower, EngineCapacity, Price, Manufacturer);
            return message;
        }
    }
}
