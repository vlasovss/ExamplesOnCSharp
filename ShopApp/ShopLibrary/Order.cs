using System;
using System.Collections;
using System.Collections.Generic;

namespace ShopLibrary
{
    [Serializable]
    public class ItemOrder
    {
        public int IdStore { get; private set; }
        public int IdProduct { get; private set; }
        public int Count { get; set; }

        public ItemOrder(int idStore, int idProduct, int count)
        {
            IdStore = idStore;
            IdProduct = idProduct;
            Count = count;
        }
    }

    [Serializable]
    public class Order
    {
        public int IdOrder { get; private set; }
        public string FIOClient { get; private set; }
        private List<ItemOrder> items;
        public ItemOrder this[int index] { get => items[index]; }
        public IEnumerator GetEnumerator() => items.GetEnumerator();
        public int Count { get => items.Count; }

        public event ShopStateHandler Added;
        public event ShopStateHandler Removed;
        public event ShopStateHandler RemovedOrder;
        public event ShopStateHandler Performed;

        private ItemOrder Find(int id, out int index)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IdProduct == id)
                {
                    index = i;
                    return items[i];
                }
            }
            index = -1;
            return null;
        }

        private ItemOrder Find(int id)
        {
            foreach (ItemOrder item in items)
            {
                if (item.IdProduct == id) { return item; }
            }
            return null;
        }

        private void CallEvent(ShopEventArgs e, ShopStateHandler handler)
        {
            if ((handler != null) && (e != null)) { handler(this, e); }
        }

        private void OnAdded(ShopEventArgs e) => CallEvent(e, Added);
        private void OnRemoved(ShopEventArgs e) => CallEvent(e, Removed);
        private void OnRemovedOrder(ShopEventArgs e) => CallEvent(e, RemovedOrder);
        private void OnPerformed(ShopEventArgs e) => CallEvent(e, Performed);

        public Order(int id, string fIOClient, 
            ShopStateHandler addHandler,
            ShopStateHandler removeHandler,
            ShopStateHandler removeOrderHandler,
            ShopStateHandler performHandler)
        {
            IdOrder = id;
            FIOClient = fIOClient;
            Added += addHandler;
            Removed += removeHandler;
            RemovedOrder += removeOrderHandler;
            Performed += performHandler;
        }

        public void Add(int idStore, int idProduct, int count)
        {
            if (items == null) { items = new List<ItemOrder>(); }

            ItemOrder item = Find(idProduct);

            if (item == null)
            {
                item = new ItemOrder(idStore, idProduct, count);
                items.Add(item);
            }
            else
            {
                item.Count += count;
            }
            OnAdded(new ShopEventArgs("Товар добавлен в заказ.", count));
        }

        public void Remove(int idProduct)
        {
            ItemOrder item = Find(idProduct, out int index);
            if (item == null) { throw new Exception("Товар не найден в заказе."); }
            items.RemoveAt(index);
            OnRemoved(new ShopEventArgs("Товар удален из заказа.", 0));
        }

        public void RemoveOrder()
        {
            OnRemovedOrder(new ShopEventArgs($"Заказ № {IdOrder} удален.", 0));
        }

        public void Perform()
        {
            OnPerformed(new ShopEventArgs($"Заказ № {IdOrder} выполнен.", 0));
        }
    }
}
