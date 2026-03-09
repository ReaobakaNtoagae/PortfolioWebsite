using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PROG7312PRACTICE
{
    public partial class Events : Window
    {
        private readonly EventManager _eventManager = new();


        private Dictionary<string, int> searchFrequency;
        private bool _drawerOpen = false;


        public Events()
        {
            InitializeComponent();
            PrepopulateEvents();
            LoadUI();
        }

        private void PrepopulateEvents()
        {
            _eventManager.AddEvent("Spring Festival", "Community", new DateTime(2025, 10, 20),
                "Local celebration with food, games, and music.",
                "https://i.pinimg.com/736x/1b/c5/33/1bc533f8dbd3fc8b6351c6cace8d0b7e.jpg");

            _eventManager.AddEvent("Farmers Market", "Food", new DateTime(2025, 11, 18),
                "Fresh produce from local farms.",
                "https://i.pinimg.com/736x/bb/24/65/bb246574a3b48aec40f2ab8d3dead510.jpg");

            _eventManager.AddEvent("Tech Meetup", "Technology", new DateTime(2025, 11, 25),
                "Networking event for developers and entrepreneurs.",
               "https://i.pinimg.com/1200x/c0/2d/e2/c02de217aee20b40d7b2ccbc08a5dd0d.jpg");

            _eventManager.AddEvent("Soccer Finals", "Sports", new DateTime(2025, 11, 22),
                "Town championship game at the stadium.",
                "https://i.pinimg.com/736x/e5/04/87/e50487a4d6c228394211ce07a6a48c23.jpg");

            _eventManager.AddEvent("Book Club", "Literature", new DateTime(2025, 10, 19),
                "Discussing ‘The Great Gatsby’ at the community hall.",
                "https://i.pinimg.com/1200x/89/ff/f3/89fff37dd4315e3f13c3a7879382f440.jpg");

            _eventManager.AddEvent("Blood Drive", "Health", new DateTime(2025, 10, 21),
                "Organized by the local Red Cross chapter.",
                "https://i.pinimg.com/736x/6a/d3/a4/6ad3a4ddd1f142544fdb9aa8c3c4a357.jpg");
        }



        private void LoadUI()
        {
            EventList.ItemsSource = _eventManager.SearchEvents();

            var categories = _eventManager.GetCategories() ?? new List<string>();
            CategoryFilter.ItemsSource = new List<string> { "All Categories" }.Concat(categories).ToList();
            CategoryFilter.SelectedIndex = 0;

            Recommendations.ItemsSource = _eventManager.GetRecommendedEvents();
            RecentlyViewed.ItemsSource = _eventManager.GetRecentlyViewed();
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string? category = CategoryFilter.SelectedItem?.ToString();
            if (category == "All Categories") category = null;

            var date = DateFilter.SelectedDate;
            EventList.ItemsSource = _eventManager.SearchEvents(category, date);

            Recommendations.ItemsSource = _eventManager.GetRecommendedEvents(category);
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            LoadUI();
        }



        private void NextEvent_Click(object sender, RoutedEventArgs e)
        {
            var next = _eventManager.GetNextEvent();
            if (next != null)
            {
                EventList.ItemsSource = new List<LocalEvent> { next };
                RecentlyViewed.ItemsSource = _eventManager.GetRecentlyViewed();
                Recommendations.ItemsSource = _eventManager.GetRecommendedEvents(next.Category);
            }
        }


        private void OpenInfoPopup_Click(object sender, RoutedEventArgs e)
        {
            var popup = new InfoPopup(
                _eventManager.GetPendingEvents(),
                _eventManager.GetNewEvents(),
                _eventManager.PeekNextEvent()
            );
            popup.Owner = this;
            popup.ShowDialog();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}



