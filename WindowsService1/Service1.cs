using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private Thread _thread;

        private void WorkerThreadFunc()
        {
            while (!_shutdownEvent.WaitOne(0))
            {
                //TODO main service code goes here
            }
        }

        public Service1()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            _thread = new Thread(WorkerThreadFunc);
            _thread.Name = "Working Thread";
            _thread.IsBackground = true;
            _thread.Start();
            System.IO.File.Create("/Users/Omri/Desktop/ServiceStarted.txt");
        }

        protected override void OnStop()
        {
            _shutdownEvent.Set();
            if (_thread.Join(3000)){
                _thread.Abort();
            }
            System.IO.File.Create("/Users/Omri/Desktop/ServiceStopped.txt");
        }


            
    }
}
