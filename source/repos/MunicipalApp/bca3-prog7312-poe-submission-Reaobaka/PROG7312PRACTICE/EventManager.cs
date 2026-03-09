using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG7312PRACTICE
{
    public class EventManager
    {
        private readonly List<LocalEvent> _events = new();
        private readonly Queue<LocalEvent> _recentlyViewed = new();
        private readonly List<LocalEvent> _newEvents = new();


        public void AddEvent(string title, string category, DateTime date, string description, string imageUrl)
        {
            var e = new LocalEvent(title, category, date, description, imageUrl);
            _events.Add(e);
            _newEvents.Add(e);
        }


        public List<LocalEvent> SearchEvents(string? category = null, DateTime? date = null)
        {
            var results = _events.AsEnumerable();

            if (!string.IsNullOrEmpty(category))
                results = results.Where(ev => ev.Category == category);

            if (date != null)
                results = results.Where(ev => ev.Date.Date == date.Value.Date);

            return results.ToList();
        }



        public List<string> GetCategories()
        {
            return _events.Select(e => e.Category).Distinct().ToList();
        }

        public List<LocalEvent> GetRecommendedEvents(string? category = null)
        {
            var recommendations = category == null
                ? _events.Take(3)
                : _events.Where(e => e.Category == category).Take(3);

            return recommendations.ToList();
        }

        public List<LocalEvent> GetRecentlyViewed()
        {
            return _recentlyViewed.Reverse().ToList();
        }

        public List<LocalEvent> GetPendingEvents()
        {
            return _events.Where(e => e.Date > DateTime.Today).Take(5).ToList();
        }

        public List<LocalEvent> GetNewEvents()
        {
            return _newEvents.Take(5).ToList();
        }

        public LocalEvent? PeekNextEvent()
        {
            return _events.Where(e => e.Date >= DateTime.Today).OrderBy(e => e.Date).FirstOrDefault();
        }

        public LocalEvent? GetNextEvent()
        {
            var next = PeekNextEvent();
            if (next != null)
            {
                _recentlyViewed.Enqueue(next);
                if (_recentlyViewed.Count > 5)
                    _recentlyViewed.Dequeue();
            }

            return next;
        }
    }
}
