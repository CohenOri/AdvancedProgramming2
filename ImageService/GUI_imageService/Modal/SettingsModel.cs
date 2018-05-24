using Comunication.Event;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_imageService.Modal
{
    class SettingsModel : ISettingsModel
    {

        public int thumbNail { get; private set; }

        public string logName { get; private set; }
        public string sourceName { get; private set; }
        public string outPutDir { get; private set; }
        public List<string> Handlers { get; private set; }



        public void ReadFromServer(object sender, ServerDataReciecedEventArgs e)
        {

            if (e.DataType.Equals("Settings"))
            {
                JObject settingsObj = JObject.Parse(e.Date);
                string handlers = (string)settingsObj["Handler"];
                this.thumbNail = (int)settingsObj["thumbNail"];
                this.logName = (string)settingsObj["logName"];
                this.sourceName = (string)settingsObj["sourceName"];
                this.outPutDir = (string)settingsObj["outPutDir"];
                string[] handlerList = handlers.Split(';');
                foreach(string handler in handlerList)
                {
                    this.Handlers.Add(handler);
                }


            }
            else if (e.DataType.Equals("Handler"))
            {
                this.Handlers.Remove(e.Date);
            }
        }

    }
}
