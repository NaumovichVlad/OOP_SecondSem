using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotorVehiclesLibrary;
using System;
using System.Collections.Generic;
using ExceptionLibrary;

namespace MotorVehiclesLibraryTests
{
    [TestClass]
    public class MotoVehiclesParserTests
    {
        [TestMethod]
        public void ReadXML_Test()
        {
            var fileXMLPath = @"../../forTests/XMLVehiclesT1.xml";
            var fileXMLSchemaPath = @"../../forTests/XMLVehiclesSchemaT1.xsd";
            MotoVehiclesParser actual = new MotoVehiclesParser(fileXMLPath, fileXMLSchemaPath);
            var expected = new List<MotorVehicle>()
            {
                new MotorVehicle("3424324543452", "LF200", 13.7, 197, 5400, "Lifan"),
                new MotorVehicle("3424433453453", "WRX300", 31.0, 300, 6899, "Motoland"),
                new MotorVehicle("3424324234221", "LF150", 12.6, 158, 4499, "Lifan"),
                new MotorVehicle("3424433343332", "XR250", 22.4, 250, 7689, "Motoland"),
                new MotorVehicle("3424536754873", "Voge300RR", 29.1, 292, 13975, "Loncin")
            };

            for (var i = 0; i < actual.Count; i++)
                Assert.AreEqual(expected[i].ToString(), actual[i].ToString());
        }

        [TestMethod]
        public void ReadXML_ValidationCheck_Test()
        {
            var fileXMLPath = @"../../forTests/XMLVehiclesT2.xml";
            var fileXMLSchemaPath = @"../../forTests/XMLVehiclesSchemaT1.xsd";
            Exception actual = null;
            try
            {
                MotoVehiclesParser parser = new MotoVehiclesParser(fileXMLPath, fileXMLSchemaPath);
            }
            catch (InvalidXMLFileException exception)
            {
                actual = exception;
            }
            Exception expected = new InvalidXMLFileException();
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod]
        public void WriteXML_Test()
        {
            var fileXMLPath = @"../../forTests/XMLVehiclesT1.xml";
            var newFileXMLPath = @"../../forTests/XMLVehiclesT3.xml";
            var fileXMLSchemaPath = @"../../forTests/XMLVehiclesSchemaT1.xsd";
            MotoVehiclesParser parser = new MotoVehiclesParser(fileXMLPath, fileXMLSchemaPath);
            parser.WriteXML(newFileXMLPath);
            var actual = new MotoVehiclesParser(newFileXMLPath, fileXMLSchemaPath);
            var expected = new List<MotorVehicle>()
            {
                new MotorVehicle("3424324543452", "LF200", 13.7, 197, 5400, "Lifan"),
                new MotorVehicle("3424433453453", "WRX300", 31.0, 300, 6899, "Motoland"),
                new MotorVehicle("3424324234221", "LF150", 12.6, 158, 4499, "Lifan"),
                new MotorVehicle("3424433343332", "XR250", 22.4, 250, 7689, "Motoland"),
                new MotorVehicle("3424536754873", "Voge300RR", 29.1, 292, 13975, "Loncin")
            };

            for (var i = 0; i < actual.Count; i++)
                Assert.AreEqual(expected[i].ToString(), actual[i].ToString());
        }
    }
}
