using System;
using BoxStoreModels;

namespace BoxStoreBLL
{
    public class BoxStoreApi
    {
        public event EventHandler<System.Collections.Generic.IEnumerable<Box>> BoxCleanUp { add { BoxStoreLogic.Instance.BoxCleanUp += value; } remove { BoxStoreLogic.Instance.BoxCleanUp -= value; } }
        public System.Collections.Generic.IEnumerable<Box> AllBoxes => BoxStoreLogic.Instance.AllBoxes;
        public int CountBoxes => BoxStoreLogic.Instance.CountBoxes;
        public void AddBox(Box box) => BoxStoreLogic.Instance.AddBox(box);
        public System.Collections.Generic.IEnumerable<BoxOrder> Search(SearchTicket search,out bool succes) => BoxStoreLogic.Instance.SearchBoxes(search,out succes);
        public void BuyBoxes(System.Collections.Generic.IEnumerable<BoxOrder> orders) => BoxStoreLogic.Instance.ExecuteOrders(orders);
    }
}
