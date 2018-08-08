using System;

namespace ShopLibrary
{
    [Serializable]
    public class Milk : FoodProduct
    {
        const string UNIT = "л.";

        public Milk(int id, string name, string company, decimal price, int count, DateTime fitTo) :
            base(id, name, company, price, count, fitTo)
        {
        }

        public override void Info()
        {
            base.Info();
            Console.WriteLine($"В наличии: \t{Count.ToString()} {UNIT}");
        }
    }
}
