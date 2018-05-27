using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class AppCongigSettings
    {
        private static AppCongigSettings instance;

        private AppCongigSettings() { }

        public static AppCongigSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppCongigSettings();
                }
                return instance;
            }
        }
    
    public string outPutDir { get; set; }
        public string sourceName { get; set; }
        public string logName { get; set; }
        public int thumbNail { get; set; }
        public string Handlers { get; set; }
        public int numberHandlers { get; set; }

        public string ToJSON()
        {
            JObject settingsObj = new JObject();
            settingsObj["outPutDir"] = outPutDir;
            settingsObj["sourceName"] = sourceName;
            settingsObj["logName"] = logName;
            settingsObj["thumbNail"] = thumbNail;
            settingsObj["Handler"] = Handlers;
            return settingsObj.ToString();
        }

        public static AppCongigSettings FromJSON(string str)
        {
            AppCongigSettings settings = new AppCongigSettings();
            JObject settingsObj = JObject.Parse(str);
            settings.Handlers = (string)settingsObj["Handler"];
            settings.thumbNail = (int)settingsObj["thumbNail"];
            settings.logName = (string)settingsObj["logName"];
            settings.sourceName = (string)settingsObj["sourceName"];
            settings.outPutDir = (string)settingsObj["outPutDir"];
            return settings;
        }

    }
}
