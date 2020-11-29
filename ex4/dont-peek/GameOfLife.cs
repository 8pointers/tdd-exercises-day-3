using System;
using System.Collections.Generic;
using System.Linq;

namespace lifeSolution
{
    public class GameOfLife
    {
        private static readonly (int OffsetRow, int OffsetColumn)[] Deltas = {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)};
        private HashSet<(int Row, int Column)> _isAlive = new HashSet<(int row, int column)>();

        public bool IsCellAlive(int row, int column)
        {
            return _isAlive.Contains((row, column));
        }

        public GameOfLife ToggleCellState(int row, int column)
        {
            (_isAlive.Contains((row, column)) ? (Func<(int, int), bool>) _isAlive.Remove : _isAlive.Add)((row, column));
            return this;
        }

        public void Tick()
        {
            _isAlive = _isAlive
                .SelectMany((cell, _) => Deltas.Select(delta => (Row: cell.Row + delta.OffsetRow, Column: cell.Column + delta.OffsetColumn)))
                .GroupBy(cell => cell, (cell, cells) => new {Cell = cell, NumberOfLiveNeighbours = cells.Count()})
                .Where(candidate => IsCellAlive(candidate.Cell.Row, candidate.Cell.Column) && candidate.NumberOfLiveNeighbours == 2 || candidate.NumberOfLiveNeighbours == 3)
                .Select(candidate => candidate.Cell)
                .ToHashSet();
        }
    }
}
