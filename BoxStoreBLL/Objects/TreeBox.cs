using System;
using BoxStoreDataStructures;
using BoxStoreModels;

namespace BoxStoreBLL
{
    class LinkedTreeBox :  IComparable<LinkedTreeBox>
    {
        public LinkedListNode<Box> BoxNode { get; set; }
        public double Y => BoxNode.Value.Y;
        public int CompareTo(LinkedTreeBox other) => Y.CompareTo(other.Y);
        public int CompareTo(double otherY) => Y.CompareTo(otherY);
    }
}
