using System.Collections.Generic;

namespace BattleShips.BusinessLayer
{
    public static class BoardValue
    {
        public static Dictionary<string, int> columnString = new Dictionary<string, int>()
        {
            {"A", 0},
            {"B", 1},
            {"C", 2},
            {"D", 3},
            {"E", 4},
            {"F", 5},
            {"G", 6},
            {"H", 7},
            {"I", 8},
            {"J", 9},
        };

        public static Dictionary<string, int> rowString = new Dictionary<string, int>()
        {
            {"0", 0},
            {"1", 1},
            {"2", 2},
            {"3", 3},
            {"4", 4},
            {"5", 5},
            {"6", 6},
            {"7", 7},
            {"8", 8},
            {"9", 9},
        };
    }
}
