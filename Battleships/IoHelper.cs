using System;

namespace Battleships
{
    public interface IIoHelper
    {
        void WriteColorText(ConsoleColor color, string message);
        string GetStringFromUser(string message);
        public string GetShotFromUser(string message);
        int GetIntFromUser(string message);
    }

    public class IoHelper : IIoHelper
    {
        public void WriteColorText(ConsoleColor color, string message)
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        public int GetIntFromUser(string message)
        {
            int number;
            
            while (!int.TryParse(GetStringFromUser(message), out number))
            {
                Console.WriteLine("It is not a number. Try again...");
                Console.WriteLine();
            }

            return number;
        }

        public string GetShotFromUser(string message)
        {
            var shot = GetStringFromUser(message);

            while (string.IsNullOrEmpty(shot))
            {
                Console.WriteLine("This is not a valid shot format. Try again...");
                Console.WriteLine();

                shot = GetStringFromUser(message);
            }

            return shot;
        }

        public string GetStringFromUser(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
