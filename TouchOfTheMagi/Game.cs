using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TouchOfTheMagi
{
    public class Game
    {
        public string WorldName;
        public List<Location> Locations;
        public Player CurrentPlayer;
        public Game(Player player)
        {
            CurrentPlayer = player;

        }
        //Clears the screen and Puts the player name and location in the little bar at the top.
        public void Clear()
        {
            Console.Clear();
            Utility.Line();
            Utility.Center($"Player: {CurrentPlayer.Name} Location: {CurrentPlayer.CurrentLocation.Name}");
            Utility.Line();
        }
        //starts the game and begins the main loop for the game.
        public void Menu()
        {
            Clear();
            Console.WriteLine("You wake up in your bedroom. Not in bed like you would expect but on the floor.\nAs you try to recall the previous day you can't seem to remember anything.\n" +
                "You've lost your memory.\nYou look through the window and see only darkness. It's night.\n\nA spell scroll sits on the bedside table next to you\n" +
                "You remember writing these spell scrolls for just this sort of ocassion.\nUnfortunatly you wrote down the spells double sided on the scrolls and you can only learn one spell off of any scroll you find." +
                "\n\nUse commands to navigate your tower and figure out how to get your memories back.\n" +
                "Use the learn spell command to learn the first spell necessary\n" +
                "Use the cast command to use the spells\n" +
                "Use the move command to move from one place to another after you unlock another place\n" +
                "Use the help command to see a list of commands.\n\n" +
                "What would you like to do?\n");
            bool win = false;
            while(!win)
            {
                //gets player input and makes it lowercase for players to do commands
                switch (Console.ReadLine().ToLower().Trim())
                {
                    case "move":
                        Clear();
                        Console.WriteLine("Where would you like to move to?");
                        int i2 = 1;
                        //lists locations for player to choose from
                        foreach (var location in Locations)
                        {
                            Console.WriteLine($"{i2++}) {location.Name}\n{location.Description}\n");
                        }
                        //gets the players integer choise
                        int choice2 = Utility.Number();
                        if (choice2 > 0 && choice2 <= Locations.Count)
                        {
                            CurrentPlayer.CurrentLocation = Locations[choice2 - 1];
                            Clear();
                            Console.WriteLine($"You have moved to {CurrentPlayer.CurrentLocation.Name}.\n\n{CurrentPlayer.CurrentLocation.Description}");
                        }
                        //tells the player it failed if the choice was outside of the list of movable locations
                        else
                        {
                            Clear();
                            Console.WriteLine("You tried to select a location outside of the range of locations you can choose from");
                        }
                        break;
                    case "clear":
                        Clear();
                        break;
                    case "learn spell":
                        //checks if the spell scroll isnt empty
                        if (CurrentPlayer.CurrentLocation.SpellsOnScroll.Count == 0)
                        {
                            Console.WriteLine("there is no spell scroll in this location");
                        }
                        else
                        {
                            Console.WriteLine("Which spell would you like to learn?");
                            int i1 = 1;
                            //lists spells to choose from
                            foreach (var spell in CurrentPlayer.CurrentLocation.SpellsOnScroll)
                            {
                                Console.Write($"{i1++}) ");
                                Console.WriteLine(spell.Name);
                                Console.WriteLine(spell.Description);
                                Console.WriteLine();
                            }
                            //gets player choice
                            int choice1 = Utility.Number();
                            Clear();
                            if (choice1 > 0 && choice1 <= CurrentPlayer.CurrentLocation.SpellsOnScroll.Count)
                            {
                                //adds the spell to your list of spells and removes all spells from the list of spells on scroll
                                choice1--;
                                Console.WriteLine($"You added {CurrentPlayer.CurrentLocation.SpellsOnScroll[choice1].Name} to your list of spells. unfortunatly all other spells on the scroll were lost.");
                                CurrentPlayer.Spells.Add(CurrentPlayer.CurrentLocation.SpellsOnScroll[choice1]);
                                List<Spell> RemoveSpells = new List<Spell>();
                                foreach (var spell in CurrentPlayer.CurrentLocation.SpellsOnScroll)
                                {
                                    RemoveSpells.Add(spell);
                                }
                                foreach (var spell in RemoveSpells)
                                {
                                    CurrentPlayer.CurrentLocation.SpellsOnScroll.Remove(spell);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Selected spell not within range of spells on scroll.");
                            }
                        }
                        break;
                    case "cast":
                        //calls the cast method of the current location. The cast method is different depending on each location allowing the code run to be different for each area
                        Clear();
                        Console.WriteLine("Which spell would you like to cast?");
                        int i = 1;
                        foreach (var spell in CurrentPlayer.Spells)
                        {
                            Console.Write($"{i++}) {spell.Name}\n");
                        }
                        int choice = Utility.Number();
                        Clear();
                        if (choice > 0 && choice <= CurrentPlayer.Spells.Count)
                        {
                            CurrentPlayer.CurrentLocation.Cast(CurrentPlayer.Spells[choice-1],this);
                        }
                        else
                        {
                            Console.WriteLine("Your choice is not within the range of spells you've collected.");
                        }
                        break;
                    case"spell list":
                        //lists the spells you know
                        Clear();
                        Console.Write("Here are the spells you remember:  ");
                        bool first = true;
                        foreach (var spell in CurrentPlayer.Spells)
                        {
                            if (!first)
                            {
                                Console.Write(", ");
                            }
                            Console.Write($"{spell.Name}");
                            first = false;
                        }
                        Console.WriteLine();
                        break;
                    case "help":
                        //lists the commands you can use
                        Console.WriteLine($"Here is a list of commands: Clear, Help, Cast spell, Move, Spell List, Learn Spell{(CurrentPlayer.Knoledge?", Solve":"")}");
                        break;
                    case "solve":
                        //finishes the game if you have the knoledge and are in the right location
                        if (!CurrentPlayer.Knoledge)
                        {
                            goto default;
                        }
                        if(CurrentPlayer.CurrentLocation.Name == "Underground Lab")
                        {
                            Console.WriteLine("With the knoledge you've attained you are able to control the shifting of the cube. \nAs you turn the cube multiple different directions at the same time it begins to shift color.");
                            Console.Write("The more you move the sides the faster it shifts color until its sifing so fast the you can't tell what color it is anymore\n" +
                                "\nThen it stops. The cube is open and it settles on a cyan blue color\nThe color of ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("magic.");
                            Console.ResetColor();
                            Console.WriteLine("\n\nAs you examine the now open box you see a faint blue aura escaping from the box. It's magic.\nYou can see the magic with your normal eyes without the help of a spell.\n" +
                                "As you admire your handiwork and see the magic in the air your memories are restored.\nYou remember finding the cube in an ancient ruin of a magic school. Writing on tablets surrounding it" +
                                "describing it as a puzzle and test. It removes your memories and traps you in illusions brougt forth from your own mind, forcing you to find a way to escape lest you be stuck in your mind. " +
                                "\nThe walls of the tower fade revealing themselves to be an illusion as you find yourself in the center of the ancient ruins. The cube in front of you having found you acceptable and given you its gift" +
                                "\n:The ability to see magic given to you by the Cube of the Magi\n\nHit any key to continue ");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Thank you for playing. If you have any feedback let me know. I hope you enjoyed looking through the game. \n\nPress any key to close the game.");
                            
                            Console.ReadKey();

                            win = true;
                            break;
                        }
                        
                        Console.WriteLine();
                        break;
                    default:
                        //for any inputs that aren't commands it will tell to use the help command
                        Console.WriteLine("Command not recognised use 'help' command to se a list of commands");
                        break;
                }
            }

        }
    }
}
