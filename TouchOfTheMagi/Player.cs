using System;
using System.Collections.Generic;
using System.Text;

namespace TouchOfTheMagi
{
    public class Player
    {
        public string Name;
        
        public List<Spell> Spells = new List<Spell>();
        public Location CurrentLocation = new Location();
        public bool Knoledge = false;
    }
}
