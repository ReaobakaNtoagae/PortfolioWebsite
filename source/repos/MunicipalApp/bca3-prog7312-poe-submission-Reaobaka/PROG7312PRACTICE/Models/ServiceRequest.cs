using System;
using System.Collections.Generic;

namespace PROG7312PRACTICE.Models
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public string RequestId { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }


        public string Priority { get; set; }       // High, Medium, Low

        public string Status { get; set; }         // Pending, In Progress, Completed

        public int Progress { get; set; }          // 0-100 percent


        public List<string> History { get; set; }  // Tracks request updates

        public ServiceRequest(string requestId, string description, string department,
                              string category, string priority, string status, int progress)
        {
            RequestId = requestId;
            Description = description;
            Department = department;
            Category = category;
            Priority = priority;
            Status = status;
            Progress = progress;

            // Initialize history
            History = new List<string>
            {
                "Request Created"
            };
        }

        // Add an update to history
        public void AddHistory(string action)
        {
            History.Add($"{DateTime.Now:HH:mm} - {action}");
        }

        // IComparable used for Heap sorting (High-priority first, then progress)
        public int CompareTo(ServiceRequest? other)
        {
            if (other == null) return 1;

            // High-priority requests come first
            int priorityCompare = PriorityValue(other.Priority) - PriorityValue(this.Priority);
            if (priorityCompare != 0) return priorityCompare;

            // Higher progress comes first
            return other.Progress.CompareTo(this.Progress);
        }

        private int PriorityValue(string priority)
        {
            return priority switch
            {
                "High" => 3,
                "Medium" => 2,
                "Low" => 1,
                _ => 0
            };
        }

        
        public override string ToString() => $"{RequestId} - {Description} [{Status}]";
    }
}
