using System;
using System.Collections.Generic;
using ShopLibrary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Console;

namespace ShopApp
{
    class Program
    {
        private static Store<Product> catalogShop;
        private static Dictionary<int, object> stores;
        private static Dictionary<int, Order> orders;

        private static void AddProductHandler(object sender, ShopEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void RemoveProductHandler(object sender, ShopEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void ReceiptProductHandler(object sender, ShopEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void ShipmentProductHandler(object sender, ShopEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void PerformOrderHandler(object sender, ShopEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void RemoveOrderHandler(object sender, ShopEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void AddInOrderHandler(object sender, ShopEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static void RemoveFromOrderHandler(object sender, ShopEventArgs e)
        {
            WriteLine(e.Message);
        }

        private static int GetIdStore()
        {
            Write("Введите Id склада: ");
            return Convert.ToInt32(ReadLine());
        }

        private static int GetIdProduct()
        {
            Write("Введите Id товара: ");
            return Convert.ToInt32(ReadLine());
        }

        private static int GetIdOrder()
        {
            Write("Введите Id заказа: ");
            return Convert.ToInt32(ReadLine());
        }

        private static string GetFildName(int field)
        {
            switch (field)
            {
                case 1: Write("Введите наименование товара: "); break;
                case 2: Write("Введите производителя товара: "); break;
                default: throw new Exception("Неверно задано поле для поиска.");
            }
            return ReadLine();
        }

        private static int GetCountProduct(string oper)
        {
            switch (oper)
            {
                case "add": Write("Количество добовляемого товара: "); break;
                case "ship": Write("Количество товара для отгрузки: "); break;
            }
            return Convert.ToInt32(ReadLine());
        }

        private static (string, string, decimal, int) GetNameCompanyPriceCount()
        {
            Write("Наименование: ");
            string name = ReadLine();
            Write("Производитель: ");
            string company = ReadLine();
            Write("Цена: ");
            decimal price = Convert.ToDecimal(ReadLine());
            Write("Количество: ");
            int count = Convert.ToInt32(ReadLine());

            return (name, company, price, count);
        }

        private static void ExistsStore(out int idStore)
        {
            if (stores == null) { throw new Exception("Ни одного склада не существует."); }
            idStore = GetIdStore();
            if (!stores.ContainsKey(idStore)) { throw new Exception("Склад с данным номером не существует."); }
        }

        private static void ExistsOrder(out int idOrder)
        {
            if (orders == null) { throw new Exception("Ни одного заказа не существует."); }
            idOrder = GetIdOrder();
            if (!orders.ContainsKey(idOrder)) { throw new Exception("Заказ с данным номером не существует."); }
        }

        private static void NewStore()
        {
            if (stores == null) { stores = new Dictionary<int, object>(); }

            int idStore = GetIdStore();
            if (stores.ContainsKey(idStore)) { throw new Exception("Склад с данным номером уже существует."); }

            WriteLine("Выберите тип скалада: \n1 - Продовольственный. 2 - Не продовольственный.");
            int typeStore = Convert.ToInt32(ReadLine());
            Write("Введите название склада: ");
            string storeName = ReadLine();
            object store;

            if (typeStore == 1) { store = new Store<FoodProduct>(storeName); }
            else { store = new Store<NonFoodProduct>(storeName); }
            stores.Add(idStore, store);
            WriteLine("Склад создан.");
        }

        private static void RemoveStore(int idStore) => stores.Remove(idStore);
        
        private static void Add(int idStore)
        {
            Product product;

            WriteLine($"Производиться добавление товара на склад:");
            if (stores[idStore] is Store<FoodProduct>)
            {
                Store<FoodProduct> store = (Store<FoodProduct>)stores[idStore];
                WriteLine($"Id: {idStore}. Название: {store.Name}");
                WriteLine("Выберите тип продукта: \n1 - Молочные. 2 - Хлебобулочные. 3 - Мясные.");
                int typeProduct = Convert.ToInt32(ReadLine());

                WriteLine("Добавление нового товара на склад.");
                var (name, company, price, count) = GetNameCompanyPriceCount();
                Write("Годен до: ");
                DateTime fitTo = Convert.ToDateTime(ReadLine());
                int id = store.GetIdProduct;

                switch (typeProduct)
                {
                    case 1:
                        {
                            product = new Milk(id, name, company, price, count, fitTo);
                            break;
                        }
                    case 2:
                        {
                            product = new Bread(id, name, company, price, count, fitTo);
                            break;
                        }
                    case 3:
                        {
                            product = new Meat(id, name, company, price, count, fitTo);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Неверно указан тип товара.");
                        }
                }

                store.Add((FoodProduct)product,
                    ReceiptProductHandler,
                    ShipmentProductHandler,
                    AddProductHandler,
                    RemoveProductHandler);
            }
            else
            {
                Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[idStore];
                WriteLine($"Id: {idStore}. Название: {store.Name}");
                WriteLine("Выберите тип продукта: \n1 - Одежда. 2 - Шляпы. 3 - Обувь.");
                int typeProduct = Convert.ToInt32(ReadLine());

                WriteLine("Добавление нового товара на склад.");
                var (name, company, price, count) = GetNameCompanyPriceCount();
                Write("Гарантия: ");
                int guarantee = Convert.ToInt32(ReadLine());
                int id = store.GetIdProduct;

                switch (typeProduct)
                {
                    case 1:
                        {
                            product = new Clothes(id, name, company, price, count, guarantee);
                            break;
                        }
                    case 2:
                        {
                            product = new Hats(id, name, company, price, count, guarantee);
                            break;
                        }
                    case 3:
                        {
                            product = new Shoes(id, name, company, price, count, guarantee);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Неверно указан тип товара.");
                        }
                }

                store.Add((NonFoodProduct)product,
                    ReceiptProductHandler,
                    ShipmentProductHandler,
                    AddProductHandler,
                    RemoveProductHandler);
            }
        }

        private static void RemoveProduct(int idStore)
        {
            int idProduct = GetIdProduct();
            if (stores[idStore] is Store<FoodProduct>)
            {
                Store<FoodProduct> store = (Store<FoodProduct>)stores[idStore];
                store.Remove(idProduct);
            }
            else
            {
                Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[idStore];
                store.Remove(idProduct);
            }
        }

        private static void RemoveOverdue(int idStore)
        {
            if (stores[idStore] is Store<FoodProduct> store)
            {
                for (int i = 0; i < store.Count; i++)
                {
                    if (store[i].FitTo < DateTime.Now) { store.Remove(store[i].Id); }
                }
            }
            else
            {
                throw new Exception("На не продовольственном складе не бывает просрочки.");
            }
        }

        private static void Receipt(int idStore)
        {
            int idProduct = GetIdProduct();
            if (stores[idStore] is Store<FoodProduct>)
            {
                Store<FoodProduct> store = (Store<FoodProduct>)stores[idStore];
                int count = GetCountProduct("add");
                store.Receipt(idProduct, count);
            }
            else
            {
                Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[idStore];
                int count = GetCountProduct("add");
                store.Receipt(idProduct, count);
            }
        }

        private static void Shipment(int idStore)
        {
            int idProduct = GetIdProduct();
            if (stores[idStore] is Store<FoodProduct>)
            {
                Store<FoodProduct> store = (Store<FoodProduct>)stores[idStore];
                int count = GetCountProduct("ship");
                store.Shipment(idProduct, count);
            }
            else
            {
                Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[idStore];
                int count = GetCountProduct("ship");
                store.Shipment(idProduct, count);
            }
        }

        private static void Shipment(int idStore, int idProduct, int count)
        {
            if (stores[idStore] is Store<FoodProduct>)
            {
                Store<FoodProduct> store = (Store<FoodProduct>)stores[idStore];
                store.Shipment(idProduct, count);
            }
            else
            {
                Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[idStore];
                store.Shipment(idProduct, count);
            }
        }

        private static void Info(int idStore)
        {
            int idProduct = GetIdProduct();
            if (stores[idStore] is Store<FoodProduct>)
            {
                Store<FoodProduct> store = (Store<FoodProduct>)stores[idStore];
                store.Find(idProduct).Info();
            }
            else
            {
                Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[idStore];
                store.Find(idProduct).Info();
            }
        }

        private static void Content()
        {
            if (stores == null) { throw new Exception("Ни одного склада не существует."); }

            foreach (KeyValuePair<int, object> store in stores)
            {
                if (store.Value is Store<FoodProduct>)
                {
                    Store<FoodProduct> tempStore = (Store<FoodProduct>)store.Value;
                    WriteLine($"\nСклад № {store.Key}");
                    WriteLine($"Наименование: {tempStore.Name}");
                    WriteLine("-------------------------------------------------------------------------------");
                    WriteLine("Id".PadRight(4) + "Наименование".PadRight(23) + "Производитель".PadRight(22) + "Кол-во".PadRight(7) + "Цена".PadRight(12) + "Годен до".PadRight(10));
                    WriteLine("-------------------------------------------------------------------------------");
                    if (tempStore.Count == 0)
                        {
                            WriteLine("Склад пуст.");
                            continue;
                        }
                    foreach (FoodProduct product in tempStore)
                    {         
                        WriteLine(product.Id.ToString().PadRight(4) + product.Name.PadRight(23) + product.Company.PadRight(22) + product.Count.ToString().PadRight(7) + product.Price.ToString("C").PadRight(12) + product.FitTo.ToString("d").PadRight(10));
                        WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                {
                    Store<NonFoodProduct> tempStore = (Store<NonFoodProduct>)store.Value;
                    WriteLine($"\nСклад № {store.Key}");
                    WriteLine($"Наименование: {tempStore.Name}");
                    WriteLine("-------------------------------------------------------------------------------");
                    WriteLine("Id".PadRight(4) + "Наименование".PadRight(23) + "Производитель".PadRight(22) + "Кол-во".PadRight(7) + "Цена".PadRight(12) + "Гарантия".PadRight(10));
                    WriteLine("-------------------------------------------------------------------------------");
                    if (tempStore.Count == 0)
                    {
                        WriteLine("Склад пуст.");
                        continue;
                    }
                    foreach (NonFoodProduct product in tempStore)
                    {
                        WriteLine(product.Id.ToString().PadRight(4) + product.Name.PadRight(23) + product.Company.PadRight(22) + product.Count.ToString().PadRight(7) + product.Price.ToString("C").PadRight(12) + product.Guarantee.ToString().PadRight(10));
                        WriteLine("-------------------------------------------------------------------------------");
                    }
                }
            }
        }

        private static void Find(int idStore)
        {
            WriteLine("Осуществить поиск по: \n1 - Наименованию. 2 - Производителю.");
            int field = Convert.ToInt32(ReadLine());

            bool found = false;

            string value = GetFildName(field);
            WriteLine("Найдено:");

            if (stores[idStore] is Store<FoodProduct>)
            {
                Store<FoodProduct> store = (Store<FoodProduct>)stores[idStore];

                foreach (FoodProduct product in store)
                {
                    if (field == 1)
                    {
                        if (product.Name == value)
                        {
                            product.Info();
                            found = true;
                        }
                    }
                    else
                    {
                        if (product.Company == value)
                        {
                            product.Info();
                            found = true;
                        }
                    }
                } 
            }
            else
            {
                Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[idStore];

                foreach (NonFoodProduct product in store)
                {
                    if (field == 1)
                    {
                        if (product.Name == value)
                        {
                            product.Info();
                            found = true;
                        }
                    }
                    else
                    {
                        if (product.Company == value)
                        {
                            product.Info();
                            found = true;
                        }
                    }
                }
            }

            if (!found) { WriteLine("Товар не найден."); }
        }

        private static void NewOrder()
        {
            int idOrder;
            int max = 0;
            if (orders == null) { orders = new Dictionary<int, Order>(); }

            Write("Введите Ф.И.О. заказчика: ");
            
            string FIOClient = ReadLine();
            if (orders.Count == 0) { idOrder = 1; }
            else 
            {
                foreach (KeyValuePair<int, Order> o in orders)
                {
                    if (o.Value.IdOrder > max) { max = o.Value.IdOrder; }
                }
                idOrder = max + 1; 
            }
             
            Order order = new Order(idOrder, FIOClient, AddInOrderHandler, RemoveFromOrderHandler, RemoveOrderHandler, PerformOrderHandler);
            
            orders.Add(idOrder, order);
            try
            {
                ExistsStore(out int idStore);
                AddInOrder(idOrder, idStore);
            }
            catch (Exception exception)
            {
                orders.Remove(idOrder);
                ConsoleColor color = ForegroundColor;
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Ошибка: " + exception.Message);
                WriteLine("Заказ не создан.");
                ForegroundColor = color;
            }
            WriteLine("Заказ создан.");
        }

        private static void AddInOrder(int idOrder, int idStore)
        {
            Product product;
            Order order = orders[idOrder];
            int idProduct = GetIdProduct();
            int count = GetCountProduct("add");

            if (stores[idStore] is Store<FoodProduct>)
            {
                Store<FoodProduct> store = (Store<FoodProduct>)stores[idStore];
                product = store.Find(idProduct);
                if (product == null) { throw new Exception($"Продукта с Id: {idProduct} на складах не существует."); } 
            }
            else
            {
                Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[idStore];
                product = store.Find(idProduct);
                if (product == null) { throw new Exception($"Продукта с Id: {idProduct} на складах не существует."); }
            }
            order.Add(idStore, idProduct, count);
        }

        private static void RemoveFromOrder(int idOrder)
        {
            Order order = orders[idOrder];
            int idProduct = GetIdProduct();
            order.Remove(idProduct);
            if (order.Count == 0)
            {
                order.RemoveOrder();
                orders.Remove(idOrder);
            }
        }

        private static void ListOrder()
        {
            Product product;
            decimal curentSum = 0;
            decimal sum = 0;
            if ((orders == null) || (orders.Count == 0)) { throw new Exception("Ни одного заказа не существует."); }

            foreach (KeyValuePair<int, Order> order in orders)
            {
                WriteLine($"\nЗаказ № {order.Key}");
                WriteLine($"Клиент: {order.Value.FIOClient}");
                WriteLine("-------------------------------------------------------------------------------");
                WriteLine("Id".PadRight(4) + "Наименование".PadRight(23) + "Производитель".PadRight(22) + "Кол-во".PadRight(7) + "Цена".PadRight(12) + "Сумма");
                WriteLine("-------------------------------------------------------------------------------");

                foreach (ItemOrder item in order.Value)
                {
                    if (stores[item.IdStore] is Store<FoodProduct>)
                    {
                        Store<FoodProduct> store = (Store<FoodProduct>)stores[item.IdStore];
                        product = store.Find(item.IdProduct);
                    }
                    else
                    {
                        Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[item.IdStore];
                        product = store.Find(item.IdProduct);
                    }
                    sum = item.Count * product.Price;
                    WriteLine(product.Id.ToString().PadRight(4) + product.Name.PadRight(23) + product.Company.PadRight(22) + item.Count.ToString().PadRight(7) + product.Price.ToString("C").PadRight(12) + sum.ToString("C"));
                    WriteLine("-------------------------------------------------------------------------------");
                    curentSum += sum;
                }
                WriteLine($"Итого: {curentSum}");
                curentSum = 0;
            }
        }

        private static void CanPerformOrder(int idOrder)
        {
            Product product;
            Order order = orders[idOrder];
            foreach (ItemOrder item in order)
            {
                if (stores[item.IdStore] is Store<FoodProduct>)
                {
                    Store<FoodProduct> store = (Store<FoodProduct>)stores[item.IdStore];
                    product = store.Find(item.IdProduct);
                    if (product.Count < item.Count)
                    {
                        throw new Exception($"Не возможно выполнить заказ № {item.IdStore}. \nТовар: Id {item.IdProduct}, не достаточно на складе № {item.IdStore}."); }
                    }
                else
                {
                    Store<NonFoodProduct> store = (Store<NonFoodProduct>)stores[item.IdStore];
                    product = store.Find(item.IdProduct);
                    if (product.Count < item.Count)
                    {
                        throw new Exception($"Не возможно выполнить заказ № {item.IdStore}. \nТовар: Id {item.IdProduct}, не достаточно на складе № {item.IdStore}.");
                    }
                }
            }
        }

        private static void PerformOrder(int idOrder)
        {
            Order order = orders[idOrder];
            foreach (ItemOrder item in order)
            {
                Shipment(item.IdStore, item.IdProduct, item.Count);
            }
            order.Perform();
            order.RemoveOrder();
            orders.Remove(idOrder);
        }

        private static void SynchronizationCatalogShop() 
        {
            if (stores == null) { throw new Exception("Ни одного склада не существует."); }
            catalogShop = new Store<Product>("Каталог магазина");

            foreach (KeyValuePair<int, object> store in stores)
            {
                if (store.Value is Store<FoodProduct>)
                {
                    Store<FoodProduct> tempStore = (Store<FoodProduct>)store.Value;
                    foreach (FoodProduct product in tempStore)
                    {
                        catalogShop.AddCatalog(product);
                    }
                }
                else
                {
                    Store<NonFoodProduct> tempStore = (Store<NonFoodProduct>)store.Value;
                    foreach (NonFoodProduct product in tempStore)
                    {
                        catalogShop.AddCatalog(product);
                    }
                }
            }
        }

        private static void ViewCatalogShop()
        {
            WriteLine($"\t\t\t\tКаталог магазина");
            WriteLine("-------------------------------------------------------------------------------");
            WriteLine("Id".PadRight(4) + "Наименование".PadRight(30) + "Производитель".PadRight(23) + 
                "Кол-во".PadRight(8) + "Цена".PadRight(13));
            WriteLine("-------------------------------------------------------------------------------");
            Product p;

            foreach (Product product in catalogShop)
            {
                if (product is FoodProduct) { p = (FoodProduct)product; }
                else { p = (NonFoodProduct)product; }
                WriteLine(p.Id.ToString().PadRight(4) + p.Name.PadRight(30) + p.Company.PadRight(23) +
                    p.Count.ToString().PadRight(8) + p.Price.ToString("C").PadRight(13));
                WriteLine("-------------------------------------------------------------------------------");
            }
        }

        static void Main(string[] args)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists("stores.dat"))
            {
                using (FileStream file = new FileStream("stores.dat", FileMode.Open))
                {
                    stores = (Dictionary<int, object>)formatter.Deserialize(file);
                }
            }

            if (File.Exists("orders.dat"))
            {
                using (FileStream file = new FileStream("orders.dat", FileMode.Open))
                {
                    orders = (Dictionary<int, Order>)formatter.Deserialize(file);
                }
            }

            bool alive = true;
            while (alive)
            {
                Clear();
                ConsoleColor color = ForegroundColor;
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("\n\t\t\t\t0 - КАТАЛОГ МАГАЗИНА");
                WriteLine("\tОперации по складу:");
                WriteLine("1 - Новый склад. \t2 - Удалить склад. \t3 - Содержимое складов.");
                WriteLine("4 - Поступление. \t5 - Отгрузка. \t\t6 - Удалить просрочку.");
                WriteLine("\n\tОперации с товарами:");
                WriteLine("7 - Новый товар. \t8 - Удалить товар. \t9 - Информация о товаре. ");
                WriteLine("\t\t\t10 - Поиск.");
                WriteLine("\n\tОперации с заказами:");
                WriteLine("11 - Новый заказ. \t12 - Добавить в заказ. \t13 - Удалить из заказа.");
                WriteLine("14 - Список заказов. \t15 - Выполнить заказ. \t16 - Выйти из программы.");
                ForegroundColor = color;
                Write("\nВведите пункт меню: ");
                try
                {
                    int itemMenu = Convert.ToInt32(ReadLine());

                    switch (itemMenu)
                    {
                        case 0:
                            {
                                SynchronizationCatalogShop();
                                ViewCatalogShop();
                                break;
                            }
                        case 1:
                            {
                                NewStore();
                                break;
                            }
                        case 2:
                            {
                                ExistsStore(out int idStore);
                                RemoveStore(idStore);
                                break;
                            }
                        case 3:
                            {
                                Content();
                                break;
                            }
                        case 4:
                            {
                                ExistsStore(out int idStore);
                                Receipt(idStore);
                                break;
                            }
                        case 5:
                            {
                                ExistsStore(out int idStore);
                                Shipment(idStore);
                                break;
                            }
                        case 6:
                            {
                                ExistsStore(out int idStore);
                                RemoveOverdue(idStore);
                                break;
                            }
                        case 7:
                            {
                                ExistsStore(out int idStore);
                                Add(idStore);
                                break;
                            }
                        case 8:
                            {
                                ExistsStore(out int idStore);
                                RemoveProduct(idStore);
                                break;
                            }
                        case 9:
                            {
                                ExistsStore(out int idStore);
                                Info(idStore);
                                break;
                            }
                        case 10:
                            {
                                ExistsStore(out int idStore);
                                Find(idStore);
                                break;
                            }
                        case 11:
                            {
                                NewOrder();
                                break;
                            }
                        case 12:
                            {
                                ExistsOrder(out int idOrder);
                                ExistsStore(out int idStore);
                                AddInOrder(idOrder, idStore);
                                break;
                            }
                        case 13:
                            {
                                ExistsOrder(out int idOrder);
                                RemoveFromOrder(idOrder);
                                break;
                            }
                        case 14:
                            {
                                ListOrder();
                                break;
                            }
                        case 15:
                            {
                                ExistsOrder(out int idOrder);
                                CanPerformOrder(idOrder);
                                PerformOrder(idOrder);
                                break;
                            }
                        case 16:
                            {
                                alive = false;
                                formatter = new BinaryFormatter();
                                if (stores != null)
                                {
                                    using (FileStream file = new FileStream("stores.dat", FileMode.Create))
                                    {
                                        formatter.Serialize(file, stores);
                                    }
                                }

                                if (orders != null)
                                {
                                    using (FileStream file = new FileStream("orders.dat", FileMode.Create))
                                    {
                                        formatter.Serialize(file, orders);
                                    }
                                }

                                continue;
                            }
                    }
                    WriteLine("Для продолжения нажмите любую клавишу.");
                    ReadKey();
                }
                catch (Exception exception)
                {
                    color = ForegroundColor;
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Ошибка: " + exception.Message);
                    ForegroundColor = color;
                    WriteLine("Нажмите любую клавишу.");
                    ReadKey();
                }
            }
        }
    }
}
