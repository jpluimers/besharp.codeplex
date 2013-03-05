using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGroupingConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var fruits = Grocery.BuyFrom();
            Console.WriteLine("Explicit new: LINQ Expression ====================================");
            showExplicitGrouping_LINQ_Expression(fruits);
            Console.WriteLine("Explicit new: LINQ Method ========================================");
            showExplicitGrouping_LINQ_Method(fruits);

            Console.WriteLine("Implicit: LINQ Expression ====================================");
            showImplicitGrouping_LINQ_Expression(fruits);
            Console.WriteLine("Implicit: LINQ Method ========================================");
            showImplicitGrouping_LINQ_Method(fruits);

            Console.WriteLine("Explicit distinct: LINQ Expression ====================================");
            showExplicitGrouping_LINQ_Expression_Distinct(fruits);
            Console.WriteLine("Explicit distinct: LINQ Method ========================================");
            showExplicitGrouping_LINQ_Method_Distinct(fruits);

            Console.WriteLine("Explicit factory: LINQ Expression====================================");
            showExplicitGrouping_LINQ_Expression_Factory(fruits);
            Console.WriteLine("Explicit factory: LINQ Method ========================================");
            showExplicitGrouping_LINQ_Method_Factory(fruits);
        }

        private static void showExplicitGrouping_LINQ_Expression(IEnumerable<Fruit> fruits)
        {
            IEnumerable<IGrouping<FruitVariety, Fruit>> groups =
                from fruit
                    in fruits
                group fruit by new FruitVariety(fruit.Type, fruit.Variety);

            showExplicitFruitVarietyGrouping(groups);
        }

        private static void showExplicitGrouping_LINQ_Method(IEnumerable<Fruit> fruits)
        {
            IEnumerable<IGrouping<FruitVariety, Fruit>> groups =
                fruits.GroupBy(fruit => new FruitVariety(fruit.Type, fruit.Variety));

            showExplicitFruitVarietyGrouping(groups);
        }

        private static void showExplicitFruitVarietyGrouping(IEnumerable<IGrouping<FruitVariety, Fruit>> groups)
        {
            foreach (IGrouping<FruitVariety, Fruit> group in groups)
            {
                FruitVariety key = group.Key;
                Console.WriteLine("Type: {0}, Variety: {1}", key.Type, key.Variety);
                showFruits(group);
            }
        }

        private static void showFruits(IEnumerable<Fruit> fruits)
        {
            foreach (Fruit fruit in fruits)
            {
                Console.WriteLine("  Quantity: {0}, Price per Kilo: {1}", fruit.Quantity, fruit.PricePerKilo);
            }
        }

        private static void showImplicitGrouping_LINQ_Expression(IEnumerable<Fruit> fruits)
        {
            var groups = from fruit
                             in fruits
                         group fruit by new { fruit.Type, fruit.Variety };

            foreach (var group in groups) // cannot make this a separate method, as it is using `var`
            {
                var key = group.Key;
                Console.WriteLine("Type: {0}, Variety: {1}", key.Type, key.Variety);

                showFruits(group);
            }
        }

        private static void showImplicitGrouping_LINQ_Method(IEnumerable<Fruit> fruits)
        {
            var groups = fruits.GroupBy(fruit => new { fruit.Type, fruit.Variety });

            foreach (var group in groups)
            {
                var key = group.Key;
                Console.WriteLine("Type: {0}, Variety: {1}", key.Type, key.Variety);

                showFruits(group);
            }
        }

        //private static void showImplicitFruitVarietyGrouping<TKey, TElement>(IEnumerable<IGrouping<TKey, TElement>> groups)
        //{
        //    foreach (IGrouping<TKey, TElement> group in groups)
        //    {
        //        TKey key = group.Key;
        //        Console.WriteLine("Type: {0}, Variety: {1}", key.Type, key.Variety);
        //        showFruits(group);
        //    }
        //}

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private static void showExplicitGrouping_LINQ_Expression_Distinct(IEnumerable<Fruit> fruits)
        {
            IEnumerable<IGrouping<FruitVariety, Fruit>> groups =
                from fruit
                    in fruits
                group fruit by fruit.GetDistinctFruitVariety(fruit.Type, fruit.Variety);

            showExplicitFruitVarietyGrouping(groups);
        }

        private static void showExplicitGrouping_LINQ_Method_Distinct(IEnumerable<Fruit> fruits)
        {
            IEnumerable<IGrouping<FruitVariety, Fruit>> groups =
                fruits.GroupBy(fruit => fruit.GetDistinctFruitVariety(fruit.Type, fruit.Variety));

            showExplicitFruitVarietyGrouping(groups);
        }

        private static void showExplicitGrouping_LINQ_Expression_Factory(IEnumerable<Fruit> fruits)
        {
            IEnumerable<IGrouping<FruitVariety, Fruit>> groups =
                from fruit
                    in fruits
                group fruit by fruit.GetFruitVariety();

            showExplicitFruitVarietyGrouping(groups);
        }

        private static void showExplicitGrouping_LINQ_Method_Factory(IEnumerable<Fruit> fruits)
        {
            IEnumerable<IGrouping<FruitVariety, Fruit>> groups =
                fruits.GroupBy(fruit => fruit.GetFruitVariety());

            showExplicitFruitVarietyGrouping(groups);
        }

    }
}
