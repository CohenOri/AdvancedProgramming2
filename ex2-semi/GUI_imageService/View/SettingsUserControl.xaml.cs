using Image_Service_GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class SettingsUserControl : UserControl, INotifyPropertyChanged
    {
        // holds all the current handled directoreis - directories under tracking
        //private ObservableCollection<HandlerDirectories> handlerDirs;
        //public ObservableCollection<HandlerDirectories> HandlerDirs
        //{
        //    get { return this.handlerDirs; }
        //}
    
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private SettingsVM svm;
        public SettingsUserControl()
        {
            InitializeComponent();
            this.svm = new SettingsVM();
            PropertyChanged += this.svm.NotifyServerRemove;
            this.DataContext = this.svm;
            //handlerDirs = ;
            handlerList.ItemsSource = this.svm.HandlerDirsList;
            //this.RemoveDirClick.IsEnabled = false;

            // temp add items manualy, later should be added using HandlerDirs.add(SOMETHING);
            //handlerDirs.Add(new HandlerDirectories() { Path = "John Doe" }); // temp
            //handlerDirs.Add(new HandlerDirectories() { Path = "Jane Doe" }); // temp
        }

        private void btnRemoveDirClick(object sender, RoutedEventArgs e)
        {

            if (handlerList.SelectedItem != null)
            {
               // handlerDirs.Remove(handlerList.SelectedItem as HandlerDirectories);
                this.NotifyPropertyChanged((handlerList.SelectedItem as HandlerDirectories).Path); // notify all the subscirbers about the removal
                //this.RemoveDirClick.IsEnabled = false;
            }
        }

        private void handlerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Console.WriteLine(this.handlerDirs.Count()); //prints how much more dirs there is
           this.RemoveDirClick.IsEnabled = !(this.RemoveDirClick.IsEnabled);
            //Console.WriteLine(this.handlerDirs[0].Path);
        }


    }
}
