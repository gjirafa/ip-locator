using System;

namespace IpLocator.Models
{
    public class RedBlackTree<T> where T : class
    {
        private const bool Red = true;
        private const bool Black = false;

        private Node _root;

        private class Node
        {
            public long Key { get; }
            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
            public int Size { get; set; }

            public Node(long key, T value, bool color, int size)
            {
                Key = key;
                Value = value;
                Color = color;
                Size = size;
            }
        }

        private static bool IsRed(Node x)
        {
            if (x == null) return false;
            return x.Color == Red;
        }
        private static int Size(Node x)
        {
            return x?.Size ?? 0;
        }

        public bool IsEmpty()
        {
            return _root == null;
        }

        // Get Value from tree
        public T Get(long key)
        {
            return key == 0 ? null : Get(_root, key);
        }
        private static T Get(Node x, long key)
        {
            while (x != null)
            {
                var cmp = key.CompareTo(x.Key);
                if (cmp < 0)
                    x = x.Left;
                else if (cmp > 0)
                    x = x.Right;
                else
                    return x.Value;
            }
            return null;
        }


        // Insert key and value into the tree
        public void Put(long key, T value)
        {
            if (key == 0) throw new Exception("Key value is null");
            if (value == null) throw new Exception("Value of put is null!");
            _root = Put(_root, key, value);
            _root.Color = Black;
        }
        private static Node Put(Node h, long key, T value)
        {
            if (h == null) return new Node(key, value, Red, 1);

            var cmp = key.CompareTo(h.Key);
            if (cmp < 0)
                h.Left = Put(h.Left, key, value);
            else if (cmp > 0)
                h.Right = Put(h.Right, key, value);
            else
                h.Value = value;

            if (IsRed(h.Right) && !IsRed(h.Left)) h = RotateLeft(h);
            if (IsRed(h.Left) && IsRed(h.Left.Left)) h = RotateRight(h);
            if (IsRed(h.Left) && IsRed(h.Right)) FlipColors(h);
            h.Size = Size(h.Left) + Size(h.Right) + 1;
            return h;
        }


        private static Node RotateRight(Node h)
        {
            var x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.Color = x.Right.Color;
            x.Right.Color = Red;
            x.Size = h.Size;
            h.Size = Size(h.Left) + Size(h.Right) + 1;
            return x;
        }
        private static Node RotateLeft(Node h)
        {
            var x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.Color = x.Left.Color;
            x.Left.Color = Red;
            x.Size = h.Size;
            h.Size = Size(h.Left) + Size(h.Right) + 1;
            return x;
        }
        private static void FlipColors(Node h)
        {
            h.Color = !h.Color;
            h.Left.Color = !h.Left.Color;
            h.Right.Color = !h.Right.Color;
        }
        public long Floor(long key)
        {
            if (key == 0)
                throw new Exception("argument to floor() is null");
            if (IsEmpty())
                throw new Exception("called floor() with empty symbol table");
            var x = Floor(_root, key);

            return x?.Key ?? -1;
        }

        private static Node Floor(Node x, long key)
        {
            if (x == null)
                return null;

            var cmp = key.CompareTo(x.Key);
            if (cmp == 0)
                return x;
            if (cmp < 0)
                return Floor(x.Left, key);

            var t = Floor(x.Right, key);
            return t ?? x;
        }
    }
}
