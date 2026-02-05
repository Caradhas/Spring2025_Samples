using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.DTO;

namespace Spring2025_Samples.Models
{
    public class Product
    {

        public int Id { get; set; }
        public double? cost { get; set; }
        public string? Name { get; set; }
        public string LegacyProperty1 { get; set; }
        public string LegacyProperty2 { get; set; }
        public string LegacyProperty3 { get; set; }
        public string LegacyProperty4 { get; set; }
        public string LegacyProperty5 { get; set; }
        public string LegacyProperty6 { get; set; }
        public string? Display
        {
            get
            {
                return $"{Id}. {Name} ${cost}";
            }
        }
        public Product()
        {
            Name = string.Empty;
            cost = 0.0;
        }
        public Product(Product p)
        {
            Name = p.Name;
            Id = p.Id;
            cost = p.cost;
        }
        public override string ToString()
        {
            return Display ?? string.Empty;

        }
        public Product(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
            cost = p.cost;
            LegacyProperty1 = string.Empty;
        }
    }
}
