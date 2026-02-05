using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring2025_Samples.Models;

namespace Library.eCommerce.Models
{
    public class TotalPrice
    {
        public double? totalCost { get; set; }
        public string Display
        {
            get
            {
                return $"{totalCost}";
            }
        }
        public TotalPrice()
        {
            totalCost = 0;
        }
        public void add(double? num)
        {
            totalCost += num;
        }
        public void remove(double? num)
        {
            totalCost -= num;
        }
    }
}
