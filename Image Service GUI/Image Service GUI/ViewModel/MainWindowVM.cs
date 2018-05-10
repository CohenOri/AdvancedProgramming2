using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;

namespace Image_Service_GUI.ViewModel
{
    class MainWindowVM
    {
        /// <summary>
        /// If connected to ImageService return white (background) otherwise return grey
        /// </summary>
        public Brush VM_BackgroundColor
        {
            get
            {
                return new SolidColorBrush(Colors.Yellow);

            }
        }



    }
}
