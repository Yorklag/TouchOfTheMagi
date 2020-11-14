using System;
using System.Collections.Generic;
using System.Text;

namespace TouchOfTheMagi
{
    /*List of areas and where they can lead to
     * Bedroom: study / basement
     * basement: research lab / garden
     * study: research lab / garden
     * research lab: magic leak
     * garden: magic leak
     * magic leak: research lab (if not already known)
     */
    public class Location
    {
        public string Name;
        public string Description;
        public List<Spell> SpellsOnScroll;
        public bool SpellUsed = false;
        public virtual void Cast(Spell spell, Game game)
        {
            Console.WriteLine("That spell has no effect in this room.");
        }
    }
    //each room does something different
    //depending on the spell used each room will give access to another room, give a description of the room, or fail to do anything.
    //the cast method pulls in the game to add each location to the location list in the game class.
    public class Bedroom : Location
    {
        public override void Cast(Spell spell, Game game)
        {
            if (spell.Number == 1)
            {
                Console.WriteLine("The glow of magic permeates through the room. everything is at least a little magical but nothing stands out above anything else.");
            }
            else
            {
                if (!SpellUsed)
                {
                    switch (spell.Name)
                    {
                        case "Night vision":
                            Console.WriteLine("As your vision becomes bright and clear you see the room in shades of grey. You see a hole in the wall that leads to a ladder going down through the tower.\n\n" +
                                "New Location Unlocked: Basement");
                            game.Locations.Add(new Basement()
                            {
                                Name = "Basement",
                                Description = "A room at the bottom of the tower dimly lit by torches. A wall of dirt seems to fill in a hallway to the north and a deep pool of water which you can't see the bottom of ",
                                SpellsOnScroll = new List<Spell>() { new Spell("Clear dirt","Magically vanishes dirt",4), new Spell("Breathe water","allows you to breathe underwater",5) }
                            });
                            SpellUsed = true;
                            break;
                        case "Light":
                            Console.WriteLine("As the light in your hand illuminates the room you catch a glimmer of light. \nAs you investigate to find what gave off the shine: a small metal key that you know unlocks the door that leads to your study." +
                                "\n\nNew Location Unlocked: Study");
                            game.Locations.Add(new Study() 
                            {
                                Name = "Study",
                                Description = "A desk sits in the center of the room with a scroll propped against a pile of books. Blue Curtains line the wall and an open window reveals the garden at the foot of the tower.",
                                SpellsOnScroll = new List<Spell>() { new Spell("Fire bolt","Shoot a small ball of fire that can set things alight.",6),new Spell("Slow fall","Allows you to very slow your fall to remain unhurt",7)}
                            });
                            SpellUsed = true;
                            break;
                        default:
                            Console.WriteLine("You cast the spell but nothing remarkable happens");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The spell doesn't seem to have any more effect.");
                }
            }
        }
    }
    public class Study : Location
    {
        public override void Cast(Spell spell, Game game)
        {
            if (spell.Number == 1)
            {
                Console.WriteLine($"The glow of magic permeates through the room. {(SpellsOnScroll.Count == 0 ? "Nothing stands out above anything else":"The scroll stands out amongst the glow of background magic. The knowledge of the spells is magical itself")}.");
            }
            else
            {
                if (!SpellUsed)
                {
                    switch (spell.Name.ToLower())
                    {
                        case "fire bolt":
                            Console.WriteLine("You fire a bolt of fire at the curtains, but not the entire curtain burns. THere is only a small portion of the curtain that burns in the small pattern of runes.\n" +
                                "You realise that these runes are the ones used to open a portal between the bedroom and the underground research lab\n\nNew Location Unlocked: Underground Research Lab");
                            game.Locations.Add(new UndergroundLab()
                            {
                                SpellsOnScroll = new List<Spell>() { new Spell("Reveal Secret Passages","shows any secret passages in the are, you're still not sure how this one works",8),new Spell("Surface", "Teleports you to the surface relative to wherever you are", 9) }
                            }
                                );
                            break;
                        case "slow fall":
                            Console.WriteLine("You jump out the window and cast the spell. You feel youself become weightless as you slowly drift down towards the ground\n\nNew Location Unlocked: Garden");
                            game.Locations.Add(new Garden() 
                            {
                                SpellsOnScroll = new List<Spell>() {new Spell("Kill plants","Kills plants in an area",10),new Spell("Grow plants","Makes plants in an area grow",11) }
                            });
                            break;
                        default:
                            Console.WriteLine("The spell seems to have no effect");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The spell doesn't seem to have any more effect");
                }
            }
        }
    }
    public class Basement : Location
    {
        public override void Cast(Spell spell, Game game)
        {
            if (spell.Number == 1)
            {
                Console.WriteLine($"The glow of magic permeates through the room. {(SpellsOnScroll.Count == 0 ? "Nothing stands out above anything else" : "The scroll stands out amongst the glow of background magic. The knowledge of the spells is magical itself")}.");
            }
            else
            {
                if (!SpellUsed)
                {
                    switch (spell.Name.ToLower())
                    {
                        case "clear dirt":
                            Console.WriteLine("you clear the dirt in te passageway ahead revealing a tunnel further in\n" +
                                "\n\nNew Location Unlocked: Underground Research Lab");
                            game.Locations.Add(new UndergroundLab()
                            {
                                SpellsOnScroll = new List<Spell>() { new Spell("Reveal Secret Passages", "shows any secret passages in the are, you're still not sure how this one works", 8), new Spell("Surface", "Teleports you to the surface relative to wherever you are", 9) }
                            }
                                );
                            break;
                        case "breathe water":
                            Console.WriteLine("You swim through the water which was a much longer tunnel than it appeared on the surface. As you reach the end you find yourself surrounded by plants.\n\nNew Location Unlocked: Garden");
                            game.Locations.Add(new Garden()
                            {
                                SpellsOnScroll = new List<Spell>() { new Spell("Kill plants", "Kills plants in an area", 10), new Spell("Grow plants", "Makes plants in an area grow", 11) }
                            });
                            break;
                        default:
                            Console.WriteLine("The spell seems to have no effect");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The spell doesn't seem to have any effect");
                }
            }
        }
    }
    public class Garden : Location
    {
        public Garden()
        {
            Name = "Garden";
            Description = "A small garden at the foot of the tower filled with small plants. On the wall of the tower is a one way portal that can take you back up to the bedroom." +
                "\nTo the south is a wall of dry vines that block your path.";
        }
        public override void Cast(Spell spell, Game game)
        {
            if (spell.Number == 1)
            {
                Console.WriteLine("As you look around the garden a couple of plants grow brighter than others, but you don't remember anything useful you can use them for.");
            }
            else
            {
                if (!SpellUsed)
                {
                    switch (spell.Name.ToLower())
                    {
                        case "kill plants":
                            Console.WriteLine("You aim your spell towards the vine growth blocking the path and watch as they shrivel up and die revealing the way forward" +
                                "\n\nNew Location Unlocked: Magic Leak");
                            SpellUsed = true;
                            game.Locations.Add(new MagicLeak());
                            break;
                        case "grow plants":
                            Console.WriteLine("Your spell goes off as the plants near the wall grow to an abnormal size creating a staircase above and past the vines\n\n" +
                                "New Location Unlocked: Magic Leak");
                            SpellUsed = true;
                            game.Locations.Add(new MagicLeak());

                            break;
                        default:
                            Console.WriteLine("The spell seems to have no effect");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The spell doesn't seem to have any effect");
                }
            }
        }
    }
    public class UndergroundLab : Location
    {
        public UndergroundLab()
        {
            Name = "Underground Lab";
            Description = "Off to the side is a portal to your bedroom. In the middle of the room sits a mysterious cube which sides are rotating seemingly at random. \n" +
                "This strange cube has symbols written on each of is faces but every time you look away or blink the symbols change.";
        }
        public override void Cast(Spell spell, Game game)
        {
            if (spell.Number == 1)
            {
                Console.WriteLine("As you look around the garden a couple of plants grow brighter than others, but you don't remember anything useful you can use them for.");
            }
            else
            {
                if (!SpellUsed)
                {
                    switch (spell.Name.ToLower())
                    {
                        case "reveal secret passages":
                            Console.WriteLine("As you cast the spell countless doors and cabinets around the room open one of which being a secret passage way to the surface" +
                                "\n\nNew Location Unlocked: Magic Leak");
                            SpellUsed = true;
                            game.Locations.Add(new MagicLeak());
                            break;
                        case "surface":
                            Console.WriteLine("You feel yourself being pulled through solid stone all the way to the surface\n\n" +
                                "New Location Unlocked: Magic Leak");
                            SpellUsed = true;
                            game.Locations.Add(new MagicLeak());

                            break;
                        default:
                            Console.WriteLine("The spell seems to have no effect");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The spell doesn't seem to have any effect");
                }
            }
        }

    }
    public class MagicLeak : Location
    {
        public MagicLeak()
        {
            Name = "Magic Leak";
            Description = "A large cylindrical area with a crack in the center of a large marble circle that takes up the entire area. When you walk up to it you can feel wind blowing from the crack.";
            SpellsOnScroll = new List<Spell>();
        }
        public override void Cast(Spell spell, Game game)
        {
            if (spell.Number == 1)
            {
                if (!game.CurrentPlayer.Knoledge)
                {
                    bool test = false;
                    foreach (var location in game.Locations)
                    {
                        if (location.Name == "Underground Lab")
                        {
                            test = true;
                        }
                    }
                    Console.WriteLine("You see that the wind blowing forth from the crack isnt just wind as the blue tint of magic if flying from the crack.\nYou feel drawn to the magic and when you touch it with your hand it swirls around you filling you with knowledge\n" +
                        "Images flash before your eyes of a room with four stone walls and a strange cube in the center\n" +
                        $"{(!test?"The knowledge shows you that this room is a undergound lab beneath the tower":"You recognize this as the secret lab beneath the tower")}" +
                        $"{(test?"":"\n\nNew Location Unlocked: Undergound lab")}" +
                        $"\n\nYou suddenly understand the cube. The knowledge filling your head of countless solutions to the infinity possibilities that is this cube.\n\nNew Command Unlocked: Solve");
                    if (!test)
                    {
                        game.Locations.Add(new UndergroundLab());
                    }
                    game.CurrentPlayer.Knoledge = true;
                }
                else
                {
                    Console.WriteLine("Even though you can see further into the latent magic there is no new knowledge revealed to you.");
                }
            }
            else
            {
                Console.WriteLine("The spell seems to have no effect");
            }
        }
    }
}
