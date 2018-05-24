using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Image_Service_GUI.ViewModel;

using ImageService.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Image_Service_GUI.ViewModel
{
    class LogVM : INotifyPropertyChanged
    {


        private ObservableCollection<LogRecord> logList;
        public ObservableCollection<LogRecord> LogList
        {
            get { return this.logList; } // return this.logModel.logs;
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public LogVM()
        {
            this.logList = new ObservableCollection<LogRecord>();

            // temp just to see it works
            this.logList.Add(new LogRecord(MessageTypeEnum.FAIL, "test"));
            this.logList.Add(new LogRecord(MessageTypeEnum.INFO, "s"));
            this.logList.Add(new LogRecord(MessageTypeEnum.WARNING, "WOW this is so dangerous"));
        }


    }
}
           
