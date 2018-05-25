using Comunication.Client;
using Comunication.Event;
using Infrastructure.Enums;
using Logging.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_imageService.Modal
{
    class LogModal
    {
        public event EventHandler<LogRecord> NewLogNotify;
  //     public List<LogRecord> _logs { get; set; }
        /// <summary>
        /// C'tor for logModal
        /// </summary>
        public LogModal()
        {
            // this._logs = new List<LogRecord>();
            GuiClient c = GuiClient.Instance;
            c.Connect();
            c.ServerMassages += ReadFromServer;

        }
        public void GetLogList()
        {
            if (GuiClient.Instance.ConnectedToServer)
            {
                GuiClient.Instance.SendMessage("" + CommandEnum.LogCommand);
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
            if(e.DataType.Equals("Log"))
            {
                string[] logRecord = e.Date.Split(':');
                NewLogNotify(this, new LogRecord((MessageTypeEnum)int.Parse(logRecord[0]), logRecord[1]));
                //this._logs.Add(new LogRecord((MessageTypeEnum)int.Parse(logRecord[0]),logRecord[1]));
            }
            //if log list recived. recived in string, each log sperated by ";"
            else if (e.DataType.Equals("LogList"))
            {
                //make an array from the logs and add them.
                string[] logList = e.Date.Split(';');
                foreach(string log in logList)
                {
                    string[] logRecord = log.Split(':');
                    NewLogNotify(this, new LogRecord((MessageTypeEnum)int.Parse(logRecord[0]), logRecord[1]));

                    //this._logs.Add(new LogRecord((MessageTypeEnum)int.Parse(logRecord[0]), logRecord[1]));
                }
            }
        }
    }
}
