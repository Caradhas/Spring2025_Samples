using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;

namespace Maui.eCommerce.ViewModels
{
    class ProductViewModel
    {
        private Item? cachedModel { get; set; }
        public string? Name {
            get
            {
                return Model?.Product.Name ?? string.Empty;
            }
            set
            {
                if(Model != null && Model.Product.Name != value)
                {
                    Model.Product.Name = value;
                }
            }
        }

        public double? cost
        {
            get
            {
                return Model?.Product.cost;
            }
            set
            {
                if(Model != null && Model.Product.cost != value)
                {
                    Model.Product.cost = value;
                }
            }
        }

        public int? Quantity
        {
            get
            {
                return Model?.Quantity;
            }
            set
            {
                if (Model != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                }
            }
        }

        public Item? Model { get; set; }

        public void AddOrUpdate()
        {
            ProductServiceProxy.Current.AddOrUpdate(Model, cost);
        }

        public ProductViewModel()
        {
            Model = new Item();
        }
        public ProductViewModel(Item? model)
        {
            Model = model;
            if(model != null)
            {
                cachedModel = new Item(model);
            }
        }

        public void Undo()
        {
            ProductServiceProxy.Current.AddOrUpdate(cachedModel);
        }
    }
}
