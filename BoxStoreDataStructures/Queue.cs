using System.Text;

namespace BoxStoreDataStructures
{
    public class Queue<T>
    {
        public int Count => count;
        public bool IsFull => count == arr.Length;
        public bool IsEmpty => count == 0;
        public int Capacity => arr.Length;

        public Queue(int capacity)
        {
            first = 0;
            last = 0;
            count = 0;
            arr = new T[capacity];
        }

        public bool Enqueue(T item)
        {
            if (IsFull) return false;
            arr[last] = item;
            last = (last + 1) % arr.Length;
            count++;
            return true;
        }
        public bool Dequeue(out T item)
        {
            item = default;
            if (IsEmpty) return false;
            item = arr[first];
            first = (first + 1) % arr.Length;
            count--;
            return true;
        }
        public bool Peek(out T item)
        {
            item = default;
            if (IsEmpty) return false;
            item = arr[first];
            return true;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("(");
            if (IsEmpty) { sb.Append(")"); return sb.ToString(); }
            sb.Append(arr[first]);
            int counter = 1;
            for (int i = (first + 1) % arr.Length; counter < Count; i = (i + 1) % arr.Length)
            {
                counter++;
                sb.Append($",{arr[i]}");
            }
            sb.Append(")");
            return sb.ToString();
        }

        T[] arr;
        int last;
        int first;
        int count;
    }
}
