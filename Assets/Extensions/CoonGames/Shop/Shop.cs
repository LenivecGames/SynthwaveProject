using System.Collections.Generic;
using UnityEngine;
using LiteDB;

namespace CoonGames.Shop
{
    public class Shop : System.IDisposable
    {
        private class Item
        {
            [BsonId]
            public string Name { get; private set; }
            public BsonDocument Commodity { get; private set; }

            public float Price { get; set; }
            public uint Quantity { get; set; }

            public Item() { }

            public Item(string name, float price, uint quantity, ISerializable commodity)
            {
                Name = name;
                Price = price;
                Quantity = quantity;
                Commodity = commodity.Serialize();
            }
        }

        private LiteDatabase _DB;
        private LiteCollection<Item> _Items;
        private IWallet _Wallet;

        public Shop(string dbName, IWallet wallet = null)
        {
            if (string.IsNullOrWhiteSpace(dbName))
                throw new System.ArgumentException("Incorrect DB name.", nameof(dbName));

            _DB = new LiteDatabase(dbName + ".db");
            _Items = _DB.GetCollection<Item>("items");
            _Wallet = wallet;
        }

        public void Dispose()
        {
            ((System.IDisposable)_DB).Dispose();
        }

        public IEnumerable<string> EnlistItems()
        {
            return System.Linq.Enumerable.Select(_Items.FindAll(), s => s.Name);
        }

        public void AddItem<T>(string name, float price, uint quantity, T commodity, Texture2D image = null) where T : class, ISerializable, new()
        {
            _Items.Insert(new Item(name, price, quantity, commodity));

            if (image != null)
                SetItemImage(name, image);
        }

        public float GetItemPrice(string name)
        {
            return GetItem(name).Price;
        }

        public void SetItemPrice(string name, float price)
        {
            Item item = GetItem(name);

            item.Price = price;
            _Items.Update(item);
        }

        public float GetItemQuantity(string name)
        {
            return GetItem(name).Quantity;
        }

        public void SetItemQuantity(string name, uint quantity)
        {
            Item item = GetItem(name);

            item.Quantity = quantity;
            _Items.Update(item);
        }

        public Texture2D GetItemImage(string name)
        {
            using (LiteFileStream stream = _DB.FileStorage.OpenRead(name))
            {
                //Debug.Log("GetItemImage:");
                //Debug.Log(stream.FileInfo.Metadata.Get("width").AsInt32);
                //Debug.Log(stream.FileInfo.Metadata.Get("height").AsInt32);
                //Debug.Log((TextureFormat)stream.FileInfo.Metadata.Get("format").AsInt32);
                //Debug.Log(stream.Length);

                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length - 1);
                //Texture2D image = new Texture2D(
                //    stream.FileInfo.Metadata.Get("width").AsInt32,
                //    stream.FileInfo.Metadata.Get("height").AsInt32,
                //    (TextureFormat)stream.FileInfo.Metadata.Get("format").AsInt32,
                //    stream.FileInfo.Metadata.Get("mipmap").AsBoolean
                //    );

                //image.LoadRawTextureData(buffer);
                //image.Apply();

                Texture2D image = new Texture2D(0, 0);
                bool fuck = image.LoadImage(buffer);
                //Debug.Log(fuck);

                //Debug.Log(image.width);
                //Debug.Log(image.height);
                //Debug.Log(image.format);
                //Debug.Log("===========");

                return image;
            }
        }

        public void SetItemImage(string name, Texture2D image)
        {
            //Debug.Log("SetItemImage");
            //Debug.Log(image.width);
            //Debug.Log(image.height);
            //Debug.Log(image.format);
            //Debug.Log(image./*GetRawTextureData()*/EncodeToJPG().Length);
            //Debug.Log("===========");

            if (image == null)
                throw new System.ArgumentNullException(nameof(image));

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(image./*GetRawTextureData()*/EncodeToJPG()))
            {
                _DB.FileStorage.Upload(name, null, stream);
                //_DB.FileStorage.SetMetadata(name, new BsonDocument(new Dictionary<string, BsonValue>
                //{
                //    { "width", image.width },
                //    { "height", image.height },
                //    { "format",(int)image.format },//TODO
                //    { "mipmap",image.mipmapCount>1}
                //}));
            }
        }

        public void DeleteItem(string name)
        {
            _Items.Delete(name);
            _DB.FileStorage.Delete(name);
        }

        public T BuyItem<T>(string name) where T : class, ISerializable, new()
        {
            Item item = GetItem(name);

            if (item.Quantity == 0 || _Wallet?.Money < item.Price)
                return null;

            T commodity = new T();
            commodity.Deserialize(item.Commodity);

            item.Quantity--;
            _Items.Update(item);

            if (_Wallet != null)
                _Wallet.Withdraw(item.Price);

            return commodity;
        }

        private Item GetItem(string name)
        {
            Item item = _Items.FindById(name);
            if (item == null)
                throw new System.ArgumentException("Item not found.", nameof(name));

            return item;
        }
    }
}
