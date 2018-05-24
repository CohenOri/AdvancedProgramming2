using Logging.Modal;
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
        /// <summary>
        /// create a list that will conatin each log recived
        /// </summary>
        public LogList()
        {
            this.log = new List<string>();
        }
        /// <summary>
        /// this mathoud will be called when new log recived to service. 
        ///add the log to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddNewLog(object sender, MessageRecievedEventArgs e)
        {
            this.log.Add(e.Status + ":" + e.Message);
        }

        /// <summary>
        /// create a string from the list to send it via tcp server.
        /// </summary>
        /// <returns></returns>
        public string GetLogList()
        {
            string loggs = "";
            foreach (string log in log)
            {
                loggs = loggs + ";" + log;
            }
            return loggs;
        }
    }
}
