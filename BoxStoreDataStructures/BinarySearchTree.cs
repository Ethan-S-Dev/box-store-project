using System;
using System.Collections;
using System.Collections.Generic;

namespace BoxStoreDataStructures
{
    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public void Add(T value, Comparison<T> comparer)
        {
            count++;
            if (root == null) { root = new BinarySearchTreeNode<T>(value); return; };
            BinarySearchTreeNode<T> tmp = root;

            while (true)
            {
                if (comparer(value, tmp.Value) < 0)
                {
                    if (tmp.Left == null)
                    {
                        tmp.Left = new BinarySearchTreeNode<T>(value);
                        break;
                    }

                    tmp = tmp.Left;
                }
                else
                {
                    if (tmp.Right == null)
                    {
                        tmp.Right = new BinarySearchTreeNode<T>(value);
                        break;
                    }
                    tmp = tmp.Right;
                }
            }
        }
        public void Add(T value)
        {
            count++;
            if (root == null) { root = new BinarySearchTreeNode<T>(value); return; };
            BinarySearchTreeNode<T> tmp = root;

            while (true)
            {
                if (value.CompareTo(tmp.Value) < 0)
                {
                    if (tmp.Left == null)
                    {
                        tmp.Left = new BinarySearchTreeNode<T>(value);
                        break;
                    }

                    tmp = tmp.Left;
                }
                else
                {
                    if (tmp.Right == null)
                    {
                        tmp.Right = new BinarySearchTreeNode<T>(value);
                        break;
                    }
                    tmp = tmp.Right;
                }
            }
        }
        public void Add(BinarySearchTreeNode<T> head, T value)
        {
            var comp = value.CompareTo(head.Value);
            if (comp < 0) { if (head.Left == null){ head.Left = new BinarySearchTreeNode<T>(value); count++; } else Add(head.Left, value); }
            else { if (head.Right == null) { head.Right = new BinarySearchTreeNode<T>(value); count++; } else Add(head.Right, value); }
            
        }

        public bool Remove(T value)
        {
            BinarySearchTreeNode<T> toRemoveFather = null;
            BinarySearchTreeNode<T> toRemove = GetNodeOfValue(root, value, ref toRemoveFather);
            if (toRemove == null) return false;

            if (toRemove.IsLeaf)
            {
                if (toRemoveFather == null) root = null;
                else
                if (value.CompareTo(toRemoveFather.Value) < 0) toRemoveFather.Left = null;
                else toRemoveFather.Right = null;

                count--;
                return true;
            }

            if (toRemove.Left != null && toRemove.Right != null)
            {
                BinarySearchTreeNode<T> toSwitchFather = toRemove;
                BinarySearchTreeNode<T> toSwithc = GetSmalestNodeInTree(toRemove.Right, ref toSwitchFather);

                toSwitchFather.Left = toSwithc.Right;

                toSwithc.Left = toRemove.Left;
                toSwithc.Right = toRemove.Right;

                if (toRemoveFather == null) { root = toSwithc; return true; }

                if (value.CompareTo(toRemoveFather.Value) < 0) toRemoveFather.Left = toSwithc;
                else toRemoveFather.Right = toSwithc;


                count--;
                return true;
            }


            if (toRemoveFather == null)
            {
                if (toRemove.Left != null)
                    root = toRemove.Left;
                else
                    root = toRemove.Right;

                count--;
                return true;
            }
            if (value.CompareTo(toRemoveFather.Value) < 0)
            {
                if (toRemove.Left != null)
                    toRemoveFather.Left = toRemove.Left;
                else
                    toRemoveFather.Left = toRemove.Right;

                count--;
                return true;
            }

            if (toRemove.Left != null)
                toRemoveFather.Right = toRemove.Left;
            else
                toRemoveFather.Right = toRemove.Right;

            count--;
            return true;
        }
        public void PrintInOrderLeft()
        {
            if (root == null) { Console.WriteLine("[]"); return; }
            Console.Write("[");
            root.PrintOrdered();

            Console.WriteLine("]");
        }
        public string ToStringInOrder()
        {
            if (root == null) return "[]";
            T[] arr = new T[count];
            int pos = 0;
            root.ToStringInOrder(ref arr, ref pos);
            return $"[{string.Join(",", arr)}]";
        }
        public string ToStringByLevel()
        {

            if (root == null) return "[]";

            System.Collections.Generic.Queue<BinarySearchTreeNode<T>> queue = new System.Collections.Generic.Queue<BinarySearchTreeNode<T>>(count);
            string[] arr = new string[count];
            queue.Enqueue(root);
            queue.Enqueue(null);
            BinarySearchTreeNode<T> current;
            int pos = 0;

            while (true)
            {
                current = queue.Dequeue();
                if (current != null)
                {
                    arr[pos++] = current.Value.ToString();
                    if (current.Left != null) queue.Enqueue(current.Left);
                    if (current.Right != null) queue.Enqueue(current.Right);
                }
                else
                {
                    if (queue.Count == 0) break;
                    queue.Enqueue(null);
                }

            }

            return $"[{string.Join(",", arr)}]";
        }
        public int GetDepth()
        {
            return GetDepth(root);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new BSTIterator(root, count);
        }

        protected BinarySearchTreeNode<T> HeadNode => root;
        protected BinarySearchTreeNode<T> SearchNode(T value)
        {
            BinarySearchTreeNode<T> temp = root;
            do
            {
                if (temp.Value.CompareTo(value) == 0) return temp;
                if (temp.Value.CompareTo(value) < 0) temp = temp.Right;
                else temp = temp.Left;
            } while (!temp.IsLeaf);

            return null;
        }
        protected class BSTIterator : IEnumerator<T>
        {
            public T Current => nodesSorted[index];
            public BSTIterator(BinarySearchTreeNode<T> root, int length)
            {
                this.nodesSorted = new T[length];
                InOrder(root);
            }
            public bool MoveNext()
            {
                if (index < nodesSorted.Length)
                {
                    index++;
                    return true;
                }
                return false;
            }
            public void Reset()
            {
                index = 0;
            }
            public void Dispose()
            {

            }
            
            T[] nodesSorted;
            int pos = 0;
            int index = 0;
            object IEnumerator.Current => Current;
            private void InOrder(BinarySearchTreeNode<T> root)
            {

                if (root == null)
                {
                    return;
                }

                InOrder(root.Left);
                nodesSorted[pos++] = root.Value;
                InOrder(root.Right);
            }
        }     

        int count = 0;
        BinarySearchTreeNode<T> root;

        private BinarySearchTreeNode<T> GetSmalestNodeInTree(BinarySearchTreeNode<T> tree, ref BinarySearchTreeNode<T> father)
        {
            if (tree.Left == null) return tree;
            father = tree;
            return GetSmalestNodeInTree(tree.Left, ref father);
        }
        private BinarySearchTreeNode<T> GetBigestLeafInTree(BinarySearchTreeNode<T> tree, ref BinarySearchTreeNode<T> father)
        {
            if (tree.Left == null && tree.Right == null) return tree;

            father = tree;
            if (tree.Right != null)
                return GetBigestLeafInTree(tree.Right, ref father);

            return GetBigestLeafInTree(tree.Left, ref father);
        }
        private BinarySearchTreeNode<T> GetNodeOfValue(BinarySearchTreeNode<T> tree, T value, ref BinarySearchTreeNode<T> father)
        {
            if (tree.Value.CompareTo(value) == 0) return tree;

            if (tree.Value.CompareTo(value) > 0)
                if (tree.Left != null)
                {
                    father = tree;
                    return GetNodeOfValue(tree.Left, value, ref father);
                }
            if (tree.Value.CompareTo(value) < 0)
                if (tree.Right != null)
                {
                    father = tree;
                    return GetNodeOfValue(tree.Right, value, ref father);
                }

            return null;
        }
        private int GetDepth(BinarySearchTreeNode<T> node)
        {
            if (node == null) return 0;
            return Math.Max(GetDepth(node.Left), GetDepth(node.Right)) + 1;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class BinarySearchTreeNode<T> where T : IComparable<T>
    {
        public T Value;
        public bool IsLeaf => (Left == null && Right == null);
        public BinarySearchTreeNode<T> Left;
        public BinarySearchTreeNode<T> Right;

        public BinarySearchTreeNode(T value)
        {
            Value = value;
        }

        
        public void PrintOrdered()
        {
            if (Left != null) Left.PrintOrdered();
            Console.Write($",{Value}");
            if (Right != null) Right.PrintOrdered();
        }
        public void ToStringInOrder(ref T[] arr, ref int pos)
        {
            if (Left != null) Left.ToStringInOrder(ref arr, ref pos);
            arr[pos] = Value;
            pos++;
            if (Right != null) Right.ToStringInOrder(ref arr, ref pos);
        }
    }


}
