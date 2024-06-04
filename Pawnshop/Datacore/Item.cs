using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Datacore
{
    public class Item
    {
        public string name { get; set; }
        public int price { get; set; }
        public int age { get; set; }

        public Item(string name, int price, int age)
        {
            this.name = name;
            this.price = price;
            this.age = age;
        }

        public override string ToString()
        {
            return $"*{name}*{price}*{age}";
        }
    }
}
