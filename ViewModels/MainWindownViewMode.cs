using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlParseForPolistar.MVVMUtilities;
using XmlParseForPolistar.Models;
using System.Data;
using System.Xml;

namespace XmlParseForPolistar.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private XmlNodeList List;
        public XmlNodeList ListObject
        {
            get { return List; }
            private set 
            {
                List = value;
                NotifyPropertyChanged("ListObject");
            }
        }
        private DataTable table;
        public DataTable TableObject
        {
            get { return table; }
            private set
            {
                table = value;
                NotifyPropertyChanged("TableObject");
            }
        }

        private void InitiateState()
        {
            table = new DataTable();
        }

        // Commands
        /*
        public RelayCommand ClearResultCommand { get; private set; }
        private void ClearResult()
        {
            TableObject = new DataTable();
        }
        */
        public RelayCommand XMLPathLoadCommand { get; private set; }
        private void XMLPathLoad()
        {
            ListObject = XMLParsers.LoadByXMLPath();
        }

        public RelayCommand XMLDocumentLoadCommand { get; private set; }
        private void XMLDocumentLoad()
        {
            TableObject = XMLParsers.ParseByXMLDocument();
        }
        public RelayCommand LinqLoadCommand { get; private set; }
        private void LinqLoad()
        {
            TableObject = XMLParsers.ParseByLinq();
        }
        private void WireCommands()
        {
            /*
            ClearResultCommand = new RelayCommand(ClearResult);
            ClearResultCommand.IsEnabled = true;
            */

            XMLPathLoadCommand = new RelayCommand(XMLPathLoad);
            XMLPathLoadCommand.IsEnabled = true;

            XMLDocumentLoadCommand = new RelayCommand(XMLDocumentLoad);
            XMLDocumentLoadCommand.IsEnabled = true;

            LinqLoadCommand = new RelayCommand(LinqLoad);
            LinqLoadCommand.IsEnabled = true;
        }

        // Constructor
        public MainWindowViewModel()
        {
            InitiateState();
            WireCommands();
        }
    }
}
