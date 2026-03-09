using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312PRACTICE.Models
{
    public class Issue
    {

        public string Location { get; set; }

        public Category category { get; set; }

        public string Description { get; set; }

        public string FilePath { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;   


     }

    public enum Category
    {
        Sanitation,
        Roads,
        Waste,
        Utilities,
        Public_Safety,
        Public_Spaces
    }
}
