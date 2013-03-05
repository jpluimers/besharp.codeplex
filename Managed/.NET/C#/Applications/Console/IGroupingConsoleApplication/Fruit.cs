namespace IGroupingConsoleApplication
{
    public class Fruit
    {
        public string Type { get; private set; }
        public string Variety { get; private set; }
        public int  Quantity { get; private set; }
        public decimal PricePerKilo { get; private set; }

        public FruitVariety GetFruitVariety()
        {
            return new FruitVariety(Type, Variety);
        }

        public FruitVariety GetDistinctFruitVariety(string type, string variety)
        {
            return FruitVariety.GetDistinctFruitVariety(type, variety);
        }

        public Fruit(string type, string variety, int quantity, decimal pricePerKilo)
        {
            Type = type;
            Variety = variety;
            Quantity = quantity;
            PricePerKilo = pricePerKilo;
        }
    }
}
