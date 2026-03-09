using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312PRACTICE
{
    public class LocalEvent
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public LocalEvent(string title, string category, DateTime date, string desc, string image)
        {
            Title = title; Category = category; Date = date; Description = desc; ImageUrl = image;
        }
    }
}
