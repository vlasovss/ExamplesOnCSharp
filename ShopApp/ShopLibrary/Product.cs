using System;

namespace ShopLibrary
{
    [Serializable]
    public abstract class Product : IComparable
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Company { get; private set; }
        public decimal Price { get; private set; }
        public int Count { get; set; }

        protected internal event ShopStateHandler Shipmented;
        protected internal event ShopStateHandler Receipted;
        protected internal event ShopStateHandler Added;
        protected internal event ShopStateHandler Removed;

        protected internal Product(int id, string name, string company, decimal price, int count)
        {
            Id = id;
            Name = name;
            Company = company;
            Price = price;
            Count = count;
        }

        private void CallEvent(ShopEventArgs e, ShopStateHandler handler)
        {
            if ((handler != null) && (e != null)) { handler(this, e); }
        }

        private void OnReceipt(ShopEventArgs e) => CallEvent(e, Receipted);
        private void OnShipmented(ShopEventArgs e) => CallEvent(e, Shipmented);
        private void OnAdded(ShopEventArgs e) => CallEvent(e, Added);
        private void OnRemoved(ShopEventArgs e) => CallEvent(e, Removed);

        public void Receipt(int count)
        {
            Count += count;
            OnReceipt(new ShopEventArgs($"Товар: \nId: {Id} \nНаименование: {Name}. \nНа склад поступило {count} единиц.", count));
        }

        public void Shipment(int count)
        {
            if (Count >= count)
            {
                Count -= count;
                OnShipmented(new ShopEventArgs($"Товар: \nId: {Id} \nНаименование: {Name}. \nСо склада отгружено {count} единиц. \nОсталось {Count} единиц.", count));
            }
            else
            {
                OnShipmented(new ShopEventArgs($"Товар: \nId: {Id} \nНаименование: {Name}. \nНедостаточно на складе. \nОсталось {Count} единиц.", count));
            }
        }

        public void Add()
        {
            OnAdded(new ShopEventArgs($"Товар \nId: {Id} \nНаименование: {Name}. \nНа склад добавлен новый товар в количестве {Count} единиц.", Count));
        }

        public void Remove()
        {
            OnRemoved(new ShopEventArgs($"Товар: \nId: {Id} \nНаименование: {Name}. \nУдален со склада.", Count));
        }

        public abstract void Info();

        public abstract int CompareTo(object product);
    }
}
