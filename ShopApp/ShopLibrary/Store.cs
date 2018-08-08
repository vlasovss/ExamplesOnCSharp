using System;
using System.Collections;
using System.Collections.Generic;

namespace ShopLibrary
{
    [Serializable]
    public class Store<T> where T : Product
    {
        private List<T> items;
        public string Name { get; private set; }

        public int Count { get => items.Count; }

        public IEnumerator GetEnumerator() => items.GetEnumerator();

        public T this[int index] { get => items[index]; } 

        public Store(string name)
        {
            Name = name;
            items = new List<T>();
        }

        public int GetIdProduct
        {
            get
            {
                if (items.Count == 0) { return 1; }
                return (items[items.Count - 1].Id + 1);
            }
        }

        private T Find(T product)
        {
            foreach (T p in items)
            {
                if (p.CompareTo(product) == 0) { return p; }
            }
            return null;
        }

        private T Find(int id, out int index)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == id)
                {
                    index = i;
                    return items[i];
                }
            }
            index = -1;
            return null;
        }

        public T Find(int id)
        {
            foreach (T p in items)
            {
                if (p.Id == id) { return p; }
            }
            return null;
        }

        public T Find(string value, string field)
        {
            if (field.Equals("Name"))
            {
                foreach (T p in items)
                {
                    if (p.Name == value) { return p; }
                }
            }

            if (field.Equals("Company"))
            {
                foreach (T p in items)
                {
                    if (p.Company == value) { return p; }
                }
            }

            return null;
        }

        public void Add(T product,
            ShopStateHandler receiptHandler,
            ShopStateHandler shipmentHandler,
            ShopStateHandler addHandler,
            ShopStateHandler removeHandler)
        {
            if (product == null) { throw new Exception("Товар не создан."); }

            if (items.Count == 0) 
            {
                items.Add(product);
            }
            else
            {
                T newProduct = Find(product);

                if (newProduct == null)
                {
                    items.Add(product);
                }
                else
                {
                    newProduct.Receipt(product.Count);
                }
            }
            product.Added += addHandler;
            product.Receipted += receiptHandler;
            product.Shipmented += shipmentHandler;
            product.Removed += removeHandler;
            
            product.Add(); 
        }

        public void AddCatalog(T product)
        {
            if (product == null) { throw new Exception("Товар не создан."); }

            items.Add(product);
        }

        public void Receipt(int id, int count)
        {
            T product = Find(id);

            if (product == null) { throw new Exception("Товар не найден"); }

            product.Receipt(count);
        }

        public void Shipment(int id, int count)
        {
            T product = Find(id);

            if (product == null) { throw new Exception("Товар не найден"); }

            product.Shipment(count);

            if (product.Count == 0) { Remove(id); }
        }

        public void Remove(int id)
        {
            T product = Find(id, out int index);

            if (product == null) { throw new Exception("Товар не найден"); }

            items.RemoveAt(index);

            product.Remove();
        }
    }
}
