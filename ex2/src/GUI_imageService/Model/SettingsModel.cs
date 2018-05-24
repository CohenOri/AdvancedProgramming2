using Comunication.Client;
using Comunication.Event;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_imageService.Modal
{
    class SettingsModel //: ISettingsModel
    {

        public int thumbNail { get; private set; }
        public string logName { get; private set; }
        public string sourceName { get; private set; }
        public string outPutDir { get; private set; }
        public List<string> Handlers { get; private set; }
        public event EventHandler<ServerDataReciecedEventArgs> ReadSettingsFromServer;
        public SettingsModel()
        {
            this.Handlers = new List<string>();
            GuiClient.Instance.ServerMassages += ReadFromServer;
        }

        public void GetSettings()
        {
            GuiClient.Instance.SendMessage("" + CommandEnum.GetConfigCommand);
        }

        public void ReadFromServer(object sender, ServerDataReciecedEventArgs e)
        {
            ReadSettingsFromServer(this, e);
       
        }

        public void RemoveHandler(object obj, PropertyChangedEventArgs e)
        {
            GuiClient.Instance.SendMessage("" + CommandEnum.CloseCommand + ":" + (string)e.PropertyName);
        }

    }
}
