using BattleShips.BusinessLayer;
using System;
using System.Linq;

namespace Battleships
{
    public interface IIoHelperBoard
    {
        void AssignValueToBoard(string[,] board);
        void AssignValueToColumns(string[] columns);
        void AssignValueToRows(string[] rows);
        void DrawBoard(string[] columns, string[] rows, string[,] board);
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

        public void AssignValueToColumns(string[] table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = BoardValue.columnString.Keys.ElementAt(i);
            }
        }

        public void AssignValueToRows(string[] table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = BoardValue.rowString.Keys.ElementAt(i);
            }
        }

        public void DrawBoard(string[] columns, string[] rows, string[,] board)
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
                Console.Write($"{rows[x]}|");

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
