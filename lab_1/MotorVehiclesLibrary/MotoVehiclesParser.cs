using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using ExceptionLibrary;

namespace MotorVehiclesLibrary
{
    public class MotoVehiclesParser
    {
        private string fileXMLPath;
        private string fileXMLSchemaPath;

        public MotoVehiclesParser(string fileXMLPath, string fileXMLSchemaPath)
        {
            this.fileXMLPath = fileXMLPath;
            this.fileXMLSchemaPath = fileXMLSchemaPath;
        }

        public List<MotorVehicle> ReadXML()
        {
            List<MotorVehicle> vehicles = new List<MotorVehicle>();
            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.Schemas.Add(null, fileXMLSchemaPath);
            xmlSettings.ValidationType = ValidationType.Schema;
            xmlSettings.ValidationEventHandler += new ValidationEventHandler(xmlSettingsValidationEventHandler);
            XmlReader xr = XmlReader.Create(fileXMLPath, xmlSettings);
            string element = string.Empty;
            string[] propertys = new string[6];
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    element = xr.Name;
                }
                else
                {
                    if (xr.NodeType == XmlNodeType.Text)
                    {
                        switch (element)
                        {
                            case "article":
                                propertys[0] = xr.Value;
                                break;
                            case "name":
                                propertys[1] = xr.Value;
                                break;
                            case "power":
                                propertys[2] = xr.Value;
                                break;
                            case "capacity":
                                propertys[3] = xr.Value;
                                break;
                            case "price":
                                propertys[4] = xr.Value;
                                break;
                            case "manufacturer":
                                propertys[5] = xr.Value;
                                break;
                        }
                    }
                    else
                    {
                        if (xr.NodeType == XmlNodeType.EndElement && (xr.Name == "MotorVehicle"))
                        {
                            vehicles.Add(CreateVehicle(propertys));
                        }
                    }
                }
            }
            return vehicles;
        }

        public void WriteXML()
        {

        }

        public MotorVehicle CreateVehicle(string[] propertys)
        {
            string article = propertys[0];
            string name = propertys[1];
            double power = Convert.ToDouble(propertys[2]);
            int capacity = Convert.ToInt32(propertys[3]);
            int price = Convert.ToInt32(propertys[4]);
            string manufacturer = propertys[5];

            var vehicle = new MotorVehicle(article, name, power, capacity, price, manufacturer);
            propertys = null;
            return vehicle;
        }
        static void xmlSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning && e.Severity == XmlSeverityType.Error)
            {
                throw new InvalidXMLFileException();
            }
        }
    }
}

