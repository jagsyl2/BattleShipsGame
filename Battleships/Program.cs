using BattleShips.BusinessLayer;
using System;
using Unity;

namespace Battleships
{
    class Program
    {
        private readonly IIoHelper _ioHelper;
        private readonly IIoHelperBoard _ioHelperBoard;
        private readonly IIoHelperShot _ioHelperShot;
        private readonly IShipService _shipService;

        static void Main()
        {
            var container = new UnityDiContainerProvider().GetContainer();

            container.Resolve<Program>().Run();
        }

        public Program(
            IIoHelper ioHelper,
            IIoHelperBoard ioHelperBoard,
            IIoHelperShot ioHelperShot,
            IShipService shipService)
        {
            _ioHelper = ioHelper;
            _ioHelperBoard = ioHelperBoard;
            _ioHelperShot = ioHelperShot;
            _shipService = shipService;
        }

        private void Run()
        {
            bool _exit=false;

            do
            {
                Console.WriteLine("BATTLESHIPS");
                Console.WriteLine("1. Play");
                Console.WriteLine("2. Exit");

                var userChoice = _ioHelper.GetIntFromUser("Choose option:");

                switch (userChoice)
                {
                    case 1:
                        Play();
                        break;
                    case 2:
                        _exit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown option. Try again...");
                        Console.WriteLine();
                        break;
                }
            } 
            while (!_exit);
        }

        private void Play()
        {
            var board = new string[10, 10];
            _ioHelperBoard.AssignValueToBoard(board);

            _ioHelperBoard.DrawBoard(board);

            _shipService.RandomShipPlacement(board);

            GetShot(board);
        }

        public void GetShot(string[,] board)
        {
            bool endOfGame;
            do
            {
                Console.WriteLine("Take a shot: (*example: A5)");

                var userShot = _ioHelperShot.GetShotFromUser("Your shot:");

                _ioHelperShot.ParseShot(userShot, board);

                _ioHelperBoard.DrawBoard(board);

                endOfGame = _shipService.CheckIfAllShipsSunken() ? true : false;
            }
            while (!endOfGame);

            _ioHelper.WriteColorText(ConsoleColor.DarkMagenta, "\t\tWINNER!!!");
        }
    }
}
