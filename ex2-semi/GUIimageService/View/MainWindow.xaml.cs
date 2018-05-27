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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //SolidColorBrush my_brush = new SolidColorBrush(Color.FromArgb(255, 255, 215, 0));
            //WOW.Background = my_brush;

            /*System.Drawing.Color customColor = System.Drawing.Color.FromArgb(50, Color.Gray);
            WOW.Background = new SolidBrush(customColor);
            InitializeComponent();*/

            //var newTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            //newTextBlock.Name = "txt";
            //newTextBlock.Text = "YAAY";
            //DataTemplate newDataTemplate = new DataTemplate() { VisualTree = newTextBlock };
            //ContentPresenter contentPresenter = FindVisualChild<ContentPresenter>();
            //DataTemplate template = tabControl.ContentTemplate;

            //Console.WriteLine(template.VisualTree.ToString());
            this.DataContext = new MainWindowVM();


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









        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
