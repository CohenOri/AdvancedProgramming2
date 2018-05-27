using Logging.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class LogList
    {
        private List<string> log;
        public List<string> LogRecords {
            get { return log; }
            set { if (log != null)
                    log = value;
            }
        }
        private static LogList instance;

        public static LogList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogList();
                }
                return instance;
            }
        }
        /// <summary>
        /// create a list that will conatin each log recived
        /// </summary>
        public void CreateLogList()
        {
            if(this.log == null)
                this.log = new List<string>();
            if(this.LogRecords == null)
            {
                this.LogRecords = new List<string>();
            }
        }
        /// <summary>
        /// this mathoud will be called when new log recived to service. 
        ///add the log to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddNewLog(object sender, MessageRecievedEventArgs e)
        {
           // this.log.Add((int)e.Status + ":" + e.Message);
            this.LogRecords.Add((int)e.Status + ":" + e.Message);
        }

        /// <summary>
        /// create a string from the list to send it via tcp server.
        /// </summary>
        /// <returns></returns>
        public string GetLogList()
        {

            //string loggs = JsonConvert.SerializeObject(log, Formatting.None);
           // List<string> b = JsonConvert.DeserializeObject<List<string>>(a);
            string loggs = "";
            foreach (string logI in this.LogRecords)
            {
                if (loggs.Equals(""))
                    loggs = logI;
                else 
                    loggs = loggs + ";" + logI;
            }
            return loggs;
        }
    }
}
