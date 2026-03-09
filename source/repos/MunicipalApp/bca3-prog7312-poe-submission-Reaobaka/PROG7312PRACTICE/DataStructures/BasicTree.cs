using PROG7312PRACTICE.DataStructures;
using PROG7312PRACTICE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312PRACTICE.DataStructures
{
    public class BasicTree
    {
        public class Node
        {
            public string Value;
            public List<Node> Children = new();
            public Node(string value) => Value = value;
        }

        public Node Root;
        public BasicTree(string rootValue) => Root = new Node(rootValue);

        public void AddChild(string parent, string child)
        {
            var p = Find(Root, parent);
            if (p != null) p.Children.Add(new Node(child));
        }

        private Node? Find(Node node, string value)
        {
            if (node.Value == value) return node;
            foreach (var c in node.Children)
            {
                var found = Find(c, value);
                if (found != null) return found;
            }
            return null;
        }

        public List<string> GetCategories()
        {
            var list = new List<string>();
            Traverse(Root, list);
            return list.Skip(1).ToList();
        }

        private void Traverse(Node node, List<string> list)
        {
            list.Add(node.Value);
            foreach (var c in node.Children)
                Traverse(c, list);
        }
    }
}
