using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SWP490_G9_PE.Models
{
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }

        public Product() { }
        public Product(int id, string name, string cate, string color, decimal uP, int avQ)
        {
            this.ProductId = id;
            this.Name = name;
            this.Category = cate;
            this.Color = color;
            this.UnitPrice = uP;
            this.AvailableQuantity = avQ;
        }
        public Product(string name, string cate, string color, decimal uP, int avQ)
        {
            this.Name = name;
            this.Category = cate;
            this.Color = color;
            this.UnitPrice = uP;
            this.AvailableQuantity = avQ;
        }
    }
}
