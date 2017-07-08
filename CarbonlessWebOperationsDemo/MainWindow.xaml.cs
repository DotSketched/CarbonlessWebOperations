using CarbonlessWebOperations.Classes.HelperObjects;
using CarbonlessWebOperations.Main;
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
using CarbonlessWebOperations.Classes.Events;

namespace CarbonlessWebOperationsDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebOperations webOperations;

        public MainWindow()
        {
            InitializeComponent();
            MainMethod();
        }

        public void MainMethod()
        {
            webOperations = new WebOperations();
            webOperations.DownloadFileAsync(new WebOperationDownloadObject() { Url = new Uri(@"https://www.dropbox.com/s/ovlpue2r4693iy4/A%20Troll%20Never%20Dies....mp4?dl=1") });

            webOperations.WebOperationProgress += Downloading;
            webOperations.WebOperationCompleted += Completed;
        }

        private void Completed(object sender, AsyncWebOperationsCompleteArgs e)
        {
            
        }

        private void Downloading(object sender, AsyncWebOperationsProgressArgs e)
        {
            
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            webOperations.PauseDownload();
            State.Content = webOperations.WebOperationState;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            webOperations.StopDownload();
            State.Content = webOperations.WebOperationState;
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            webOperations.ResumeDownload();
            State.Content = webOperations.WebOperationState;
        }
    }
}
