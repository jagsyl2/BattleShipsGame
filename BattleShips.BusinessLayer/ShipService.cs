using BattleShips.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShips.BusinessLayer
{
    public interface IShipService
    {
        void RandomShipPlacement(string[,] board);
        bool CheckIfShotIsHit(Coordinate userShot);
        bool CheckIfShipIsSunken(Coordinate userShot);
        void UpdateShipAfterHit(Coordinate userShot);
        bool CheckIfAllShipsSunken();
    }

    public class ShipService : IShipService
    {
        private readonly Dictionary<(string, int), DownShip> _shipsPositions = new Dictionary<(string, int), DownShip>();
        private readonly List<Ship> _shipList = new List<Ship>();
        
        private Random _random = new Random();

        private List<Ship> CreatListOfShips()
        {
            _shipList.Clear();

            var destroyer = new Ship()
            {
                Name = "Destroyer",
                Length = 4
            };

            var battleship = new Ship()
            {
                Name = "Battleship",
                Length = 5
            };

            _shipList.Add(destroyer);
            _shipList.Add(destroyer);
            _shipList.Add(battleship);

            return _shipList;
        }

        public void RandomShipPlacement(string[,] board)
        {
            _shipsPositions.Clear();

            CreatListOfShips();

            int downShipId = 1;

            foreach (var ship in _shipList)
            {
                bool shipAdded = false;

                do
                {
                    shipAdded = AssignShipCoordinates(board, downShipId, ship, shipAdded);
                }
                while (!shipAdded);

                downShipId++;
            }

            foreach (var item in _shipsPositions)                   //do skasowania!!!
            {
                Console.WriteLine($"{item.Key.Item1}{item.Key.Item2}-{ item.Value.Ship.Name} -id:{item.Value.Id} -{ item.Value.Down}");
            }
        }

        private bool AssignShipCoordinates(string[,] board, int downShipId, Ship ship, bool shipAdded)
        {
            int columnIndex, rowIndex;
            RandomStartCoordinateForShip(board, out columnIndex, out rowIndex);
            RandomChoiceOfDirection(ship);

            int i = ship.Length;
            int j = columnIndex;
            int k = rowIndex;

            while (i > 0)
            {
                var coordinate = ship.Direction == Direction.Horizontal ? GetCoordinate(j, rowIndex) : GetCoordinate(columnIndex, k);

                if (_shipsPositions.ContainsKey((coordinate.Column, coordinate.Row)))
                {
                    RemoveShipCoordinates(downShipId);
                    shipAdded = false;

                    break;
                }

                _shipsPositions.Add((coordinate.Column, coordinate.Row), new DownShip { Ship = ship, Id = downShipId });
                shipAdded = true;

                i--;
                var index = ship.Direction == Direction.Horizontal ? columnIndex <= BoardValue.columnString.Count / 2 ? j++ : j-- : rowIndex <= board.GetLength(0) / 2 ? k++ : k--;
            }

            return shipAdded;
        }

        private static Coordinate GetCoordinate(int columnIndex, int rowIndex)
        {
            return new Coordinate()
            {
                Column = BoardValue.columnString.Keys.ElementAt(columnIndex),
                Row = rowIndex
            };
        }

        //private static Coordinate GetHorizontal(int rowIndex, int columnIndex)
        //{
        //    return new Coordinate()
        //    {
        //        Column = BoardValue.columnString.Keys.ElementAt(columnIndex),
        //        Row = rowIndex
        //    };
        //}

        private void RandomStartCoordinateForShip(string[,] board, out int columnIndex, out int rowIndex)
        {
            columnIndex = RandomChoiceOfColumn();
            rowIndex = _random.Next(board.GetLength(0));
        }

        private int RandomChoiceOfColumn()
        {
            var index = _random.Next(BoardValue.columnString.Count);
            var columnIndex = BoardValue.columnString.Values.ElementAt(index);

            return columnIndex;
        }

        private void RandomChoiceOfDirection(Ship ship)
        {
            var values = Enum.GetValues(typeof(Direction));
            var positionIndex = _random.Next(values.Length);

            ship.Direction = (Direction)values.GetValue(positionIndex);
        }

        private void RemoveShipCoordinates(int downShipId)
        {
            var toRemove = _shipsPositions
                .Where(x => x.Value.Id == downShipId)
                .Select(x => x.Key)
                .ToList();

            foreach (var key in toRemove)
            {
                _shipsPositions.Remove(key);
            }
        }

        public bool CheckIfShotIsHit(Coordinate userShot)
        {
            return _shipsPositions.Any(x => x.Key.Item1 == userShot.Column && x.Key.Item2 == userShot.Row);
        }
        
        public bool CheckIfShipIsSunken(Coordinate userShot)
        {
            var shipId = _shipsPositions
                .Where(x => x.Key.Item1 == userShot.Column && x.Key.Item2 == userShot.Row)
                .Select(x => x.Value.Id)
                .FirstOrDefault();

            foreach (var item in _shipsPositions)                   //do skasowania!!!
            {
                Console.WriteLine($"{item.Key.Item1}{item.Key.Item2}-{ item.Value.Ship.Name} -id:{item.Value.Id} -{ item.Value.Down}");
            }

            return _shipsPositions
                .Where(x=>x.Value.Id == shipId)
                .All(x => x.Value.Down == true);
        }

        public void UpdateShipAfterHit(Coordinate userShot)
        {
            _shipsPositions
                .Where(x => x.Key.Item1 == userShot.Column && x.Key.Item2 == userShot.Row)
                .SingleOrDefault(x=>x.Value.Down = true);

            foreach (var item in _shipsPositions)                   //do skasowania!!!
            {
                Console.WriteLine($"{item.Key.Item1}{item.Key.Item2}-{ item.Value.Ship.Name} -id:{item.Value.Id} -{ item.Value.Down}");
            }
        }

        public bool CheckIfAllShipsSunken()
        {
            foreach (var item in _shipsPositions)                   //do skasowania!!!
            {
                Console.WriteLine($"{item.Key.Item1}{item.Key.Item2}-{ item.Value.Ship.Name} -id:{item.Value.Id} -{ item.Value.Down}");
            }


            return _shipsPositions.All(x=>x.Value.Down==true);


        }
    }
}
