/**
* ServiceRequestManager.cs
* ------------------------
* Author: Reabaka Ntoagae
* Project: PROG7312 Practice Project
* Date: 2025-11-12
* 
* Description:
* This class manages service requests using multiple data structures:
* - Binary Search Tree (BST) for Request ID indexing
* - AVL Tree for Description indexing
* - Red-Black Tree for Status indexing
* - Max Heap for High-Priority Request management
* - Graph for department interconnections
* - Basic Tree for hierarchical service category management
*
* Attribution:
* - BinarySearchTree, AvlTree, RedBlackTree, Heap, Graph, and BasicTree 
*   are custom data structure implementations created within the project 
*   under the namespace PROG7312PRACTICE.DataStructures.
* - Conceptual understanding and structural logic for trees and heaps 
*   are based on standard data structure principles (CLRS, GeeksforGeeks, 
*   and Microsoft documentation).
* - All logic integration, seeding, and usage design written by 
*   Reabaka Ntoagae.
*/
using System.Collections.Generic;
using System.Linq;
using PROG7312PRACTICE.DataStructures;
using PROG7312PRACTICE.Models;

namespace PROG7312PRACTICE
{
    /// <summary>
    /// Manages all ServiceRequest operations using multiple data structures.
    /// </summary>
    public class ServiceRequestManager
    {
        // --- Data Structure Fields ---
        private readonly BinarySearchTree<ServiceRequest> bst = new(); // BST indexed by RequestId
        private readonly AvlTree<ServiceRequest> avl = new();          // AVL indexed by Description
        private readonly RedBlackTree<ServiceRequest> rbt = new();     // RBT indexed by Status
        private readonly Heap<ServiceRequest> heap = new();            // Heap for high-priority requests
        private readonly Graph graph = new();                          // Graph for department network
        private readonly BasicTree categoryTree = new("City Services");// Category tree root node

        // In-memory list of requests
        private readonly List<ServiceRequest> requests = new();

        /// <summary>
        /// Initializes the manager with sample service requests and categories.
        /// </summary>
        public ServiceRequestManager()
        {
            SeedRequests();
        }

        /// <summary>
        /// Seeds the system with demo data for testing and demonstration purposes.
        /// </summary>
        private void SeedRequests()
        {
            // --- Category Seeding ---
            categoryTree.AddChild("City Services", "Water");
            categoryTree.AddChild("City Services", "Roads");
            categoryTree.AddChild("City Services", "Sanitation");
            categoryTree.AddChild("City Services", "Electricity");

            // --- Service Request Seeding ---
            var seedRequests = new List<ServiceRequest>
            {
                new("SR101", "Fix water leak", "Water Dept", "Water", "High", "Pending", 20),
                new("SR102", "Repair pothole", "Roads Dept", "Roads", "Medium", "In Progress", 60),
                new("SR103", "Collect garbage", "Sanitation", "Sanitation", "Low", "Completed", 100),
                new("SR104", "Power outage in block A", "Electricity", "Electricity", "High", "In Progress", 70),
                new("SR105", "Inspect street lights", "Electricity", "Electricity", "Medium", "Pending", 10)
            };

            foreach (var r in seedRequests)
            {
                requests.Add(r);

                // BST keyed by RequestId
                bst.Insert(r.RequestId, r);

                // AVL keyed by Description
                avl.Insert(r.Description, r);

                // RBT keyed by Status
                rbt.Insert(r.Status, r);

                // Heap stores High-priority requests
                if (r.Priority == "High")
                    heap.Insert(r);

                // Graph: node per department
                graph.AddNode(r.Department);
            }

            // --- Graph Connections between Departments ---
            graph.AddEdge("Water Dept", "Sanitation", 1);
            graph.AddEdge("Sanitation", "Roads Dept", 1);
            graph.AddEdge("Electricity", "Water Dept", 1);
            graph.AddEdge("Roads Dept", "Electricity", 1);
        }

        // --- Data Retrieval Methods ---

        public IEnumerable<ServiceRequest> GetAllRequests() => requests;

        public IEnumerable<ServiceRequest> GetHighPriorityRequests() => heap.ToList();

        public IEnumerable<string> GetCategoryNames() => categoryTree.GetCategories();

        public ServiceRequest? SearchById(string requestId) => bst.Search(requestId);

        public IEnumerable<ServiceRequest> SearchByDescription(string description) => avl.Search(description);

        public IEnumerable<ServiceRequest> SearchByStatus(string status) => rbt.Search(status);

        public IEnumerable<Graph.Edge> GetDepartmentConnections(string department) => graph.GetEdges(department);
    }
}
