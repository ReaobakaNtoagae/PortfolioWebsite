using System;
using System.Collections.Generic;

namespace PROG7312PRACTICE.DataStructures
{
    public class RedBlackTree<T> where T : class
    {
        private enum Color { Red, Black }

        private class Node
        {
            public string Key;
            public List<T> DataList; // allow duplicates
            public Node? Left, Right, Parent;
            public Color NodeColor;

            public Node(string key, T data)
            {
                Key = key;
                DataList = new List<T> { data };
                NodeColor = Color.Red;
            }
        }

        private Node? root;

        public void Insert(string key, T data)
        {
            Node newNode = new Node(key, data);
            root = BstInsert(root, newNode);
            FixViolation(newNode);
        }

        private Node BstInsert(Node? root, Node node)
        {
            if (root == null) return node;

            int cmp = string.Compare(node.Key, root.Key);
            if (cmp < 0)
            {
                root.Left = BstInsert(root.Left, node);
                root.Left.Parent = root;
            }
            else if (cmp > 0)
            {
                root.Right = BstInsert(root.Right, node);
                root.Right.Parent = root;
            }
            else
            {
                root.DataList.Add(node.DataList[0]); // duplicate key
            }
            return root;
        }

        private void RotateLeft(Node x)
        {
            var y = x.Right!;
            x.Right = y.Left;
            if (y.Left != null) y.Left.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;
            y.Left = x;
            x.Parent = y;
        }

        private void RotateRight(Node y)
        {
            var x = y.Left!;
            y.Left = x.Right;
            if (x.Right != null) x.Right.Parent = y;
            x.Parent = y.Parent;
            if (y.Parent == null) root = x;
            else if (y == y.Parent.Left) y.Parent.Left = x;
            else y.Parent.Right = x;
            x.Right = y;
            y.Parent = x;
        }

        private void FixViolation(Node z)
        {
            while (z.Parent != null && z.Parent.NodeColor == Color.Red)
            {
                if (z.Parent == z.Parent.Parent?.Left)
                {
                    var y = z.Parent.Parent.Right;
                    if (y != null && y.NodeColor == Color.Red)
                    {
                        z.Parent.NodeColor = Color.Black;
                        y.NodeColor = Color.Black;
                        z.Parent.Parent.NodeColor = Color.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            RotateLeft(z);
                        }
                        z.Parent.NodeColor = Color.Black;
                        z.Parent.Parent!.NodeColor = Color.Red;
                        RotateRight(z.Parent.Parent);
                    }
                }
                else
                {
                    var y = z.Parent.Parent?.Left;
                    if (y != null && y.NodeColor == Color.Red)
                    {
                        z.Parent.NodeColor = Color.Black;
                        y.NodeColor = Color.Black;
                        z.Parent.Parent!.NodeColor = Color.Red;
                        z = z.Parent.Parent!;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RotateRight(z);
                        }
                        z.Parent.NodeColor = Color.Black;
                        z.Parent.Parent!.NodeColor = Color.Red;
                        RotateLeft(z.Parent.Parent);
                    }
                }
            }
            root!.NodeColor = Color.Black;
        }

        // Search by key
        public IEnumerable<T> Search(string key)
        {
            var node = root;
            while (node != null)
            {
                int cmp = string.Compare(key, node.Key);
                if (cmp < 0) node = node.Left;
                else if (cmp > 0) node = node.Right;
                else return node.DataList; // found
            }
            return new List<T>(); // not found
        }
    }
}
