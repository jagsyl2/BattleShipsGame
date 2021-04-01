using BattleShips.BusinessLayer.Models;

namespace BattleShips.BusinessLayer
{
    public interface IShotService
    {
        bool CheckIfShotIsInBoard(Coordinate userShot, string[,] board);
        bool CheckIfShotIsRepeated(string[,] board, Coordinate userShot);
        void MarkShotOnBoard(string[,] board, Coordinate userShot, string sign);
    }

    public class ShotService : IShotService
    {
        public bool CheckIfShotIsInBoard(Coordinate userShot, string[,] board)
        {
            return !BoardValue.columnString.ContainsKey(userShot.Column) ||
                !(0 <= userShot.Row && userShot.Row <= board.GetLength(0));
        }

        public bool CheckIfShotIsRepeated(string[,] board, Coordinate userShot)
        {
            var columnIndex = BoardValue.columnString[userShot.Column];

            return board[userShot.Row, columnIndex] == "X" || board[userShot.Row, columnIndex] == "0" ? true: false;
        }

        public void MarkShotOnBoard(string[,] board, Coordinate userShot, string sign)
        {
            var columnIndex = BoardValue.columnString[userShot.Column];

            board[userShot.Row, columnIndex] = sign;
        }
    }
}
