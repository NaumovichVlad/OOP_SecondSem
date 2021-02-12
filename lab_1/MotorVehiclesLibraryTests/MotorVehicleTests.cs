using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotorVehiclesLibrary;
using System;

namespace MotorVehiclesLibraryTests
{
    [TestClass]
    public class MotorVehicleTests
    {
        [TestMethod]
        public void OverrideToString_Test()
        {
            MotorVehicle vehicle = new MotorVehicle("234", "Ferrari", 234.3, 34, 100000, "Ferrari");
            var expected = "234 Ferrari 234,3 34 100000 Ferrari";
            var actual = vehicle.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
