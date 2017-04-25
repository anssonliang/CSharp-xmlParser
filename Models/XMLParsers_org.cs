using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Data;
using Microsoft.Win32;
using System.ComponentModel;

namespace XmlParseForPolistar.Models
{
    public static class XMLParsers
    {
        //private static DataTable tableParameter;
        private static XmlNodeList ParameterNodeList;
        public static XmlNodeList LoadByXMLPath()
        {
            //Read xml file
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = "xml (*.xml)|*.xml";
            dialog.ShowDialog();
            XmlDocument doc = new XmlDocument();
            doc.Load(dialog.FileName);
            
            //doc.Load(@"d:\VEJLE_CITY_041410.xml");

            //XmlNodeList with Xpath and nameSpace
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("nsTrace", "http://www.polystar.com/OSIX/monitoring/traceSchemaV1.0");
            ParameterNodeList = doc.SelectNodes("//nsTrace:userTableParameters//nsTrace:parameter", nsmgr);

            /*
            //Use BackgroundWorker and enable reporting progress
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            //Add event handlers
            worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);

            //Start worker thread
            worker.RunWorkerAsync(callParameter.ParameterList);
            */
            
            return ParameterNodeList;
        }


        public static DataTable ParseByXMLDocument()
        {
            Call callParameter = new Call();
            //Load xml parameters into a parameterList
            foreach (XmlNode node in ParameterNodeList)
            {
                Parameter drop = new Parameter();
                drop.name = node.Attributes.GetNamedItem("name").Value;
                drop.content = node.InnerText;

                callParameter.ParameterList.Add(drop);

                //worker.ReportProgress(callParameter.ParameterList.Count / ParameterNodeList.Count);
            }

            //Transpose parameterList to a datatable
            var transposedList = callParameter.ParameterList
                            .GroupBy(d => d.name)
                            .Select(g => new { Name = g.Key, Data = g.Select(d => d.content).ToList() })
                            .ToList();

            var tableParameter = new DataTable();
            foreach (var name in transposedList)
            {
                tableParameter.Columns.Add(name.Name);
            }

            var maxCount = transposedList.Max(t => t.Data.Count);
            for (var i = 0; i < maxCount; i++)
            {
                var row = tableParameter.NewRow();
                foreach (var name in transposedList)
                {
                    if (name.Data.Count > i)
                    {
                        row[name.Name] = name.Data[i];
                    }
                }

                tableParameter.Rows.Add(row);
            }
            return tableParameter;
        }

        // Parse the XML
        public static DataTable ParseByLinq()
        {
            var filter = from row in ParseByXMLDocument().AsEnumerable()
                         where row.Field<string>("S1AP Cause") == "3; release-due-to-eutran-generated-reason"
                         group row by new
                         {
                             S1AP_IMSI = row.Field<string>("S1AP IMSI"),
                             S1AP_TMSI = row.Field<string>("S1AP M-TMSI"),
                             S1AP_Cause = row.Field<string>("S1AP Cause")
                         } into grp
                         select new
                         {
                             IMSI = grp.Key.S1AP_IMSI,
                             TMSI = grp.Key.S1AP_TMSI,
                             Cause = grp.Key.S1AP_Cause,
                             Count = (int)grp.Count()
                         };

            var filter_1 = from row_1 in filter.AsEnumerable()
                           orderby row_1.Count descending
                           select row_1;

            DataTable filteredParameter = new DataTable();
            filteredParameter.Columns.Add("Count", typeof(int));
            filteredParameter.Columns.Add("TMSI", typeof(string));
            filteredParameter.Columns.Add("IMSI", typeof(string));
            filteredParameter.Columns.Add("Cause", typeof(string));

            foreach (var row_1 in filter_1)
            {
                DataRow dr = filteredParameter.NewRow();
                dr["TMSI"] = row_1.TMSI;
                dr["IMSI"] = row_1.IMSI;
                dr["Count"] = row_1.Count;
                dr["Cause"] = row_1.Cause;
                filteredParameter.Rows.Add(dr);
            }
            return filteredParameter;
        }

    }
}
