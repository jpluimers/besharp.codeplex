using System.Collections.Generic;

namespace IGroupingConsoleApplication
{
    public class Grocery
    {
        public static IEnumerable<Fruit> BuyFrom()
        {
            const string Apple = "Apple";
            const string Pear = "Pear";
            const string Cherry = "Cherry";

            Fruit[] result = new Fruit[]
            {
                new Fruit(Apple, "Belle de Boskoop", 12, 2.48m),
                new Fruit(Apple, "Elstar", 7, 1.98m),
                new Fruit(Apple, "Elstar", 9, 1.88m),
                new Fruit(Apple, "Red Prince", 11, 2.98m),
                new Fruit(Apple, "Santana", 5, 2.48m),
                new Fruit(Pear, "Gieser Wildeman", 3, 3.48m),
                new Fruit(Pear, "Verdi", 6, 3.98m),
                new Fruit(Cherry, "Early Rivers", 30, 5.98m),
                new Fruit(Cherry, "Morel", 25, 6.98m),
            };
            return result;
        }
    }
}
