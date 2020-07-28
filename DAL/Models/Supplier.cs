using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Locality { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
