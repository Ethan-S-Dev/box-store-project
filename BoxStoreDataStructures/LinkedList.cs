using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BoxStoreDataStructures
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public LinkedList()
        {
            counter = 0;
        }
        public LinkedListNode<T> StartNode { get { return first;  } set { first = value; } }
        public LinkedListNode<T> EndNode { get { return last; } set { last = value; } }
        public int Count { get { return counter; } set { counter = value; } }
        public void AddFirst(T value)
        {
            LinkedListNode<T> nNode = new LinkedListNode<T>(value);
            if (counter == 0)
            {
                first = nNode;
                last = first;
                counter++;
                return;
            }
            nNode.Next = first;
            first.Prive = nNode;
            first = nNode;
            counter++;
        }
        public void AddLast(T value)
        {
            LinkedListNode<T> nNode = new LinkedListNode<T>(value);
            if (counter == 0)
            {
                last = nNode;
                first = last;
                counter++;
                return;
            }
            last.Next = nNode;
            nNode.Prive = last;
            last = nNode;
            counter++;
        }
        public void AddLast(LinkedListNode<T> node)
        {
            node.Prive = null;
            node.Next = null;
            if (counter == 0)
            {
                last = node;
                first = last;
                counter++;
                return;
            }
            last.Next = node;
            node.Prive = last;
            last = node;
            counter++;
        }
        public void AddFirst(LinkedListNode<T> node)
        {
            node.Next = null;
            node.Prive = null;
            if (counter == 0)
            {
                first = node;
                last = first;
                counter++;
                return;
            }
            node.Next = first;
            first.Prive = node;
            first = node;
            counter++;
        }

        public T RemoveFirst()
        {
            if (counter == 0)
                throw new InvalidOperationException("List is empty!");
            LinkedListNode<T> temp = first;
            if (counter == 1)
            {
                first = null;
                last = null;
            }
            else
            {
                first = first.Next;
                first.Prive = null;    
            }
            counter--;
            return temp.Value;
        }
        public T RemoveLast()
        {
            if (counter == 0)
                throw new InvalidOperationException("List is empty!");
            LinkedListNode<T> temp = last;
            if (counter > 1)
            {
                last = last.Prive;
                last.Next = null;
            }
            else
            {
                first = null;
                last = null;
            }
            counter--;
            return temp.Value;
        }
        public T RemoveNode(LinkedListNode<T> node)
        {
            if (counter == 0)
                throw new InvalidOperationException("List is empty!");
            if (node.Next == null)  return RemoveLast();
            if (node.Prive == null) return RemoveFirst();

            node.Prive.Next = node.Next;
            counter--;
            return node.Value;
        }

        public bool GetAt(int position, out T value)
        {
            value = default(T);

            try
            {
                LinkedListNode<T> node = GetNode(position);
                value = node.Value;
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }
        public bool AddAt(int position, T value)
        {

            if (position == 0)
            {
                AddFirst(value);
                return true;
            }
            if (position == Count)
            {
                AddLast(value);
                return true;
            }

            LinkedListNode<T> temp;
            try
            {
                temp = GetNode(position - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }

            LinkedListNode<T> nNode = new LinkedListNode<T>(value);
            nNode.Prive = temp;
            nNode.Next = temp.Next;
            temp.Next.Prive = nNode;
            temp.Next = nNode;
            counter++;
            return true;
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("(");
            if (counter > 0)
                str.Append(first.Value.ToString());
            LinkedListNode<T> temp = first;
            for (int i = 1; i < Count; i++)
            {
                temp = temp.Next;
                str.Append($",{temp.Value}");
            }
            str.Append(')');

            return str.ToString();
        }

        int counter;
        LinkedListNode<T> first;
        LinkedListNode<T> last;

        public LinkedListNode<T> GetNode(int position)
        {
            if (position < 0 || position >= Count)
                throw new ArgumentOutOfRangeException();
            LinkedListNode<T> pointer;
            if (Count / 2 >= position)
            {
                pointer = first;
                for (int i = 0; i < position; i++)
                {
                    pointer = pointer.Next;
                }

            }
            else
            {
                pointer = last;
                for (int i = 1; i < counter - position; i++)
                {
                    pointer = pointer.Prive;
                }
            }

            return pointer;
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = first;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class LinkedListNode<T>
    {
        public T Value { get; set; }
        public LinkedListNode<T> Prive { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T value)
        {
            Value = value;
        }
    }
}
