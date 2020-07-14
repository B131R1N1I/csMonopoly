using System;

namespace MonopolyApp
{
    public class Property
    {
        public Property(string name, int amount, double price, int[] color)
        {
            this.name = name;
            this.owner = 0;
            this.amount = amount;
            this.price = price;
            this.color = color;
        }
        public readonly string name;
        public int owner
        {
            get
            {
                return owner;
            }
            set
            {
                Console.WriteLine($"{name} owner set to {value}");
                owner = value;
            }
        }
        public readonly int amount;// how many properties of this color
        public readonly double price;
        public readonly int[] color = new int[3];

    }
}