using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLibrary
{
    public class InvalidXMLFileException : Exception
    {
        public InvalidXMLFileException() 
            : base("XML file does not match the schema")
        { }
            
    }
}
