using BattleShips.BusinessLayer;
using BattleShips.BusinessLayer.Models;
using System;

namespace Battleships
{
    public interface IIoHelperShot
    {
        Coordinate GetShotFromUser(string message);
        void ParseShot(Coordinate userShot, string[,] board);
    }

    public class IoHelperShot : IIoHelperShot
    {
        private readonly IIoHelper _ioHelper;
        private readonly IShipService _shipService;
        private readonly IShotService _shotService;

        public IoHelperShot(
            IIoHelper ioHelper,
            IShipService shipServic,
            IShotService shotService)
        {
            _ioHelper = ioHelper;
            _shipService = shipServic;
            _shotService = shotService;
        }

        public Coordinate GetShotFromUser(string message)
        {
            bool correctShot;
            var userShot = new Coordinate();

            do
            {
                var shot = _ioHelper.GetStringFromUser(message);

                if (shot.Length != 2)
                {
                    _ioHelper.WriteColorText(ConsoleColor.Red, "Wrong shot format (*example: A5). Try again...");
                    correctShot = false;
                    continue;
                }
                if (!int.TryParse(shot.Substring(1, shot.Length - 1), out userShot.Row))
                {
                    _ioHelper.WriteColorText(ConsoleColor.Red, "Wrong shot format (*example: A5). Try again...");
                    correctShot = false;
                }
                else
                {
                    userShot.Column = shot.Substring(0, 1).ToUpper();
                    correctShot = true;
                }
            }
            while (!correctShot);

            return userShot;
        }

        public void ParseShot(Coordinate userShot, string[,] board)
        {
            if (_shotService.CheckIfShotIsInBoard(userShot, board))
            {
                _ioHelper.WriteColorTextOnCleanConsole(ConsoleColor.Red, "Shot off the board. Try again...");
                return;
            }
            if (_shotService.CheckIfShotIsRepeated(board, userShot))
            {
                _ioHelper.WriteColorTextOnCleanConsole(ConsoleColor.DarkBlue, "That's where you've already aimed. Try again...");
                return;
            }

            ParseShotAtBoard(board, userShot);
        }

        private void ParseShotAtBoard(string[,] board, Coordinate userShot)
        { 
            if (_shipService.CheckIfShotIsHit(userShot))
            {
                _shipService.UpdateShipAfterHit(userShot);

                var message = _shipService.CheckIfShipIsSunken(userShot) ? "Hit and sink!" : "Hit!";
                _ioHelper.WriteColorTextOnCleanConsole(ConsoleColor.Green, $"\t{message}");

                _shotService.MarkShotOnBoard(board, userShot, "X");
            }
            else
            {
                _ioHelper.WriteColorTextOnCleanConsole(ConsoleColor.Yellow, "\tMiss!");

                _shotService.MarkShotOnBoard(board, userShot, "0");
            }
        }
    }
}
