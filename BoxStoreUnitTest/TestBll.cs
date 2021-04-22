using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BoxStoreBLL;
using BoxStoreModels;
using System.Linq;

namespace BoxStoreUnitTest
{
    [TestClass]
    public class TestBll
    {
        BoxStoreApi api = new BoxStoreApi();

        [TestMethod]
        public void AddRandom1000Boxes()
        {
            var start = api.CountBoxes;

            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                api.AddBox(new Box() { X = rnd.Next(2, 25), Y = rnd.Next(2, 25) });
            }

            var end = api.CountBoxes;

            Assert.IsTrue(end - start == 1000);
        }

        [TestMethod]
        public void TestSearch()
        {
            var x = 4;
            var y = 6;
            var quant = 3;
            var multi = 3.5;

            api.AddBox(new Box() { X = x, Y = y });

            var search = new SearchTicket();
            search.X = x;
            search.Y = y;
            search.MaxDifBoxes = 10;
            search.MaxSizeMulti = multi;
            search.Amount = quant;

            bool res;
            var orders = api.Search(search, out res);

            Assert.IsTrue(res);
            Assert.IsTrue(orders.Sum(a => a.Amount) > 1);
        }

        [TestMethod]
        public void TestBuy()
        {
            var x = 6;
            var y = 5;
            var quant = 6;
            var multi = 3.5;

            api.AddBox(new Box() { X = x, Y = y });

            var search = new SearchTicket();
            search.X = x;
            search.Y = y;
            search.MaxDifBoxes = 10;
            search.MaxSizeMulti = multi;
            search.Amount = quant;

            bool res;
            var orders = api.Search(search, out res);

            Assert.IsTrue(res);
            var toBuy = orders.Sum(a => a.Amount);

            var beforeBuy = api.CountBoxes;

            api.BuyBoxes(orders);

            var AfterBuy = api.CountBoxes;

            Assert.IsTrue(beforeBuy- AfterBuy== toBuy);
        }
    }
}
