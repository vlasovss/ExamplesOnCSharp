using System;

namespace ShopLibrary
{
    [Serializable]
    public abstract class FoodProduct : Product
    {
        public DateTime FitTo { get; private set; }

        protected internal FoodProduct(int id, string name, string company, decimal price, int count, DateTime fitTo) :
            base(id, name, company, price, count)
        {
            FitTo = fitTo;
        }

        public override int CompareTo(object product)
        {
            if (!(product is FoodProduct p)) { return -1; }

            if (Name.CompareTo(p.Name) == 0)
            {
                if (Company.CompareTo(p.Company) == 0)
                {
                    if (Price.CompareTo(p.Price) == 0)
                    {
                        if (FitTo.CompareTo(p.FitTo) == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return FitTo.CompareTo(p.FitTo);
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
            Console.WriteLine($"Годен до: \t{FitTo.ToString()}");
        }
    }
}
