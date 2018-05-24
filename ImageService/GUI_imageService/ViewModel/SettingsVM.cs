using System;
using System.Collections.Generic;
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
        
        public SettingsVM()
        {
            this.outputDic = "def";
            this.srcName = "def";
            this.logName = "def";
            this.thumbnailSize = "def";
        }




    }
}
