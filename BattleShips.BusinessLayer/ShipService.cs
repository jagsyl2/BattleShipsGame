using BattleShips.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShips.BusinessLayer
{
    public interface IShipService
    {
        void RandomShipPlacement();
        bool CheckIfShotIsHit(string columnString, string rowString);
        bool CheckIfShipIsSunken(string columnString, string rowString);
        void UpdateShipAfterHit(string columnString, string rowString);
        bool CheckIfAllShipsSunken();
    }

    public class ShipService : IShipService
    {
        private readonly Dictionary<(string, string), DownShip> _shipsPositions = new Dictionary<(string, string), DownShip>();
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

            var cruiser = new Ship()
            {
                Name = "Cruiser",
                Length = 4
            };

            var battleship = new Ship()
            {
                Name = "Battleship",
                Length = 5
            };

            _shipList.Add(destroyer);
            _shipList.Add(cruiser);
            _shipList.Add(battleship);

            return _shipList;
        }

        public void RandomShipPlacement()
        {
            _shipsPositions.Clear();

            CreatListOfShips();

            foreach (var ship in _shipList)
            {
                bool shipAdded = false;

                do
                {
                    int columnIndex = RandomChoiceOfColumn();
                    int rowIndex = RandomChoiceOfRow();

                    RandomChoiceOfDirection(ship);

                    int i = ship.Length;                                                    //czy moze byc taka zmienna?

                    if (ship.Direction == Direction.horizontal)
                    {
                        int j = columnIndex;

                        while (i > 0)
                        {
                            var coordinate = new Coordinate()
                            {
                                Column = BoardValue.columnString.Keys.ElementAt(j),
                                Row = BoardValue.rowString.Keys.ElementAt(rowIndex),
                            };

                            if (_shipsPositions.ContainsKey((coordinate.Column, coordinate.Row)))
                            {
                                RemoveShipCoordinates(ship);

                                shipAdded = false;
                                break;
                            }

                            _shipsPositions.Add((coordinate.Column, coordinate.Row), new DownShip {Ship = ship});
                            shipAdded = true;

                            i--;
                            var indexValue = columnIndex <= BoardValue.columnString.Count / 2 ? j++ : j--;
                        }
                    }
                    else
                    {
                        int j = rowIndex;

                        while (i > 0)
                        {
                            var coordinate = new Coordinate()
                            {
                                Column = BoardValue.columnString.Keys.ElementAt(columnIndex),
                                Row = BoardValue.rowString.Keys.ElementAt(j),
                            };

                            if (_shipsPositions.ContainsKey((coordinate.Column, coordinate.Row)))
                            {
                                RemoveShipCoordinates(ship);

                                shipAdded = false;
                                break;
                            }

                            _shipsPositions.Add((coordinate.Column, coordinate.Row), new DownShip { Ship = ship });
                            shipAdded = true;

                            i--;
                            var indexValue = rowIndex <= BoardValue.rowString.Count / 2 ? j++ : j--;
                        }
                    }
                }
                while (!shipAdded);
            }
        }

        private int RandomChoiceOfRow()
        {
            var index = _random.Next(BoardValue.rowString.Count);
            var rowIndex = BoardValue.rowString.Values.ElementAt(index);

            return rowIndex;
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

        private void RemoveShipCoordinates(Ship ship)
        {
            var toRemove = _shipsPositions
                .Where(x => x.Value.Ship == ship)
                .Select(x => x.Key)
                .ToList();

            foreach (var key in toRemove)
            {
                _shipsPositions.Remove(key);
            }
        }

        public bool CheckIfShotIsHit(string columnString, string rowString)
        {
            return _shipsPositions.Any(x => x.Key.Item1 == columnString && x.Key.Item2 == rowString);
        }
        
        public bool CheckIfShipIsSunken(string columnString, string rowString)
        {
            var ship = _shipsPositions
                .Where(x => x.Key.Item1 == columnString && x.Key.Item2 == rowString)
                .Select(x => x.Value.Ship)
                .FirstOrDefault();
                
            return _shipsPositions
                .Where(x=>x.Value.Ship == ship)
                .All(x => x.Value.Down == true);
        }

        public void UpdateShipAfterHit(string columnString, string rowString)
        {
            _shipsPositions
                .Where(x => x.Key.Item1 == columnString && x.Key.Item2 == rowString)
                .FirstOrDefault(x=>x.Value.Down = true);
        }

        public bool CheckIfAllShipsSunken()
        {
            return _shipsPositions.All(x=>x.Value.Down==true);
        }
    }
}
