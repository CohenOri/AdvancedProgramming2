using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Image_Service_GUI.ViewModel;

using ImageService.Model;
using System.Collections.ObjectModel;

namespace Image_Service_GUI.ViewModel
{
    class LogVM
    {

        //private List<LogRecord> logList;
        private string msgType;
        private string msg;
        private ObservableCollection<LogRecord> logList;
        //private MessageTypeToBackgroundConverter converter = new MessageTypeToBackgroundConverter();

        public string Msg
        {
            // get { return this.logList[0].Message; }
            get { return "MEssage"; }
        }
        public string MsgType
        {
            //get { return this.logList[0].Type.ToString(); }
            get { return "TyPO"; }
        }

        public ObservableCollection<LogRecord> LogList
        {
            get { return this.logList; } // return this.logModel.logs;
        }


        public LogVM()
        {
            this.logList = new ObservableCollection<LogRecord>();
            this.logList.Add(new LogRecord(MessageTypeEnum.FAIL, "test"));
            this.logList.Add(new LogRecord(MessageTypeEnum.INFO, "s"));

        }


    }
}
           
