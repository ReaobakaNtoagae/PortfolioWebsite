using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PROG7312PRACTICE.Models;

namespace PROG7312PRACTICE
{
    public partial class ServiceRequestWindow : Window
    {
        private ServiceRequestManager manager;

        public ServiceRequestWindow()
        {
            InitializeComponent();
            manager = new ServiceRequestManager();
            LoadLists();
        }

        private void LoadLists()
        {
            // Load categories (from BasicTree)
            CategoryList.ItemsSource = manager.GetCategoryNames();

            // Load high-priority requests (from Heap)
            RefreshPriorityList();

            // Load all recent requests (ordered by progress)
            RefreshRecentRequests();
        }

        private void RefreshPriorityList()
        {
            var highPriority = manager.GetHighPriorityRequests()
                                      .OrderByDescending(r => r.Progress)
                                      .ToList();
            PriorityList.ItemsSource = highPriority;
        }

        private void RefreshRecentRequests()
        {
            RequestsList.ItemsSource = manager.GetAllRequests()
                                              .OrderByDescending(r => r.Progress)
                                              .ToList();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchText.Text.Trim();
            if (string.IsNullOrEmpty(query))
            {
                RefreshRecentRequests();
                return;
            }

            var results = manager.GetAllRequests()
                                 .Where(r => r.RequestId.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                             r.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                             r.Status.Contains(query, StringComparison.OrdinalIgnoreCase))
                                 .OrderByDescending(r => r.Progress)
                                 .ToList();

            if (results.Any())
            {
                RequestsList.ItemsSource = results;
                DisplayRequest(results.First());
            }
            else
            {
                MessageBox.Show($"No matching request found for '{query}'.");
            }
        }

        private void RequestsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RequestsList.SelectedItem is ServiceRequest req)
            {
                DisplayRequest(req);
            }
        }

        private void DisplayRequest(ServiceRequest req)
        {
            // Update details
            TxtId.Text = $"ID: {req.RequestId}";
            TxtDescription.Text = $"Description: {req.Description}";
            TxtDepartment.Text = $"Department: {req.Department}";
            TxtStatus.Text = $"Status: {req.Status}";
            TxtPriority.Text = $"Priority: {req.Priority}";

            // Update history
            HistoryList.ItemsSource = req.History.Any() ? req.History : new List<string>
            {
                "Request created",
                "Assigned to department",
                req.Status != "Pending" ? $"Status updated to {req.Status}" : ""
            };

            // Draw department network graph highlighting selected department
            DrawServiceGraph(req.Department);
        }

        private void DrawServiceGraph(string? highlightDept = null)
        {
            GraphCanvas.Children.Clear();

            // Get all unique department names
            var departments = manager.GetAllRequests()
                                     .Select(r => r.Department)
                                     .Distinct()
                                     .ToList();

            double canvasWidth = GraphCanvas.ActualWidth > 0 ? GraphCanvas.ActualWidth : 360;
            double canvasHeight = GraphCanvas.ActualHeight > 0 ? GraphCanvas.ActualHeight : 250;
            int n = departments.Count;
            var positions = new Dictionary<string, Point>();

            // Circle layout
            double radius = Math.Min(canvasWidth, canvasHeight) / 2 - 50;
            double centerX = canvasWidth / 2;
            double centerY = canvasHeight / 2;

            for (int i = 0; i < n; i++)
            {
                double angle = 2 * Math.PI * i / n;
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                positions[departments[i]] = new Point(x, y);
            }

            // Draw edges using Graph
            foreach (var dept in departments)
            {
                foreach (var edge in manager.GetDepartmentConnections(dept))
                {
                    var line = new Line
                    {
                        X1 = positions[edge.From.Id].X,
                        Y1 = positions[edge.From.Id].Y,
                        X2 = positions[edge.To.Id].X,
                        Y2 = positions[edge.To.Id].Y,
                        Stroke = Brushes.Gray,
                        StrokeThickness = 2
                    };
                    GraphCanvas.Children.Add(line);
                }
            }

            // Draw department nodes and labels
            foreach (var dept in departments)
            {
                var ellipse = new Ellipse
                {
                    Width = 40,
                    Height = 40,
                    Fill = dept == highlightDept ? Brushes.MediumSlateBlue : Brushes.LightGray,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                Canvas.SetLeft(ellipse, positions[dept].X - 20);
                Canvas.SetTop(ellipse, positions[dept].Y - 20);
                GraphCanvas.Children.Add(ellipse);

                var label = new TextBlock
                {
                    Text = dept,
                    FontSize = 10,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Black,
                    TextAlignment = TextAlignment.Center,
                    Width = 80
                };
                Canvas.SetLeft(label, positions[dept].X - 40);
                Canvas.SetTop(label, positions[dept].Y + 22);
                GraphCanvas.Children.Add(label);
            }
        }
    

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }
}
}
