using Comunication.Event;
using Infrastructure.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Settings
    {
        public List<string> Handlers { get; private set; }
        private string outputDir;
        private string srcName;
        private string logName;
        private string thumbnailSize;



        private Settings()
        {
            this.Handlers = new List<string>();
            this.outputDir = "def";
            this.srcName = "def";
            this.logName = "def";
            this.thumbnailSize = "def";
            Client client = Client.Instance;
            client.ServerMassages += SetSettingsData;
            //connect and get data from server
            GetData();
        }

        private static Settings instance = null;
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings();
                }
                return instance;
            }
        }
        /// <summary>
        /// connects to server if not connected and send data request
        /// </summary>
        public void GetData()
        {
            Client client = Client.Instance;
            if (!client.IsConnected())
            {
                client.Connect();
                //means no connection
                if (!client.IsConnected()) return;
            }
            client.SendMessage("" + (int)CommandEnum.GetConfigCommand);
        }
        /// <summary>
        /// reads setting data from server, and sets them to the prop's
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void SetSettingsData(object obj, ServerDataReciecedEventArgs e)
        {
            if (e.DataType.Equals("Settings")) // if reads "settings" as pre-command from server, we now need to read settings
            {
                JObject settingsObj = JObject.Parse(e.Date);
                string handlers = (string)settingsObj["Handler"];
                string[] handlerList = handlers.Split(';');
                foreach (string handler in handlerList)
                {
                    this.Handlers.Add(handler);
                }
                this.thumbnailSize = "" + (int)settingsObj["thumbNail"];
                this.logName = (string)settingsObj["logName"];
                this.srcName = (string)settingsObj["sourceName"];
                this.outputDir = (string)settingsObj["OutPutDir"];
            }
            // if not Log or Close cmd & not settings.. - Handlers cmd
            else if (e.DataType.Equals("Log") && e.Date.StartsWith("0:close handler:"))
            {
                string starts = e.Date.Substring(16); // reads handlres...
                String dir = null;
                foreach (string dirr in this.Handlers)
                {
                    if (dirr.Equals(starts))
                    {
                        dir = dirr;
                        break;
                    }
                }
                if (dir != null)
                {
                    this.Handlers.Remove(dir);
                }
            }
        }

        [Required]
        [Display(Name = "Sorce Name")]
        public string SrcName { get { return this.srcName; } }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Log Name")]
        public string LogName { get { return this.logName; } }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Thumbnail Size")]
        public string ThumbNail { get { return this.thumbnailSize; } }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutPut Directory")]
        public string OutPutDur { get { return this.outputDir; } }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handlers")]
        public List<string> ListHandlers { get { return this.Handlers; } }

        public string RelativePath { get { return ConvertToRelative(this.outputDir); } }

        // make the path relative and not absolute
        public string ConvertToRelative(string path)
        {
            string[] relativePath = path.Split('\\');
            //return "~\\" + relativePath[relativePath.Length - 2];
            return @"~\" + relativePath[relativePath.Length - 2] + '\\' ;
        }


    }
}