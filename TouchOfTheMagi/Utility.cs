using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace TouchOfTheMagi
{
    public static class Utility
    {
        /// <summary>
        /// Asks the player a yes no question and gets their response, true if y, false if n
        /// </summary>
        /// <param name="input">The question to ask the player put in a string</param>
        /// <returns></returns>
        public static bool Affirm(string input)
        {
            Console.WriteLine(input);
            var response = Console.ReadKey().Key;
            while (true)
            {
                switch (response)
                {

                    case ConsoleKey.N:
                        return false;
                    case ConsoleKey.Y:
                        return true;
                    default:
                        Console.WriteLine("Please press either Y or N");
                        response = Console.ReadKey().Key;
                        break;
                }
            }
        }
        /// <summary>
        /// Gets the players input and makes sure its an integer
        /// </summary>
        /// <returns></returns>
        public static int Number()
        {
            string input = Console.ReadLine();
            int output;
            while (!Int32.TryParse(input, out output))
            {
                Console.WriteLine("Please input a number");
                input = Console.ReadLine();
            }
            return output;
        }
        /// <summary>
        /// Gets the players input and makes sure its an integer
        /// </summary>
        /// <param name="question">The question to ask the player</param>
        /// <returns></returns>
        public static int Number(string question) 
        {
            Console.WriteLine(question);
            string input = Console.ReadLine();
            int output;
            while (!Int32.TryParse(input, out output))
            {
                Console.WriteLine("Please input a number");
                input = Console.ReadLine();
            }
            return output;
        }
        /// <summary>
        /// Writes a line to the center of the console line
        /// </summary>
        /// <param name="input">the string to center</param>
        public static void Center(string input)
        {
            int width = Console.WindowWidth;
            width = width - input.Length;
            if (width > 0) 
            {
                width = width / 2;
                for (int i = 0; i < width; i++)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine(input);
        }
        /// <summary>
        /// makes a horisontal line of hyphons
        /// </summary>
        public static void Line()
        {
            for (int i = 0; i < Console.WindowWidth-1; i++)
            {
                Console.Write("-");
                
            }
            Console.WriteLine("-");
        }
        






    }
}
