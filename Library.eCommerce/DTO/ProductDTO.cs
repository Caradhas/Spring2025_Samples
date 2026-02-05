using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public double? cost { get; set; }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name} ${cost} ";
            }
        }



        public ProductDTO()
        {
            Name = string.Empty;
            cost = 0.0;
        }
        public ProductDTO(Product p)
        {
            Name = p.Name;
            Id = p.Id;
            cost = p.cost;

        }

        public ProductDTO(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
            cost = p.cost;

        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}