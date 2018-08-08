using System;

namespace ShopLibrary
{
    [Serializable]
    public delegate void ShopStateHandler(object sender, ShopEventArgs e);

    public class ShopEventArgs
    {
        public string Message { get; private set; }
        public int Count { get; private set; }

        public ShopEventArgs(string message, int count)
        {
            Message = message;
            Count = count;
        }
    }
}
