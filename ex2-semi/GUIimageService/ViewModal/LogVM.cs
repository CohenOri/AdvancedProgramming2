﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Image_Service_GUI.ViewModel;

using System.Collections.ObjectModel;
using System.ComponentModel;
using Comunication.Event;
using GUI_imageService.Modal;
using Logging.Modal;

namespace Image_Service_GUI.ViewModel
{
    class LogVM : INotifyPropertyChanged
    {

        private LogModal logModel;
        private ObservableCollection<LogRecord> logList;
        public ObservableCollection<LogRecord> LogList
        {
            get { return this.logList; } // return this.logModel.logs;
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public LogVM()
        {
            
            this.logList = new ObservableCollection<LogRecord>();
            this.logModel = new LogModal();
            this.logModel.NewLogNotify += AddToList; //listen to incoming new logs from server
            this.logModel.GetLogList();
            // temp just to see it works
            this.logList.Add(new LogRecord(MessageTypeEnum.FAIL, "test"));
            this.logList.Add(new LogRecord(MessageTypeEnum.INFO, "s"));
            this.logList.Add(new LogRecord(MessageTypeEnum.WARNING, "WOW this is so dangerous"));
        }

        public void AddToList(object obj, LogRecord log)
        {
            this.logList.Add(log);
        }

    }
}
           
