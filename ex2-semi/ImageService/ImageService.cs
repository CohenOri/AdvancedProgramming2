using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


using ImageService.Server;
using ImageService.Controller;
using ImageService.Modal;
using Logging;
using Logging.Modal;
using System.Configuration;
using Infrastructure;
using ImageService.Commands;

namespace ImageService
{
    public partial class ImageService : ServiceBase

    {
        private ImageServer m_imageServer;          // The Image Server
        private IImageServiceModal modal;
        private IImageController controller;
        private ILoggingService logging;


        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        /// <summary>
        /// c'tor instelize all members from arguments of app config
        /// </summary>
        /// <param name="args"></param>
        public ImageService(string[] args)
        {
            InitializeComponent();
            //System.Diagnostics.Debugger.Launch();
            //get info fro, app config to set the varibles.
            LogList logList = LogList.Instance;
            logList.LogRecords = new List<string>();

            AppCongigSettings appConfig = AppCongigSettings.Instance;
            appConfig.outPutDir = ConfigurationManager.AppSettings["OutputDir"];
            appConfig.thumbNail = Int32.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
            appConfig.sourceName = ConfigurationManager.AppSettings["SourceName"];
            appConfig.logName = ConfigurationManager.AppSettings["Name"];

           /* string outPutDir = ConfigurationManager.AppSettings["OutputDir"];
            int thumpNailSize = Int32.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
            string eventSourceName = ConfigurationManager.AppSettings["SourceName"];
            string logName = ConfigurationManager.AppSettings["Name"];**/


            //create logger server, modal. controller and server.
            this.modal = new ImageServiceModal(appConfig.outPutDir, appConfig.thumbNail);
            this.logging = new LoggingService();
            this.controller = new ImageController(this.modal, this.logging);
            this.m_imageServer = new ImageServer(this.controller, this.logging);
            if (args.Count() > 0)
            {
                appConfig.sourceName = args[0];
            }
            if (args.Count() > 1)
            {
                appConfig.logName = args[1];
            }
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(appConfig.sourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(appConfig.sourceName, appConfig.logName);
            }
            //set log and sourse names.
           // eventLog1.Source = appConfig.sourceName;
           // eventLog1.Log = appConfig.logName;
            eventLog1.Source = appConfig.sourceName;
            eventLog1.Log = appConfig.logName;
            // listen to the server updates.
            logging.MessageRecieved += OnUpDateService;
            //logging.MessageRecieved += logList.AddNewLog;
        }


        /// <summary>
        /// start server's action.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            eventLog1.WriteEntry("Start Image service");
            // LogList.Instance.AddNewLog(this, new MessageRecievedEventArgs("Start Image service", MessageTypeEnum.INFO));
            LogList.Instance.LogRecords.Add("0:Start Image service");
            //logging.Log("Start Image service", MessageTypeEnum.INFO);

           // OnUpDateService(this, new MessageRecievedEventArgs("Start Image service", MessageTypeEnum.INFO));

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }
        /// <summary>
        /// stop the server, update the event and send message to server to close.
        /// </summary>
        protected override void OnStop()
        {

            eventLog1.WriteEntry("Stop Service: close handlers");

            //update service to state stop.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            //send massage to server that service stopped.
            this.m_imageServer.CloseAllHndlers(new CommandRecievedEventArgs(
                            (int) Infrastructure.Enums.CommandEnum.CloseCommand, null, null));
            //inform log event of end of operations
            eventLog1.WriteEntry("End of service-good bye");

            //stop lisening to loggers updates.
            logging.MessageRecieved -= OnUpDateService;

        }

        /// <summary>
        /// update the service logger event.
        /// </summary>
        /// <param name="sender></param>
        /// <param name="e" val=MessageRecievedEventArgs></param>
        public void OnUpDateService(object sender, MessageRecievedEventArgs e)
        {
            string[] status = { "INFO", "WARNING", "FAIL" };
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            //write event massage.
            eventLog1.WriteEntry(status[(int) e.Status]+ ": " + e.Message);
        }

        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            OnStop();
        }
    }
}
