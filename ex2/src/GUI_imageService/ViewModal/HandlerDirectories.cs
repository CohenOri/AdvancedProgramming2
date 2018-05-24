using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Service_GUI.ViewModel
{
    public class HandlerDirectories : INotifyPropertyChanged
    {
        private string path;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Path
        {
            get { return this.path; }
            set
            {
                if (this.path != value)
                {
                    this.path = value;
                    this.NotifyPropertyChanged("Path");
                }
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
