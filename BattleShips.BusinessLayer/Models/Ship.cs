namespace BattleShips.BusinessLayer.Models
{
    public enum Direction
    {
        Horizontal = 0,
        Vertical = 1,
    }

    public class Ship
    {
        public string Name;
        public int Length;
        public Direction Direction;
    }
}
