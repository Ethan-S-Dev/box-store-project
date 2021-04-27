using System;
using BoxStoreDataStructures;
using BoxStoreModels;

namespace BoxStoreBLL
{
    class XTree : BinarySearchTree<YTree>
    {
        public void AddBox(LinkedTreeBox box)
        {
            var node = Search(box.BoxNode.Value.X);
            if(node == null)
            {
                var yTree = new YTree(box.BoxNode.Value.X);
                yTree.Add(box);
                Add(yTree);
            }
            else
            {
                var linkTreeBox = node.Value.Search(box.BoxNode.Value.Y);
                if (linkTreeBox == null)
                {
                    node.Value.Add(box);
                }
                else
                    throw new ArgumentException("Box all ready Exist.");         
            }

        }
        public BinarySearchTreeNode<YTree> Root => HeadNode;
        public LinkedTreeBox RemoveBox(Box box)
        {
            var node = Search(box.X);
            if (node == null) throw new ArgumentException("Box doesnt exist.");
            var linkTreeBox = node.Value.Search(box.Y);
            if (!node.Value.Remove(linkTreeBox.Value)) throw new ArgumentException("Box doesnt exist.");
            if (node.Value.Root == null) Remove(node.Value);
            return linkTreeBox.Value;
        }
        public bool BoxExist(Box box, out LinkedTreeBox res)
        {
            res = null;
            var node = Search(box.X);
            if (node == null) return false;

            var linkTreeBox = node.Value.Search(box.Y);
            if (linkTreeBox == null) return false;

            res = linkTreeBox.Value;
            return true;

        }
        public BinarySearchTreeNode<YTree> Search(double x)
        {
            var temp = HeadNode;
            while (true)
            {
                if (temp == null) break;
                var comp = temp.Value.CompareTo(x);
                if (comp == 0) break;
                if (comp > 0) temp = temp.Left;
                else temp = temp.Right;
            }
            return temp;
        }
    }
}
