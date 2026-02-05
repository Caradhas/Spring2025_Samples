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
    public class ShoppingCartService
    {

        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<Item> items;
        public List<Item> CartItems
        {
            get
            {
                return items;
            }
        }
        public TotalPrice Total { get; set; }

        public static ShoppingCartService Current { 
            get
            {
                if(instance == null)
                {
                    instance = new ShoppingCartService();
                }
                return instance;
            }
        }

        private static ShoppingCartService? instance;
        private ShoppingCartService()
        {
            items = new List<Item>();
            Total = new TotalPrice();

        }

        public Item? AddOrUpdate(Item item)
        {

            var existingInvItem = _prodSvc.GetById(item.Id);
            //exits if theres no item/item quantity is 0, otherwise removes the item from inventory
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }
            if(existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }
            //adds item to shopping depending on whether or not it already exists
            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);
             }
            else
            {
                existingItem.Quantity++;
            }

            Total.add(existingInvItem.Product.cost);

            return existingInvItem;
        }

        public Item? ReturnItem(Item item)
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }
            var itemToReturn = CartItems.FirstOrDefault(c => c.Id==item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if(inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity++;
                }
            }
            Total.remove(itemToReturn.Product.cost);

            return itemToReturn;
        }

    }
}
