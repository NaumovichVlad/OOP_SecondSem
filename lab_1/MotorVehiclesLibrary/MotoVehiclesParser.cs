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
    enum Propertys
    {
        article,
        name,
        power,
        capacity,
        price,
        manufacturer
    }
    public class MotoVehiclesParser
    {
        private string fileXMLPath;
        private string fileXMLSchemaPath;
        private List<MotorVehicle> vehicles = new List<MotorVehicle>();

        public int Count { get { return vehicles.Count; } }

        public MotoVehiclesParser(string fileXMLPath, string fileXMLSchemaPath)
        {
            this.fileXMLPath = fileXMLPath;
            this.fileXMLSchemaPath = fileXMLSchemaPath;
            ReadXML();
        }

        public MotorVehicle this[int index]
        {
            get
            {
                return vehicles[index];
            }
            set
            {
                vehicles[index] = value;
            }
        }

        private void ReadXML()
        {
            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.Schemas.Add(null, fileXMLSchemaPath);
            xmlSettings.ValidationType = ValidationType.Schema;
            xmlSettings.ValidationEventHandler += new ValidationEventHandler(XmlSettingsValidationEventHandler);
            XmlReader xr = XmlReader.Create(fileXMLPath, xmlSettings);
            string element = string.Empty;
            var attributes = Enum.GetNames(typeof(Propertys));
            string[] propertys = new string[attributes.Length];
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
                        for (var i = 0; i < attributes.Length; i++)
                            if (attributes[i] == element)
                            {
                                propertys[i] = xr.Value;
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
        }

        public void WriteXML(string path)
        {
            var attributes = Enum.GetNames(typeof(Propertys));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter newXmlFile = XmlWriter.Create(path, settings);
            newXmlFile.WriteStartDocument();
            newXmlFile.WriteStartElement("vehicles");
            foreach(MotorVehicle vehicle in vehicles)
            {
                string[] propertys = vehicle.ToString().Split(' ');
                newXmlFile.WriteStartElement("MotorVehicle");
                var i = 0;
                foreach (string property in propertys)
                {
                    newXmlFile.WriteStartElement(attributes[i]);
                    newXmlFile.WriteString(property);
                    newXmlFile.WriteEndElement();
                    i++;
                }
                newXmlFile.WriteEndElement();
            }
            newXmlFile.WriteEndElement();
            newXmlFile.WriteEndDocument();
            newXmlFile.Dispose();
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
        private void XmlSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning || e.Severity == XmlSeverityType.Error)
                throw new InvalidXMLFileException();
        }
    }
}

