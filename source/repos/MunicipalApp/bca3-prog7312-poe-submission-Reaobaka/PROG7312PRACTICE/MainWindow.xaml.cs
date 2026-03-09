using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG7312PRACTICE
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        private void ReportIssue_Click(object sender, RoutedEventArgs e)
        {
            ReportIssue reportIssue = new ReportIssue();
            reportIssue.Show();
            this.Close();
        }


        private void Events_Click(object sender, RoutedEventArgs e)
        {

           Events events = new Events();
            events.Show();
            this.Close ();
        
        }

        private void Status_Click(object sender, RoutedEventArgs e)

        {
            ServiceRequestWindow serviceRequestWindow = new ServiceRequestWindow();
            serviceRequestWindow.Show();
            this.Close ();
        }

        }
    }
