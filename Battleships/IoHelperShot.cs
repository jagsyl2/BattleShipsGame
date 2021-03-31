using BattleShips.BusinessLayer;
using System;

namespace Battleships
{
    public interface IIoHelperShot
    {
        void GetShot(string[] columns, string[] rows, string[,] board);
    }

    public class IoHelperShot : IIoHelperShot
    {
        private readonly IIoHelper _ioHelper;
        private readonly IIoHelperBoard _ioHelperBoard;
        private readonly IShipService _shipService;
        private readonly IShotService _shotService;

        public IoHelperShot(
            IIoHelper ioHelper,
            IIoHelperBoard ioHelperBoard,
            IShipService shipServic,
            IShotService shotService)
        {
            _ioHelper = ioHelper;
            _ioHelperBoard = ioHelperBoard;
            _shipService = shipServic;
            _shotService = shotService;
        }

        public void GetShot(string[] columns, string[] rows, string[,] board)
        {
            bool endOfGame = false;
            do
            {
                Console.WriteLine("Take a shot: (*example: A5)");

                var userShot = _ioHelper.GetShotFromUser("Your shot:").ToUpper();

                ParseShot(userShot, board);

                _ioHelperBoard.DrawBoard(columns, rows, board);

                endOfGame = _shipService.CheckIfAllShipsSunken() ? true : false;
            }
            while (!endOfGame);

            _ioHelper.WriteColorText(ConsoleColor.DarkMagenta, "\t\tWINNER!!!");
        }

        private void ParseShot(string userShot, string[,] board)
        {
            if (userShot.Length != 2)
            {
                _ioHelper.WriteColorText(ConsoleColor.Red, "Wrong shot format. Try again...");
                return;
            }

            var columnString = userShot.Substring(0, 1);
            var rowString = userShot.Substring(1, userShot.Length - 1);

            if (_shotService.CheckIfShotIsInBoard(columnString, rowString))
            {
                _ioHelper.WriteColorText(ConsoleColor.Red, "Shot off the board. Try again...");
                return;
            }
            else if (_shipService.CheckIfShotIsHit(columnString, rowString) &&
                !_shotService.CheckIfShotIsRepeated(board, columnString, rowString))
            {
                _shipService.UpdateShipAfterHit(columnString, rowString);

                var message = _shipService.CheckIfShipIsSunken(columnString, rowString) ? "Hit and sink!" : "Hit!";
                _ioHelper.WriteColorText(ConsoleColor.Green, $"\t{message}");

                _shotService.MarkShotOnBoard(board, columnString, rowString, "X");
            }
            else if (_shotService.CheckIfShotIsRepeated(board, columnString, rowString))
            {
                _ioHelper.WriteColorText(ConsoleColor.DarkBlue, "That's where you've already aimed. Try again...");
            }
            else
            {
                _ioHelper.WriteColorText(ConsoleColor.Yellow, "\tMiss!");

                _shotService.MarkShotOnBoard(board, columnString, rowString, "0");
            }
        }
    }
}
