using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Comunication.Client;

using System.Windows.Media;

namespace Image_Service_GUI.ViewModel
{
    class MainWindowVM
    {
        /// <summary>
        /// If connected to ImageService return white (background) otherwise return grey
        /// </summary>
        private Brush backgroundColor;
        public Brush VM_BackgroundColor
        {
            get { return backgroundColor; }
        }
        public MainWindowVM()
        {
            /// if () { }         /// If connected to ImageService return white (background) otherwise return grey
            /// else {
            this.backgroundColor = new SolidColorBrush(Colors.White);
            GuiClient c = GuiClient.Instance;
            try
            {
                c.Connect();
            } catch (Exception e)
            {
                Console.WriteLine(e.Data);
            }
        }

    }
}
