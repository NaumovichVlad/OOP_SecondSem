using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace MotorVehiclesLibrary
{
    public class MotoVehiclesParser
    {
        private List<MotorVehicle> vehicles;
        private string fileXMLPath;
        private string fileXMLSchemaPath;

        public MotoVehiclesParser(string fileXMLPath, string fileXMLSchemaPath)
        {
            this.fileXMLPath = fileXMLPath;
            this.fileXMLSchemaPath = fileXMLSchemaPath;
        }
        public MotorVehicle this[int index]
        {
            get { return vehicles[index]; }
            set { vehicles[index] = value; }
        }

        public void ReadXML()
        {

        }
    }
}
