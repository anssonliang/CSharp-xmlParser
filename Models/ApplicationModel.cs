using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.Linq;

namespace XmlParseForPolistar.Models
{
    public class Parameter
    {
        public string name { get; set; }
        public string content { get; set; }
    }

    public class Call
    {
        public List<Parameter> ParameterList { get; set; }

        public Call()
        {
            ParameterList = new List<Parameter>();
        }
    }

    /*
    public class Table
    {
        public DataTable TableList { get; set; }
        public Table()
        {
            TableList = new DataTable();
        }
    }
    */
}
