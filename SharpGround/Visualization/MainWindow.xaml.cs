using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using System.Threading;
using AdjacencyMatrix;

namespace Visualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //start button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            start_button.Visibility = Visibility.Hidden;
            start_button.IsEnabled = false;
            pause_button.Visibility = Visibility.Visible;
            (DataContext as ViewModel).Run();
            (DataContext as ViewModel).Resume();
            
        }

        //pause resume button click
        const String ptag = "Pause";
        const String rtag = "Resume";
        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {
            switch (pause_button.Content.ToString())
            {
                case ptag:
                    (DataContext as ViewModel).Pause();
                    pause_button.Content = rtag;
                    break;
                case rtag:
                    (DataContext as ViewModel).Resume();
                    pause_button.Content = ptag;
                    break;
                default:
                    pause_button.Content = ptag;
                    break;
            }
        }
    }
}
