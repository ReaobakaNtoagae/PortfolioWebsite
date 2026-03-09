using System.Windows;
using System.Windows.Controls;

namespace PROG7312PRACTICE
{
    public partial class LanguageSelectionDialog : Window
    {
        public string SelectedLanguage { get; private set; }

        public LanguageSelectionDialog()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (Languages.SelectedItem is ListBoxItem selectedItem && selectedItem.Tag is string cultureCode)
            {
                SelectedLanguage = cultureCode;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a language before proceeding.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (Languages.SelectedItem is ListBoxItem selectedItem && selectedItem.Tag is string cultureCode)
            {
                SelectedLanguage = cultureCode;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a language before proceeding.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}