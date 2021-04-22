using System;

namespace BoxStoreModels
{
    public class Box
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset LastTimeBought { get; set; }
    }
}
