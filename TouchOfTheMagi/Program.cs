/*
 * Touch of the Magi
 * a text adventure game by Michael Tendy
 * made for Columbia college programing 101 class
 * 
 * ascii art generated through http://patorjk.com/software/taag
 */

using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace TouchOfTheMagi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Touch of the Magi";
            //tries setting the console size and if it fails (due to monitor size or any other reason like a console in another program) doesn't use the ascii art and instead just lists the title.
            try
            {
                Console.WindowWidth = 132;
                Console.WindowHeight = 30;
                Console.WriteLine(@"
                                                             Welcome to 
_________ _______           _______             _______  _______   _________          _______    _______  _______  _______ _________
\__   __/(  ___  )|\     /|(  ____ \|\     /|  (  ___  )(  ____ \  \__   __/|\     /|(  ____ \  (       )(  ___  )(  ____ \\__   __/
   ) (   | (   ) || )   ( || (    \/| )   ( |  | (   ) || (    \/     ) (   | )   ( || (    \/  | () () || (   ) || (    \/   ) (   
   | |   | |   | || |   | || |      | (___) |  | |   | || (__         | |   | (___) || (__      | || || || (___) || |         | |   
   | |   | |   | || |   | || |      |  ___  |  | |   | ||  __)        | |   |  ___  ||  __)     | |(_)| ||  ___  || | ____    | |   
   | |   | |   | || |   | || |      | (   ) |  | |   | || (           | |   | (   ) || (        | |   | || (   ) || | \_  )   | |   
   | |   | (___) || (___) || (____/\| )   ( |  | (___) || )           | |   | )   ( || (____/\  | )   ( || )   ( || (___) |___) (___
   )_(   (_______)(_______)(_______/|/     \|  (_______)|/            )_(   |/     \|(_______/  |/     \||/     \|(_______)\_______/                                                                         

                                                      Press any key to continue
");
            }
            catch (Exception)
            {
                Console.WriteLine("Welcome to Touch of the Magi\n\nPress any key to continue");
            }
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("What is your name?");
            string name = "";
            //forces the player to input something as their name
            while (String.IsNullOrEmpty(name)) 
            {
                name = Console.ReadLine().Trim();
                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Please enter a name");
                }
            }
            List<Spell> spells = new List<Spell>();
            spells.Add(new Spell("Detect magic","allows you to see magic in an area", 1));
            Spell NightVision = new Spell("Night vision","lets you see in the dark, viewing everythings like it was day", 2);
            Spell Light = new Spell("Light","Makes a light in your hand.", 3);
            /*List of all spells created throughout the game
             * Start/
             * detect magic: 1
             * 
             * Bedroom/
             * Night vision: 2
             * Light: 3
             * 
             * Basement/
             * Clear dirt: 4
             * Breathe water: 5
             * 
             * Study/
             * Fire bolt: 6
             * Slow fall: 7
             * 
             * Underground Lab/
             * Reveal secret passages: 8
             * Surface: 9
             * 
             * Garden/
             * Kill plants: 10
             * Grow plants: 11
             * 
             * 
             * 
             */
            //initising the first room 
            Location Bedroom = new Bedroom()
            {
                Name = "Bedroom",
                Description = "It's your bedroom. the same place you retire to almost every night when you go to bed",
                SpellsOnScroll = new List<Spell>() { NightVision, Light },
            
            };


            Player player1 = new Player()
            {
                Name = name,
                Spells = spells,
                CurrentLocation = Bedroom,

            };
            
            Game game = new Game(player1);
            game.Locations = new List<Location>() {Bedroom};
            game.Menu();
        }
    }
}
