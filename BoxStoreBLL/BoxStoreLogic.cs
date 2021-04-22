using System;
using System.Threading;
using System.Threading.Tasks;
using BoxStoreDataStructures;
using BoxStoreModels;

namespace BoxStoreBLL
{
    class BoxStoreLogic
    {
        public static BoxStoreLogic Instance { get { if (instance == null) instance = new BoxStoreLogic(); return instance; } }
        public EventHandler<System.Collections.Generic.IEnumerable<Box>> BoxCleanUp { get {return dataManger.BoxCleanUp; } set { dataManger.BoxCleanUp = value; } }
        public int CountBoxes => dataManger.CountBoxes;
        public System.Collections.Generic.IEnumerable<Box> AllBoxes => dataManger.GetAllBoxes();
       
        public void AddBox(Box box) => dataManger.AddBox(box);
        public System.Collections.Generic.IEnumerable<BoxOrder> SearchBoxes(SearchTicket search, out bool succes)
        {
            var cart = dataManger.SearchBoxes(search.X, search.Y, search.Amount, search.MaxSizeMulti, search.MaxDifBoxes);
            
            var list = new LinkedList<BoxOrder>();
            var boxesLeft = search.Amount;
            while (!cart.IsEmpty)
            {
                Box box;
                cart.Dequeue(out box);
                var toAdd = Math.Min(boxesLeft, box.Quantity);
                var order = new BoxOrder() { Box = box,Amount = toAdd };
                boxesLeft -= toAdd;
                list.AddLast(order);
            }

            succes = boxesLeft == 0;

            return list;
        }
        public void ExecuteOrders(System.Collections.Generic.IEnumerable<BoxOrder> orders)
        {
            foreach (var order in orders)
            {
                dataManger.BuyBoxes(order.Box, order.Amount);
            }
        }
        
        private static BoxStoreLogic instance;
        private DataManger dataManger;
        private Timer timer;

        private BoxStoreLogic()
        {
            dataManger = new DataManger();
            dataManger.Init();
            timer = new Timer(CheckOld,null,new TimeSpan(0,0,10),new TimeSpan(1,0,0));
        }   
        ~BoxStoreLogic()
        {
            timer.Dispose();
        }
        
        private void CheckOld(object obj) => dataManger.RemoveOld();

    }
}
