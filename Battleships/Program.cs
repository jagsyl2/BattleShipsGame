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
            var columns = new string[10];
            var rows = new string[10];
            var board = new string[10, 10];

            _ioHelperBoard.AssignValueToColumns(columns);
            _ioHelperBoard.AssignValueToRows(rows);
            _ioHelperBoard.AssignValueToBoard(board);

            _ioHelperBoard.DrawBoard(columns, rows, board);

            _shipService.RandomShipPlacement();

            _ioHelperShot.GetShot(columns, rows, board);
        }
    }
}
