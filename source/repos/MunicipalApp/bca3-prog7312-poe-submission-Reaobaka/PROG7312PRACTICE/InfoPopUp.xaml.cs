using System.Collections.Generic;
using System.Windows;

namespace PROG7312PRACTICE
{
    public partial class InfoPopup : Window
    {
        public InfoPopup(List<LocalEvent> pending, List<LocalEvent> newEvents, LocalEvent? nextUpcoming)
        {
            InitializeComponent();

            PendingList.ItemsSource = pending ?? new List<LocalEvent>();
            NewList.ItemsSource = newEvents ?? new List<LocalEvent>();

            if (nextUpcoming != null)
            {
                NextEventPanel.DataContext = nextUpcoming;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
