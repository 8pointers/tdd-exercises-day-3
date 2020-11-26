using System.Collections.Generic;
using System.Linq;

namespace lifeSolution
{
    public class GameOfLife
    {
        private static readonly (int OffsetRow, int OffsetColumn)[] Deltas = {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)};
        private Dictionary<(int Row, int Column), bool> _isAlive = new Dictionary<(int row, int column), bool>();

        public bool IsCellAlive(int row, int column)
        {
            return _isAlive.ContainsKey((row, column));
        }

        public GameOfLife ToggleCellState(int row, int column)
        {
            if (_isAlive.ContainsKey((row, column)))
                _isAlive.Remove((row, column));
            else
                _isAlive[(row, column)] = true;
            return this;
        }

        public void Tick()
        {
            _isAlive = _isAlive.Keys
                .SelectMany(cell => Deltas.Select(delta => (Row: cell.Row + delta.OffsetRow, Column: cell.Column + delta.OffsetColumn)))
                .GroupBy(cell => cell, (cell, cells) => new {Cell = cell, NumberOfLiveNeighbours = cells.Count()})
                .Where(candidate => IsCellAlive(candidate.Cell.Row, candidate.Cell.Column) && candidate.NumberOfLiveNeighbours == 2 || candidate.NumberOfLiveNeighbours == 3)
                .ToDictionary(aliveOnes => aliveOnes.Cell, _ => true);
        }
    }
}
