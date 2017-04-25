using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XmlParseForPolistar.ViewModels;
using System.Threading;
using System.ComponentModel;
using System.Xml;

namespace XmlParseForPolistar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /*
        //Use BackgroundWorker and enable reporting progress
        BackgroundWorker worker = new BackgroundWorker();
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
           
            // get a reference to the worker that start this request
            BackgroundWorker workerSender = sender as BackgroundWorker;

            //get a node list from argument passed to RunWorkAsync
            Call callParameter = e.Argument as Call;
            //Load xml parameters into a parameterList
            foreach (XmlNode node in XmlParseForPolistar.Models.XMLParsers.LoadByXMLPath. ParameterNodeList)
            {
                Parameter drop = new Parameter();
                drop.name = node.Attributes.GetNamedItem("name").Value;
                drop.content = node.InnerText;

                callParameter.ParameterList.Add(drop);

                workerSender.ReportProgress(callParameter.ParameterList.Count / ParameterNodeList.Count)
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbStatus.Value = 100;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = Math.Min(e.ProgressPercentage,100);
        }
        */
        
    }
}
