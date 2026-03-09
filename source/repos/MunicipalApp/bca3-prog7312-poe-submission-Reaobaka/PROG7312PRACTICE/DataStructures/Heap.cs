using System;
using System.Collections.Generic;
using PROG7312PRACTICE.Models;

namespace PROG7312PRACTICE.DataStructures
{
    public class Heap<T> where T : ServiceRequest
    {
        private List<T> heap = new();

        private int Compare(T a, T b)
        {
            int pa = PriorityValue(a.Priority);
            int pb = PriorityValue(b.Priority);
            return pb.CompareTo(pa); // higher priority = higher weight
        }

        private int PriorityValue(string priority) =>
            priority switch
            {
                "High" => 3,
                "Medium" => 2,
                "Low" => 1,
                _ => 0
            };

        public void Insert(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }

        public T ExtractMax()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            var max = heap[0];
            heap[0] = heap[^1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return max;
        }

        private void HeapifyUp(int i)
        {
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (Compare(heap[i], heap[parent]) > 0)
                {
                    (heap[i], heap[parent]) = (heap[parent], heap[i]);
                    i = parent;
                }
                else break;
            }
        }

        private void HeapifyDown(int i)
        {
            while (i < heap.Count)
            {
                int left = 2 * i + 1, right = 2 * i + 2, largest = i;

                if (left < heap.Count && Compare(heap[left], heap[largest]) > 0)
                    largest = left;
                if (right < heap.Count && Compare(heap[right], heap[largest]) > 0)
                    largest = right;

                if (largest == i) break;

                (heap[i], heap[largest]) = (heap[largest], heap[i]);
                i = largest;
            }
        }

        public bool IsEmpty() => heap.Count == 0;
        public List<T> ToList() => new List<T>(heap);
    }
}
