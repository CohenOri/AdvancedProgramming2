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

namespace Image_Service_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        // holds all the current handled directoreis - directories under tracking        private ObservableCollection<HandlerDirectories> handlerDirs = new ObservableCollection<HandlerDirectories>();
        public MainWindow()
        {
            InitializeComponent();

            InitSettingWindows();
            handlerDirs.Add(new HandlerDirectories() { Path = "John Doe" });
            handlerDirs.Add(new HandlerDirectories() { Path = "Jane Doe" });
            handlerList.ItemsSource = handlerDirs;

            var newTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            newTextBlock.Name = "txt";
            newTextBlock.Text = "YAAY";
            //DataTemplate newDataTemplate = new DataTemplate() { VisualTree = newTextBlock };
            //ContentPresenter contentPresenter = FindVisualChild<ContentPresenter>();
            DataTemplate template = tabControl.ContentTemplate;

            Console.WriteLine(template.VisualTree.ToString());

        }




        /* private void btnAddUser_Click(object sender, RoutedEventArgs e)
         {
             handlerDirs.Add(new HandlerDirectories() { Path = "New user" });
         }
         private void btnChangeUser_Click(object sender, RoutedEventArgs e)
         {
             if (handlerList.SelectedItem != null)
                 (handlerList.SelectedItem as HandlerDirectories).Path = "Random Name";
         }*/
        private void btnRemoveDirClick(object sender, RoutedEventArgs e)
        {

            if (handlerList.SelectedItem != null)
            {
                handlerDirs.Remove(handlerList.SelectedItem as HandlerDirectories);
                this.RemoveDirClick.IsEnabled = false;
            }
        }







        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void handlerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Console.WriteLine(this.handlerDirs.Count()); prints how much more dirs there is
            this.RemoveDirClick.IsEnabled = true;
        }

        private void InitSettingWindows()
        {
            // READ FROM FILE
            this.outDir.Text = "sss";
            this.srcName.Text = "d";
            this.logName.Text = "a";
            this.thumbnailSize.Text = "B";
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
