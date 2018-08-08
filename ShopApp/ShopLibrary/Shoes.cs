using System;

namespace ShopLibrary
{
    [Serializable]
    public class Shoes : NonFoodProduct
    {
        const string UNIT = "пар.";

        public Shoes(int id, string name, string company, decimal price, int count, int guarantee) :
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
