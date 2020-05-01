namespace JBWooliesXTest.Core.Model.TrolleyTotal
{
    public class RequestedItem
    {
        public string Name { get; set; }

        public double Quantity { get; set; }

        public double QuantityFilled { get; set; }

        public static RequestedItem Copy(RequestedItem other)
        {
            return new RequestedItem()
            {
                Name = other.Name,
                QuantityFilled = other.QuantityFilled,
                Quantity = other.Quantity
            };
        }

        public bool Incomplete => Quantity > QuantityFilled;
    }
}
