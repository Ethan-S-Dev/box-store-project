using System;
using BoxStoreDataStructures;
using BoxStoreModels;

namespace BoxStoreBLL
{
    class XTree : BinarySearchTree<YTree>
    {
        public void AddBox(LinkedTreeBox box)
        {
            var node = Search(box.BoxNode.Value.X,out BinarySearchTreeNode<YTree> xFather);
            if(node == null)
            {
                var yTree = new YTree(box.BoxNode.Value.X);
                yTree.Add(box);

                if (xFather != null) Add(xFather, yTree);
                else Add(yTree);
            }
            else
            {
                var linkTreeBox = node.Value.Search(box.BoxNode.Value.Y, out BinarySearchTreeNode<LinkedTreeBox> yFather);
                if (linkTreeBox == null)
                {
                    if (yFather != null) node.Value.Add(yFather, box);
                    else node.Value.Add(box);
                }
                else
                    throw new ArgumentException("Box all ready Exist.");         
            }

        }
        public BinarySearchTreeNode<YTree> Root => HeadNode;
        public LinkedTreeBox RemoveBox(Box box)
        {
            var node = Search(box.X,out _);
            if (node == null) throw new ArgumentException("Box doesnt exist.");
            var linkTreeBox = node.Value.Search(box.Y, out _);
            if (!node.Value.Remove(linkTreeBox.Value)) throw new ArgumentException("Box doesnt exist.");
            if (node.Value.Root == null) Remove(node.Value);
            return linkTreeBox.Value;
        }
        public bool BoxExist(Box box, out LinkedTreeBox res)
        {
            res = null;
            var node = Search(box.X, out _);
            if (node == null) return false;

            var linkTreeBox = node.Value.Search(box.Y, out _);
            if (linkTreeBox == null) return false;

            res = linkTreeBox.Value;
            return true;

        }
        public BinarySearchTreeNode<YTree> Search(double x, out BinarySearchTreeNode<YTree> father)
        {
            father = null;
            var temp = HeadNode;
            while (true)
            {
                if (temp == null) break;
                var comp = temp.Value.CompareTo(x);
                if (comp == 0) break;
                father = temp;
                if (comp > 0) temp = temp.Left;
                else temp = temp.Right;
            }
            return temp;
        }
    }
}
