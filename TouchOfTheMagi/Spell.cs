using System;
using System.Collections.Generic;
using System.Text;

namespace TouchOfTheMagi
{
    public class Spell
    {
        public string Name;
        public string Description;
        public int Number;
        public Spell() { }
        public Spell(string name,string description, int number)
        {
            Name = name;
            Description = description;
            Number = number;
        }
    }
}
