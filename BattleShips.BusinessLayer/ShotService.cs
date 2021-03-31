namespace BattleShips.BusinessLayer
{
    public interface IShotService
    {
        bool CheckIfShotIsInBoard(string columnString, string rowString);
        bool CheckIfShotIsRepeated(string[,] board, string columnString, string rowString);
        void MarkShotOnBoard(string[,] board, string columnString, string rowString, string sign);
    }

    public class ShotService : IShotService
    {
        public bool CheckIfShotIsInBoard(string columnString, string rowString)
        {
            return !BoardValue.columnString.ContainsKey(columnString) ||
                !BoardValue.rowString.ContainsKey(rowString);
        }

        public bool CheckIfShotIsRepeated(string[,] board, string columnString, string rowString)
        {
            var columnIndex = BoardValue.columnString[columnString];
            var rowIndex = BoardValue.rowString[rowString];

            return board[rowIndex, columnIndex] == "X" || board[rowIndex, columnIndex] == "0" ? true: false;
        }

        public void MarkShotOnBoard(string[,] board, string columnString, string rowString, string sign)
        {
            var columnIndex = BoardValue.columnString[columnString];
            var rowIndex = BoardValue.rowString[rowString];

            board[rowIndex, columnIndex] = sign;
        }
    }
}
