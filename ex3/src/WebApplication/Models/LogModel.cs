using Comunication.Event;
using Infrastructure.Enums;
using Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApplication.Models
{
    public class LogModel
    {
        private List<Log> logList;
        public List<Log> LogList { get { return this.logList; } }
        /// <summary>
        /// C'tor for logModal
        /// </summary>
        public LogModel()
        {
            Client c = Client.Instance;
            this.logList = new List<Log>();
            c.Connect();
            c.ServerMassages += ReadFromServer; // add methods to trigger when new message arive to GUI client
            GetLogList();
        }

        /// <summary>
        /// GetLogList cmd to send to server when asking for log list
        /// </summary>
        public void GetLogList()
        {
            if (!Client.Instance.ConnectedToServer)
                Client.Instance.Connect();

            if (Client.Instance.IsConnected())
                {
                    Client.Instance.SendMessage("" + (int)CommandEnum.LogCommand);
                }
        }

        /// <summary>
        /// The function being called when new data recived. it check what data type it is
        /// if its a log list or only one log, add to the list. any other case ignore.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReadFromServer(object sender, ServerDataReciecedEventArgs e)
        {
            //if only one log recived
            if (e.DataType.Equals("Log") && (!(e.Date.StartsWith("0:close handler:"))))
            {
                string[] logRecord = e.Date.Split(':');
                this.logList.Add(new Log
                {
                    Type = (MessageTypeEnum)int.Parse(logRecord[0]),
                    Data = e.Date

                });
            }
            //if log list(!) recived. recived in string, each log sperated by ";"
            else if (e.DataType.Equals("LogList"))
            {
                //make an array from the logs and add them.
                string[] logList = e.Date.Split(';');
                foreach (string log in logList)
                {
                    string[] logRecord = log.Split(':');
                    this.logList.Add(new Log
                    {
                        Type = (MessageTypeEnum)int.Parse(logRecord[0]),
                        Data = log

                    });
                }
            }
        }
    }
}
