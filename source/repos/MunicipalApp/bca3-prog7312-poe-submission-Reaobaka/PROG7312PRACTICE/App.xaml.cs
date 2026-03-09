using PROG7312PRACTICE;
using System;
using System.Diagnostics;
using System.Windows;

namespace PROG7312PRACTICE
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Ensure the app stays alive until all windows are closed
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;

            // Create and show MainWindow first
            var mainWindow = new MainWindow();
            this.MainWindow = mainWindow; // keep reference
            mainWindow.Show();


            var langDialog = new LanguageSelectionDialog
            {
                Owner = mainWindow 
            };

            bool? dialogResult = langDialog.ShowDialog(); 

            string selectedCulture = "en"; // default language
            if (dialogResult == true)
            {
                selectedCulture = langDialog.SelectedLanguage;
            }

            // Load the selected language into resources
            LoadLanguageResource(selectedCulture);
        }



        private void LoadLanguageResource(string culture)
    {
        Application.Current.Resources.MergedDictionaries.Clear();

        try
        {
            var dict = new ResourceDictionary
            {
                Source = new Uri(
                    $"pack://application:,,,/PROG7312PRACTICE;component/Resources/StringResources.{culture}.xaml",
                    UriKind.Absolute)
            };

            Application.Current.Resources.MergedDictionaries.Add(dict);
            Debug.WriteLine($"[INFO] Successfully loaded language resource: {culture}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[ERROR] Failed to load language resource '{culture}'. Exception: {ex}");

            MessageBox.Show(
                $"Failed to load language resource '{culture}'.\n\nError: {ex.Message}",
                "Resource Load Error",
                MessageBoxButton.OK,
                MessageBoxImage.Warning
            );

            try
            {
                var fallbackDict = new ResourceDictionary
                {
                    Source = new Uri(
                        "pack://application:,,,/PROG7312PRACTICE;component/Resources/StringResources.en.xaml",
                        UriKind.Absolute)
                };

                Application.Current.Resources.MergedDictionaries.Add(fallbackDict);
                Debug.WriteLine("[INFO] Fallback to English loaded successfully.");
            }
            catch (Exception fallbackEx)
            {
                Debug.WriteLine($"[CRITICAL] Failed to load English fallback resource. Exception: {fallbackEx}");

                MessageBox.Show(
                    $"Failed to load English fallback resource.\n\nError: {fallbackEx.Message}",
                    "Critical Resource Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }


}
}
