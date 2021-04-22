using System;

namespace BoxStoreModels
{
    public class Box
    {
        public int Id { get; set; }
        public double X { get; set; } = 1;
        public double Y { get; set; } = 1;
        public int Quantity { get; set; } = 1;
        public DateTimeOffset LastTimeBought { get; set; }
    }
}
