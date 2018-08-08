using System;

namespace ShopLibrary
{
    [Serializable]
    public abstract class NonFoodProduct : Product
    {
        public int Guarantee { get; private set; }

        protected internal NonFoodProduct(int id, string name, string company, decimal price, int count, int guarantee) :
            base(id, name, company, price, count)
        {
            Guarantee = guarantee;
        }

        public override int CompareTo(object product)
        {
            if (!(product is NonFoodProduct p)) { return -1; }

            if (Name.CompareTo(p.Name) == 0)
            {
                if (Company.CompareTo(p.Company) == 0)
                {
                    if (Price.CompareTo(p.Price) == 0)
                    {
                        if (Guarantee.CompareTo(p.Guarantee) == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return Guarantee.CompareTo(p.Guarantee);
                        }
                    }
                    else
                    {
                        return Price.CompareTo(p.Price);
                    }
                }
                else
                {
                    return Company.CompareTo(p.Company);
                }
            }
            else
            {
                return Name.CompareTo(p.Name);
            }
        }

        public override void Info()
        {
            Console.WriteLine("Товар:");
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Наименование: \t{Name}");
            Console.WriteLine($"Производитель: \t{Company}");
            Console.WriteLine($"Цена: \t\t{Price.ToString("C")}");
            Console.WriteLine($"Годен до: \t{Guarantee.ToString()}");
        }
    }
}
