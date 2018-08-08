using System;

namespace ShopLibrary
{
    [Serializable]
    public class Hats : NonFoodProduct
    {
        const string UNIT = "шт.";

        public Hats(int id, string name, string company, decimal price, int count, int guarantee) :
            base(id, name, company, price, count, guarantee)
        {
        }

        public override void Info()
        {
            base.Info();
            Console.WriteLine($"В наличии: \t{Count.ToString()} {UNIT}");
        }
    }
}
