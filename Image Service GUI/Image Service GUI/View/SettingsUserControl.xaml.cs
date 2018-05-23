using Image_Service_GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Image_Service_GUI.View
{
    /// <summary>
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        // holds all the current handled directoreis - directories under tracking
        private ObservableCollection<HandlerDirectories> handlerDirs = new ObservableCollection<HandlerDirectories>();


        /*public string VM_OutputDirectory
        {
            get
            {
                return "YO! MA!";

            }
        }*/


        public SettingsUserControl()
        {
            InitializeComponent();

            this.DataContext = new SettingsVM();

            InitSettingWindows();
            handlerDirs.Add(new HandlerDirectories() { Path = "John Doe" });
            handlerDirs.Add(new HandlerDirectories() { Path = "Jane Doe" });
            handlerList.ItemsSource = handlerDirs;

        }

        private void btnRemoveDirClick(object sender, RoutedEventArgs e)
        {

            if (handlerList.SelectedItem != null)
            {
                handlerDirs.Remove(handlerList.SelectedItem as HandlerDirectories);
                this.RemoveDirClick.IsEnabled = false;
            }
        }

        private void handlerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Console.WriteLine(this.handlerDirs.Count()); prints how much more dirs there is
            this.RemoveDirClick.IsEnabled = true;
        }

        private void InitSettingWindows()
        {
            // READ FROM FILE
            //this.outDir.Text = "sss";
            //this.srcName.Text = "d";
            //this.logName.Text = "a";
            //this.thumbnailSize.Text = "B";
        }


    }
}
