using BattleShips.BusinessLayer;
using System;

namespace Battleships
{
    public interface IIoHelperBoard
    {
        void AssignValueToBoard(string[,] board);
        void DrawBoard(string[,] board);
    }

    public class IoHelperBoard : IIoHelperBoard
    {
        public  void AssignValueToBoard(string[,] board)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    board[x, y] = " ";
                }
            }
        }

        public void DrawBoard(string[,] board)
        {
            Console.WriteLine("\tBOARD:");
            Console.Write(" |");

            foreach (var item in BoardValue.columnString)
            {
                Console.Write($"{item.Key}|");
            }
            Console.WriteLine();

            for (int x = 0; x < board.GetLength(0); x++)
            {
                Console.Write($"{x}|");

                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y]=="X")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{board[x, y]}");
                        Console.ResetColor();
                        Console.Write("|");
                    }
                    else
                    {
                        Console.Write($"{board[x, y]}|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
