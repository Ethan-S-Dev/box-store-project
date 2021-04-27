using System;
using BoxStoreDataStructures;
using BoxStoreModels;
using BoxStoreServices;
using System.Linq;
using col = System.Collections.Generic;

namespace BoxStoreBLL
{
    class DataManger
    {
        const int DAYS_TO_DELETE = 30;

        public EventHandler<col.IEnumerable<Box>> BoxCleanUp;
        public int CountBoxes => boxesByDate.Sum((b) => b.Quantity);

        public void Init()
        {
            boxesByDate = new LinkedList<Box>();
            col.IEnumerable<Box> boxes = dataBase.GetBoxes().OrderBy((b) => b.LastTimeBought);
            foreach (var box in boxes)
            {
                boxesByDate.AddLast(box);
                var linkedTreeBox = new LinkedTreeBox();
                linkedTreeBox.BoxNode = boxesByDate.EndNode;
                boxTree.AddBox(linkedTreeBox);
            }
        }
        public void AddBox(Box box)
        {
            LinkedTreeBox res;
            if (boxTree.BoxExist(box, out res)) { res.BoxNode.Value.Quantity += box.Quantity; dataBase.Save(); return; } // Increase Quantity if exist


            box.LastTimeBought = DateTimeOffset.Now;
            //box.Quantity = 1;

            boxesByDate.AddLast(box); // Add to Linked List

            res = new LinkedTreeBox();
            res.BoxNode = boxesByDate.EndNode;

            boxTree.AddBox(res); // Add to tree


            dataBase.AddBox(box); // Add to database
        }
        public LinkedList<Box> GetAllBoxes() => boxesByDate;
        public void BuyBoxes(Box boxToBuy, int quantity)
        {
            LinkedTreeBox link;
            if (!boxTree.BoxExist(boxToBuy, out link)) throw new ArgumentException("Box doesn't exist.");

            if (boxToBuy.Quantity < quantity) throw new ArgumentException("Not enough boxes.");

            if (boxToBuy.Quantity == quantity)
            {
                boxTree.RemoveBox(boxToBuy); // Remove from tree

                boxesByDate.RemoveNode(link.BoxNode); // Remove from linkedlist.

                dataBase.RemoveBoxById(boxToBuy.Id); // Remove from database

                return;
            }

            boxToBuy.Quantity -= quantity;
            boxToBuy.LastTimeBought = DateTimeOffset.Now;

            dataBase.Save(); // Save Database

            // Remove and add to the end of the LinkedList.
            boxesByDate.RemoveNode(link.BoxNode);
            boxesByDate.AddLast(link.BoxNode);
        }
        public Queue<Box> SearchBoxes(double x, double y, int amount, double maxSizeMulti, int maxDifBoxs)
        {
            var qu = new Queue<Box>(maxDifBoxs);
            SearchBoxesX(qu, boxTree.Root, new SearchData() { X = x, Y = y, Amount = amount, MaxSizeMulti = maxSizeMulti });
            return qu;
        }
        public void RemoveOld()
        {

            LinkedList<Box> removed = new LinkedList<Box>();
            while (boxesByDate.Count > 0 && (DateTimeOffset.Now - boxesByDate.StartNode.Value.LastTimeBought).TotalDays >= DAYS_TO_DELETE)
            {
                var removeBox = boxTree.RemoveBox(boxesByDate.StartNode.Value);
                removed.AddLast(boxesByDate.RemoveFirst());

                // TODO Remove from database
                dataBase.RemoveBoxById(removeBox.BoxNode.Value.Id); // Remove from database
            }

            if (removed.Count > 0) BoxCleanUp?.Invoke(this, removed);

        }
        

        LinkedList<Box> boxesByDate = new LinkedList<Box>();
        XTree boxTree = new XTree();
        IDataServices dataBase = new DataServices();

        private void SearchBoxesX(Queue<Box> boxCart, BinarySearchTreeNode<YTree> tree, SearchData searchData)
        {
            if (tree == null) return;
            if (searchData.Amount <= 0) return;
            if (boxCart.IsFull) return;

            var comp = tree.Value.CompareTo(searchData.X);

            if (comp == 0) SearchBoxesY(boxCart, tree.Value.Root, searchData);

            if (comp > 0)
            {
                SearchBoxesX(boxCart, tree.Left, searchData);
                if (tree.Value.X > searchData.MaxX) return;
                SearchBoxesY(boxCart, tree.Value.Root, searchData);
            }

            SearchBoxesX(boxCart, tree.Right, searchData);
        }
        private void SearchBoxesY(Queue<Box> boxCart, BinarySearchTreeNode<LinkedTreeBox> tree, SearchData searchData)
        {
            if (CheckOut(boxCart, tree, searchData)) return;

            var comp = tree.Value.CompareTo(searchData.Y);

            if (comp == 0) AddToQueue(boxCart, tree, searchData);

            if (comp > 0)
            {
                SearchBoxesY(boxCart, tree.Left, searchData);
                if (tree.Value.Y > searchData.MaxY) return;
                if (CheckOut(boxCart, tree, searchData)) return;
                AddToQueue(boxCart, tree, searchData);
            }

            if (searchData.Amount <= 0) return;

            SearchBoxesY(boxCart, tree.Right, searchData);
        }
        private bool CheckOut(Queue<Box> boxCart, BinarySearchTreeNode<LinkedTreeBox> tree, SearchData searchData)
        {
            if (tree == null) return true;
            if (searchData.Amount <= 0) return true;
            if (boxCart.IsFull) return true;

            return false;
        }
        private void AddToQueue(Queue<Box> boxCart, BinarySearchTreeNode<LinkedTreeBox> tree, SearchData searchData)
        {
            int quant = tree.Value.BoxNode.Value.Quantity;
            searchData.Amount -= Math.Min(quant, searchData.Amount);
            boxCart.Enqueue(tree.Value.BoxNode.Value);
        }

        class SearchData
        {
            public double X { get; set; }
            public double Y { get; set; }
            public int Amount { get; set; }
            public double MaxSizeMulti { get; set; }

            public double MaxX => X * MaxSizeMulti;
            public double MaxY => Y * MaxSizeMulti;
        }

    }
}
