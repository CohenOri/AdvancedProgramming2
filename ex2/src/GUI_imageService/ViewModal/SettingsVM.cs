using Comunication.Event;
using GUI_imageService.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Service_GUI.ViewModel
{
    class SettingsVM
    {
        private string outputDic;
        private string srcName;
        private string logName;
        private string thumbnailSize;
        private SettingsModel settings;
        private ObservableCollection<HandlerDirectories> handlerDirsList;
        public ObservableCollection<HandlerDirectories> HandlerDirsList { get; set; }
        public event EventHandler<PropertyChangedEventArgs> NotifyDirRemove;

        public SettingsVM()
        {
            this.settings = new SettingsModel();
            this.NotifyDirRemove += this.settings.RemoveHandler;
            this.settings.ReadSettingsFromServer += SetSettingsData;//listen to server to get settings or handled to close
            this.settings.GetSettings();
            this.outputDic = "def";
            this.srcName = "def";
            this.logName = "def";
           this.thumbnailSize = "def";

        }

        public void SetSettingsData(object obj, ServerDataReciecedEventArgs e )
        {
            if (e.DataType.Equals("Settings"))
            {
                JObject settingsObj = JObject.Parse(e.Date);
                string handlers = (string)settingsObj["Handler"];
                this.thumbnailSize = "" + (int)settingsObj["thumbNail"];
                this.logName = (string)settingsObj["logName"];
                this.srcName = (string)settingsObj["sourceName"];
                this.outputDic = (string)settingsObj["outPutDir"];
                string[] handlerList = handlers.Split(';');
                foreach (string handler in handlerList)
                {
                    this.handlerDirsList.Add(new HandlerDirectories() { Path = handler });
                }
            }
            else if (e.DataType.Equals("close handler"))
            {
                //   this.handlerDirsList.Remove(e.Date);
                this.handlerDirsList.Remove(new HandlerDirectories() { Path = e.Date });
            }
        }
        public string VM_OutputDirectory
        {
            get
            {
                return outputDic;

            }
            set
            {
                outputDic = value;
            }
        }
        public string VM_SrcName
        {
            get
            {
                return srcName;

            }
            set { srcName = value; }
        }
        public string VM_LogName
        {
            get
            {
                return logName;

            } set { logName = value; }
        }
        public string VM_ThumbnailSize
        {
            get
            {
                return thumbnailSize;

            } set { thumbnailSize = value; }
        }

        public void NotifyServerRemove(object obj, PropertyChangedEventArgs e)
        {
            NotifyDirRemove(this, e);
        }
        
        




    }
}
