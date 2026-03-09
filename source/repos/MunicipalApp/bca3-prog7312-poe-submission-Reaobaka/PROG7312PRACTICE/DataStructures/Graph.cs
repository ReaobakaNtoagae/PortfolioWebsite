using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace PROG7312PRACTICE.DataStructures
{
    public class Graph
    {
        public class Node
        {
            public string Id;
            public Point Layout; // optional for UI positioning
            public Node(string id)
            {
                Id = id;
                Layout = new Point(0, 0);
            }
            public override string ToString() => Id;
        }

        public class Edge
        {
            public Node From;
            public Node To;
            public double Weight;
            public Edge(Node from, Node to, double weight)
            {
                From = from;
                To = to;
                Weight = weight;
            }
        }

        private Dictionary<string, Node> nodes = new();
        private Dictionary<string, List<Edge>> adj = new();

        public IEnumerable<Node> Nodes => nodes.Values;
        public IEnumerable<Edge> Edges => adj.Values.SelectMany(x => x);

        public Node AddNode(string id)
        {
            if (!nodes.ContainsKey(id))
            {
                var node = new Node(id);
                nodes[id] = node;
                adj[id] = new List<Edge>();
            }
            return nodes[id];
        }

        public void AddEdge(string fromId, string toId, double weight = 1, bool undirected = true)
        {
            var from = nodes[fromId];
            var to = nodes[toId];
            adj[fromId].Add(new Edge(from, to, weight));
            if (undirected)
                adj[toId].Add(new Edge(to, from, weight));
        }

        // BFS traversal
        public List<Node> BFS(string startId)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<Node>();
            var order = new List<Node>();

            visited.Add(startId);
            queue.Enqueue(nodes[startId]);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                order.Add(node);

                foreach (var e in adj[node.Id])
                {
                    if (!visited.Contains(e.To.Id))
                    {
                        visited.Add(e.To.Id);
                        queue.Enqueue(e.To);
                    }
                }
            }

            return order;
        }

        // DFS traversal
        public List<Node> DFS(string startId)
        {
            var visited = new HashSet<string>();
            var order = new List<Node>();
            DFSRec(nodes[startId], visited, order);
            return order;
        }

        private void DFSRec(Node node, HashSet<string> visited, List<Node> order)
        {
            visited.Add(node.Id);
            order.Add(node);

            foreach (var e in adj[node.Id])
                if (!visited.Contains(e.To.Id))
                    DFSRec(e.To, visited, order);
        }

        public Dictionary<string, double> Dijkstra(string sourceId)
        {
            if (!nodes.ContainsKey(sourceId)) throw new ArgumentException("Source node does not exist.");

            var dist = nodes.Keys.ToDictionary(k => k, _ => double.PositiveInfinity);
            dist[sourceId] = 0;

            // Use Item1/Item2 to reference tuple components in comparer (safer)
            var pq = new SortedSet<(double dist, string id)>(
                Comparer<(double, string)>.Create((a, b) =>
                {
                    int cmp = a.Item1.CompareTo(b.Item1);           // compare distances
                    if (cmp != 0) return cmp;
                    return string.Compare(a.Item2, b.Item2, StringComparison.Ordinal); // tie-breaker: id
                })
            );

            pq.Add((0.0, sourceId));

            while (pq.Count > 0)
            {
                var first = pq.First();
                pq.Remove(first);

                double d = first.Item1;
                string u = first.Item2;

                if (d > dist[u]) continue;

                foreach (var e in adj[u])
                {
                    var v = e.To.Id;
                    var newDist = dist[u] + e.Weight;
                    if (newDist < dist[v])
                    {
                        // If the pq already contains an entry for v with an older distance,
                        // we don't remove it here (SortedSet doesn't support duplicates well).
                        // We simply add the new pair; outdated entries will be skipped when popped.
                        dist[v] = newDist;
                        pq.Add((newDist, v));
                    }
                }
            }

            return dist;
        }


        // Kruskal's Minimum Spanning Tree (MST)
        public List<Edge> KruskalMST()
        {
            var allEdges = Edges.Distinct().OrderBy(e => e.Weight).ToList();
            var uf = new UnionFind(nodes.Keys);
            var mst = new List<Edge>();

            foreach (var e in allEdges)
            {
                if (uf.Find(e.From.Id) != uf.Find(e.To.Id))
                {
                    uf.Union(e.From.Id, e.To.Id);
                    mst.Add(e);
                }
            }

            return mst;
        }

        // Helper class for MST
        private class UnionFind
        {
            private Dictionary<string, string> parent = new();
            private Dictionary<string, int> rank = new();

            public UnionFind(IEnumerable<string> items)
            {
                foreach (var i in items)
                {
                    parent[i] = i;
                    rank[i] = 0;
                }
            }

            public string Find(string x)
            {
                if (parent[x] != x)
                    parent[x] = Find(parent[x]);
                return parent[x];
            }

            public void Union(string a, string b)
            {
                var ra = Find(a);
                var rb = Find(b);
                if (ra == rb) return;

                if (rank[ra] < rank[rb])
                    parent[ra] = rb;
                else if (rank[ra] > rank[rb])
                    parent[rb] = ra;
                else
                {
                    parent[rb] = ra;
                    rank[ra]++;
                }
            }
        }

        // Helper: Get all edges connected to a department
        public IEnumerable<Edge> GetEdges(string id) =>
            adj.ContainsKey(id) ? adj[id] : Enumerable.Empty<Edge>();
    }
}
