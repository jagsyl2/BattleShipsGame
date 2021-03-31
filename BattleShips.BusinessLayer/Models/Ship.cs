namespace BattleShips.BusinessLayer.Models
{
    public enum Direction
    {
        horizontal = 0,
        vertical = 1,
    }

    public class Ship
    {
        public string Name;
        public int Length;
        public Direction Direction;
    }
}
