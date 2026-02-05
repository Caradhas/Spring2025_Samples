using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Spring2025_Samples.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {

            Total = new TotalPrice();

            Products = new List<Item?>
            {
                new Item { Product = new ProductDTO { Id = 1, Name = "Product 1", cost = 999 }, Id = 1, Quantity = 999 },
                new Item { Product = new ProductDTO { Id = 2, Name = "Product 2" }, Id = 2, Quantity = 999 },
                new Item { Product = new ProductDTO { Id = 3, Name = "Product 3", cost = 200 }, Id = 3, Quantity = 999 },

            };


        }
        private int lastKey
        {
            get
            {
                if (!Products.Any())
                {
                    return 0;
                }
                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }
                return instance;

            }
        }
        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p?.Id == id);
        }
        public List<Item?> Products { get; private set; }
        public TotalPrice Total { get; set; }

        public Item AddOrUpdate(Item item, double? price = null)
        {
            double? test = 0;
            if (item.Id == 0)
            {
                item.Id = lastKey + 1;
                item.Product.Id = item.Id;
                Products.Add(item);
                if (price.HasValue)
                {
                    item.Product.cost = price.Value;
                }
            }
            else
            {
                var existingItem = Products.FirstOrDefault(p => p.Id == item.Id);
                var index = Products.IndexOf(existingItem);
                Products.RemoveAt(index);
                Item putInItem = new Item(item);
                putInItem.Product.cost = price ?? item.Product.cost;
                item = putInItem;
                Products.Insert(index, new Item(item));
            }
            return item;
        }

        public Item? PurchaseItem(Item item)
        {
            if(item?.Id <=0 || item == null)
            {
                return null;
            }
            var itemToPurchase = GetById(item.Id);
            if (itemToPurchase != null)
            {
                itemToPurchase.Quantity--;
            }
            return itemToPurchase;
        }

        public Item? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }
            Item? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);
            return product;
        }
    }
}
