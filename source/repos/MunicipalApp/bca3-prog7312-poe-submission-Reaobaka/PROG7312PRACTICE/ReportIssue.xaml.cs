/**
* ReportIssue.xaml.cs
* --------------------
* Author: Reabaka Ntoagae
* Project: PROG7312 Practice Project
* Date: 2025-11-12
*
* Description:
* This WPF window allows users to report municipal or service-related issues
* by filling in location, category, description, and optionally attaching a file.
* It includes real-time progress updates, visual feedback animations, 
* and validation to ensure completeness before submission.
*
* Attribution:
* - Core logic, event handling, and UI integration written by Reabaka Ntoagae.
* - Progress bar animation and smooth color transitions implemented using 
*   standard WPF animation patterns from Microsoft Documentation 
*   (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/animation-overview).
* - File handling utilizes OpenFileDialog (System.Windows.Forms) — 
*   official .NET framework class for user file selection.
* - Design pattern follows MVVM principles adapted for a single-window 
*   interaction environment.
*/
using Microsoft.Win32;
using PROG7312PRACTICE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace PROG7312PRACTICE
{

    /// <summary>
    /// Interaction logic for ReportIssue.xaml
    /// </summary>
    public partial class ReportIssue : Window
    {
        private readonly List<Issue> issues = new List<Issue>();
        private string filePath;

        public ReportIssue()
        {
            InitializeComponent();

            // Populate category list dynamically from Enum values
            CategoryListBox.Items.Clear();
            foreach (Category cat in Enum.GetValues(typeof(Category)))
            {
                var item = new ListBoxItem
                {
                    Content = cat.ToLocalizedString(),
                    Tag = cat
                };
                CategoryListBox.Items.Add(item);
            }

            // Initialize progress bar
            UpdateProgress();
        }

        /// <summary>
        /// Updates the progress bar based on form completion.
        /// Changes color dynamically for visual feedback.
        /// </summary>
        private void UpdateProgress()
        {
            int value = 0;

            if (!string.IsNullOrWhiteSpace(LocationTextBox.Text)) value += 25;
            if (CategoryListBox.SelectedItem != null) value += 25;

            string description = new TextRange(DescriptionTextBox.Document.ContentStart, DescriptionTextBox.Document.ContentEnd).Text.Trim();
            if (!string.IsNullOrWhiteSpace(description)) value += 25;

            if (!string.IsNullOrWhiteSpace(filePath)) value += 25;

            ProgressBarReporting.Value = value;

            // Determine color theme based on progress level
            Color targetColor = Colors.DodgerBlue;
            if (value <= 25)
                targetColor = (Color)ColorConverter.ConvertFromString("#2196F3"); // Blue
            else if (value <= 50)
                targetColor = (Color)ColorConverter.ConvertFromString("#FF9800"); // Orange
            else if (value <= 75)
                targetColor = (Color)ColorConverter.ConvertFromString("#9C27B0"); // Purple
            else if (value == 100)
                targetColor = (Color)ColorConverter.ConvertFromString("#4CAF50"); // Green

            // Animate color transition smoothly
            SolidColorBrush brush = ProgressBarReporting.Foreground as SolidColorBrush;
            if (brush == null || brush.IsFrozen)
            {
                brush = new SolidColorBrush(targetColor);
                ProgressBarReporting.Foreground = brush;
            }
            else
            {
                ColorAnimation animation = new ColorAnimation
                {
                    To = targetColor,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                };
                brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            }
        }

        // --- UI Event Handlers ---

        private void LocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProgress();
        }

        private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProgress();
        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProgress();
        }

        /// <summary>
        /// Handles file upload, including image and document preview.
        /// </summary>
        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Supported Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.pdf;*.doc;*.docx;*.txt|" +
                         "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" +
                         "Documents|*.pdf;*.doc;*.docx;*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                string ext = Path.GetExtension(filePath).ToLower();

                // Display image preview if applicable
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".gif")
                {
                    UploadedImage.Source = new BitmapImage(new Uri(filePath));
                }
                else
                {
                    UploadedImage.Source = null;
                    MessageBox.Show($"{FindResource("Msg_DocumentSelected")}: {filePath}",
                                    FindResource("Msg_FileSelected").ToString(),
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }

                UpdateProgress();
            }
        }

        /// <summary>
        /// Validates and submits the reported issue, displaying a confirmation summary.
        /// </summary>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string location = LocationTextBox.Text;
                if (string.IsNullOrWhiteSpace(location))
                {
                    MessageBox.Show(FindResource("Msg_EnterLocation").ToString(),
                                    "Validation Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }

                if (CategoryListBox.SelectedItem == null)
                {
                    MessageBox.Show(FindResource("Msg_SelectCategory").ToString(),
                                    "Validation Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }

                string description = new TextRange(DescriptionTextBox.Document.ContentStart, DescriptionTextBox.Document.ContentEnd).Text.Trim();
                if (string.IsNullOrWhiteSpace(description))
                {
                    MessageBox.Show(FindResource("Msg_ProvideDescription").ToString(),
                                    "Validation Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }

                if (CategoryListBox.SelectedItem is ListBoxItem selectedItem &&
                    selectedItem.Tag is Category category)
                {
                    Issue issue = new Issue
                    {
                        Location = location,
                        Description = description,
                        category = category,
                        FilePath = filePath,
                        SubmittedAt = DateTime.Now
                    };

                    issues.Add(issue);

                    string message = $"{FindResource("Msg_Location")}: {issue.Location}\n" +
                                     $"{FindResource("Msg_Category")}: {selectedItem.Content}\n" +
                                     $"{FindResource("Msg_Description")}: {issue.Description}\n" +
                                     $"{FindResource("Msg_File")}: {issue.FilePath}\n" +
                                     $"{FindResource("Msg_SubmittedAt")}: {issue.SubmittedAt}";

                    MessageBox.Show(message,
                                    FindResource("Msg_SuccessTitle").ToString(),
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);

                    // Reset form
                    LocationTextBox.Clear();
                    DescriptionTextBox.Document.Blocks.Clear();
                    CategoryListBox.SelectedItem = null;
                    UploadedImage.Source = null;
                    filePath = null;

                    UpdateProgress();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Navigates back to the MainWindow.
        /// </summary>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
