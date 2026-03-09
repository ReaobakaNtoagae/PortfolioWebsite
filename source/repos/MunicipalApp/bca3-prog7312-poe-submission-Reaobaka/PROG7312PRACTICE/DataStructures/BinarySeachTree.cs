using System;
using System.Collections.Generic;


namespace PROG7312PRACTICE.DataStructures
{
    public class BinarySearchTree<T> where T : class
    {
        private class Node
        {
            public string Key;
            public T Data;
            public Node? Left;
            public Node? Right;
            public Node(string key, T data)
            {
                Key = key; Data = data;
            }
        }

        private Node? root;

        public void Insert(string key, T data)
        {
            root = InsertRec(root, key, data);
        }

        private Node InsertRec(Node? node, string key, T data)
        {
            if (node == null) return new Node(key, data);
            if (string.Compare(key, node.Key) < 0)
                node.Left = InsertRec(node.Left, key, data);
            else if (string.Compare(key, node.Key) > 0)
                node.Right = InsertRec(node.Right, key, data);
            return node;
        }

        public T? Search(string key)
        {
            var n = root;
            while (n != null)
            {
                int cmp = string.Compare(key, n.Key);
                if (cmp == 0) return n.Data;
                n = cmp < 0 ? n.Left : n.Right;
            }
            return null;
        }
    }
}
