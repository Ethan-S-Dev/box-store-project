namespace BoxStoreModels
{
    public class SearchTicket
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Amount { get; set; }
        public int MaxDifBoxes { get; set; }
        public double MaxSizeMulti { get; set; }

        public SearchTicket()
        {
            MaxSizeMulti = 1;
            MaxDifBoxes = 10;
            Amount = 1;
        }
    }
}
