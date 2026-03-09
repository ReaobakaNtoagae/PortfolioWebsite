using System;
using System.Collections.Generic;

namespace PROG7312PRACTICE.DataStructures
{
    public class AvlTree<T> where T : class
    {
        private class Node
        {
            public string Key;
            public List<T> DataList; // allow duplicates
            public int Height;
            public Node? Left, Right;

            public Node(string key, T data)
            {
                Key = key;
                DataList = new List<T> { data };
                Height = 1;
            }
        }

        private Node? root;

        private int Height(Node? n) => n?.Height ?? 0;
        private int Balance(Node? n) => n == null ? 0 : Height(n.Left) - Height(n.Right);

        private Node RotateRight(Node y)
        {
            var x = y.Left!;
            var T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            return x;
        }

        private Node RotateLeft(Node x)
        {
            var y = x.Right!;
            var T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            return y;
        }

        public void Insert(string key, T data) => root = InsertRec(root, key, data);

        private Node InsertRec(Node? node, string key, T data)
        {
            if (node == null) return new Node(key, data);

            int cmp = string.Compare(key, node.Key);
            if (cmp < 0) node.Left = InsertRec(node.Left, key, data);
            else if (cmp > 0) node.Right = InsertRec(node.Right, key, data);
            else node.DataList.Add(data); // duplicate key allowed

            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            int balance = Balance(node);

            if (balance > 1 && string.Compare(key, node.Left!.Key) < 0) return RotateRight(node);
            if (balance < -1 && string.Compare(key, node.Right!.Key) > 0) return RotateLeft(node);
            if (balance > 1 && string.Compare(key, node.Left!.Key) > 0)
            {
                node.Left = RotateLeft(node.Left!);
                return RotateRight(node);
            }
            if (balance < -1 && string.Compare(key, node.Right!.Key) < 0)
            {
                node.Right = RotateRight(node.Right!);
                return RotateLeft(node);
            }

            return node;
        }

        //Search by key
        public IEnumerable<T> Search(string key)
        {
            var node = root;
            while (node != null)
            {
                int cmp = string.Compare(key, node.Key);
                if (cmp < 0) node = node.Left;
                else if (cmp > 0) node = node.Right;
                else return node.DataList;
            }
            return new List<T>(); 
        }
    }
}
