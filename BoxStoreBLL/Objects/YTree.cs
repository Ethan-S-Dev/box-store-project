using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxStoreDataStructures;
using BoxStoreModels;

namespace BoxStoreBLL
{
    class YTree : BinarySearchTree<LinkedTreeBox>, IComparable<YTree>
    {
        public double X { get; }
        public BinarySearchTreeNode<LinkedTreeBox> Root => HeadNode;
        public YTree(double x)
        {
            X = x;
        }
        public int CompareTo(double otherX) => this.X.CompareTo(otherX);
        public int CompareTo(YTree other) => this.X.CompareTo(other.X);
        public BinarySearchTreeNode<LinkedTreeBox> Search(double y)
        {
            var temp = HeadNode;
            while(true)
            { 
                if (temp == null) break;
                var comp = temp.Value.CompareTo(y);
                if (comp == 0) break;
                if (comp > 0) temp = temp.Left;
                else temp = temp.Right;
            }
            return temp;           
        }    
    }
}
