using Comunication.Event;
using GUI_imageService;
using GUI_imageService.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
        public event EventHandler<ServerDataReciecedEventArgs> NotifyDirRemove;
        public event EventHandler<PropertyChangedEventArgs> AddToList;


        public SettingsVM()
        {
            this.settings = new SettingsModel();
            //this.NotifyDirRemove += this.RemoveHandlers;
            this.HandlerDirsList = new ObservableCollection<HandlerDirectories>();
            BindingOperations.EnableCollectionSynchronization(HandlerDirsList, HandlerDirsList);
            this.settings.ReadSettingsFromServer += SetSettingsData;//listen to server to get settings or handled to close
            NotifyDirRemove+= this.settings.RemoveHandler;
            AddToList += AddHandlerToList;
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
                string[] handlerList = handlers.Split(';');
                this.thumbnailSize = "" + (int)settingsObj["thumbNail"];
                this.logName = (string)settingsObj["logName"];
                this.srcName = (string)settingsObj["sourceName"];
                this.outputDic = (string)settingsObj["outPutDir"];
                foreach (string handler in handlerList)
                {
                    this.AddToList(this, new PropertyChangedEventArgs(handler));
                }
            }
            else if (e.DataType.Equals("Log") && e.Date.StartsWith("0:close handler:"))
            {
                string starts = e.Date.Substring(16);
                //  this.handlerDirsList.Remove(e.Date);
                HandlerDirectories dir = null;
                foreach (HandlerDirectories dirr in this.HandlerDirsList)
                {
                    if(dirr.Path.Equals(starts))
                    {
                        dir = dirr;
                    }
                }
                if(dir != null)
                    this.HandlerDirsList.Remove(dir);
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
            NotifyDirRemove(this,new ServerDataReciecedEventArgs("Close handler", e.PropertyName));
        }

        private void AddHandlerToList(object obj, PropertyChangedEventArgs path)
        {
            HandlerDirectories dir = new HandlerDirectories() { Path = path.PropertyName };
            this.HandlerDirsList.Add(dir);
        }


        private void RemoveHandler(object obj, PropertyChangedEventArgs path)
        {
            HandlerDirectories dir = new HandlerDirectories() { Path = path.PropertyName };
            this.HandlerDirsList.Remove(dir);
            Console.WriteLine("remove handelrsss: path:prop:" + path.PropertyName);
        }






    }
}
