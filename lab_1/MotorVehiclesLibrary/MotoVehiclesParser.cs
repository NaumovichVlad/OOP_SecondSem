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
    /// <summary>
    /// MotorVehicle properties names
    /// </summary>
    public enum Properties
    {
        /// <summary>
        /// Article
        /// </summary>
        article,
        /// <summary>
        /// Type of motorcycle
        /// </summary>
        name,
        /// <summary>
        /// Engine power in double
        /// </summary>
        power,
        /// <summary>
        /// Engine capacity in int
        /// </summary>
        capacity,
        /// <summary>
        /// Motorcycle price
        /// </summary>
        price,
        /// <summary>
        /// The manufacturer of the motorcycle
        /// </summary>
        manufacturer
    }
    /// <summary>
    /// MotorVehicle properties
    /// </summary>
    public enum ClassProperties
    {
        /// <summary>
        /// Article
        /// </summary>
        ArticleNumber,
        /// <summary>
        /// Type of motorcycle
        /// </summary>
        Name,
        /// <summary>
        /// Engine power in double
        /// </summary>
        EnginePower,
        /// <summary>
        /// Engine capacity in int
        /// </summary>
        EngineCapacity,
        /// <summary>
        /// Motorcycle price
        /// </summary>
        Price,
        /// <summary>
        /// The manufacturer of the motorcycle
        /// </summary>
        Manufacturer
    }
    /// <summary>
    /// Read and write xml
    /// </summary>
    public class MotoVehiclesParser
    {
        private string fileXMLPath;
        private string fileXMLSchemaPath;
        private List<MotorVehicle> vehicles = new List<MotorVehicle>();

        /// <summary>
        /// The amount of equipment read from the file
        /// </summary>
        public int Count { get { return vehicles.Count; } }

        /// <summary>
        /// Automatic reading from a file
        /// </summary>
        /// <param name="fileXMLPath">Path to the xml file</param>
        /// <param name="fileXMLSchemaPath">Path to the xsd file</param>
        public MotoVehiclesParser(string fileXMLPath, string fileXMLSchemaPath)
        {
            this.fileXMLPath = fileXMLPath;
            this.fileXMLSchemaPath = fileXMLSchemaPath;
            ReadXML();
        }

        /// <summary>
        /// Access to the read information
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Information on the index</returns>
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
            var attributes = Enum.GetNames(typeof(Properties));
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
                            CreateVehicle(propertys);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Writing data to an xml file
        /// </summary>
        /// <param name="path">Path to the xml file</param>
        public void WriteXML(string path)
        {
            var attributes = Enum.GetNames(typeof(Properties));
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
        /// <summary>
        /// Creating a class with a technique
        /// </summary>
        /// <param name="properties">vehicle parameters</param>
        public void CreateVehicle(string[] properties)
        {
            string article = properties[0];
            string name = properties[1];
            double power = Convert.ToDouble(properties[2]);
            int capacity = Convert.ToInt32(properties[3]);
            int price = Convert.ToInt32(properties[4]);
            string manufacturer = properties[5];

            var vehicle = new MotorVehicle(article, name, power, capacity, price, manufacturer);
            properties = null;
            vehicles.Add(vehicle);
        }
        private void XmlSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning || e.Severity == XmlSeverityType.Error)
                throw new InvalidXMLFileException();
        }

        /// <summary>
        /// The removal of the equipment by index
        /// </summary>
        /// <param name="index">The index to remove</param>
        public void Remove(int index)
        {
            if (index >= 0 && index < vehicles.Count)
                vehicles.RemoveAt(index);
        }
    }
}

