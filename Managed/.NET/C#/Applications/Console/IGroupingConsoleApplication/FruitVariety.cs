using System;
using System.Collections.Generic;

namespace IGroupingConsoleApplication
{
    public class FruitVariety : IEquatable<FruitVariety>
    {
        public string Type { get; private set; }
        public string Variety { get; private set; }

        public FruitVariety(string type, string variety)
        {
            Type = type;
            Variety = variety;
        }

        public static FruitVariety GetDistinctFruitVariety(string type, string variety)
        {
            FruitVariety result = new FruitVariety(type, variety);
            int index = distinctFruitVarieties.IndexOf(result);
            if (-1 == index)
                distinctFruitVarieties.Add(result);
            else
                result = distinctFruitVarieties[index];
            return result;
        }

        static List<FruitVariety> distinctFruitVarieties = new List<FruitVariety>();

        public override bool Equals(object obj)
        {
            FruitVariety other = obj as FruitVariety;
            bool result;
            IEquatable<FruitVariety> self = this as IEquatable<FruitVariety>;
            if ((null != self) && (null != other))
                result = self.Equals(other);
            else
                result = base.Equals(obj);
            return result;
        }

        public override int GetHashCode()
        {
            int result = (this.Type.GetHashCode()) ^ (this.Variety.GetHashCode());
            return result;
        }

        bool IEquatable<FruitVariety>.Equals(FruitVariety other)
        {
            bool result = (other.Type == this.Type) && (other.Variety == this.Variety);
            return result;
        }
    }
}
