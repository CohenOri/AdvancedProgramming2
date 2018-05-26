﻿using System;
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
            /*GuiClient c = GuiClient.Instance;
            try
            {
                c.Connect();
            }

            catch (Exception e)
            {
            }*/

            if (GuiClient.Instance.ConnectedToServer)
            {
                this.backgroundColor = new SolidColorBrush(Colors.White);
            }
            else
            {
                this.backgroundColor = new SolidColorBrush(Colors.Gray);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GuiClient.Instance.SendMessage("Close Client");
        }

    }
}
