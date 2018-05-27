using Comunication.Event;
using ImageService.Commands;
using Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class LogCommand : ICommand
    {
        private LogList log;
        public LogCommand(ILoggingService service)
        {
           
            this.log =  LogList.Instance;
            this.log.CreateLogList();
            //service.MessageRecieved += log.AddNewLog;
        }
        /// <summary>
        /// NewFile recevied into logged folder, we need to execute something (in this case AddFile method)
        /// </summary>
        /// <param name="args">args for the command</param>
        /// <param name="result">reference to result flag to update</param>
        /// <returns>an *error message* / image path (if you use it it will open the image)</returns>
        public string Execute(string[] args, out bool result)
        {
            result = true;
            ServerDataReciecedEventArgs a = new ServerDataReciecedEventArgs("LogList", log.GetLogList());
            string logs = a.ToJSON();
            return logs;
        }


    }
}
