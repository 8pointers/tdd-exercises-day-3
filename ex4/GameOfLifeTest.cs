using System.Linq;
using NUnit.Framework;

namespace life
{
    public class GameOfLifeTest
    {
        private GameOfLife _gameOfLife = new GameOfLife(100);

        [SetUp]
        public void SetUp()
        {
            _gameOfLife = new GameOfLife(100);
        }

        [Test]
        public void Should_make_sure_all_the_cells_are_initially_dead()
        {
            Assert.AreEqual(false, _gameOfLife.IsCellAlive(2, 3));
        }

        [Test]
        public void Should_be_able_to_make_a_cell_alive_by_toggling_its_state()
        {
            _gameOfLife.ToggleCellState(2, 3);
            Assert.AreEqual(true, _gameOfLife.IsCellAlive(2, 3));
        }

        [Test]
        public void Should_be_able_to_make_a_cell_dead_by_toggling_its_state()
        {
            _gameOfLife.ToggleCellState(2, 3);
            _gameOfLife.ToggleCellState(2, 3);
            Assert.AreEqual(false, _gameOfLife.IsCellAlive(2, 3));
        }

        [Test]
        public void Alive_cell_with_less_than_two_live_neighbours_dies_in_the_next_iteration()
        {
            _gameOfLife.ToggleCellState(2, 3);
            _gameOfLife.ComputeNextIteration();
            Assert.AreEqual(false, _gameOfLife.IsCellAlive(2, 3));
        }

        [Test]
        public void Alive_cell_with_2_live_neighbours_survives_in_next_iteration()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4).ToggleCellState(2, 5);
            _gameOfLife.ComputeNextIteration();
            Assert.AreEqual(true, _gameOfLife.IsCellAlive(2, 4));
        }

        [Test]
        public void Alive_cell_with_3_live_neighbours_survives_in_next_iteration()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4).ToggleCellState(2, 5)
                .ToggleCellState(3, 4);

            _gameOfLife.ComputeNextIteration();

            Assert.AreEqual(true, _gameOfLife.IsCellAlive(2, 4));
        }

        [Test]
        public void Alive_cell_with_more_than_3_live_neighbours_should_die()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4).ToggleCellState(2, 5)
                .ToggleCellState(3, 3).ToggleCellState(3, 4);

            _gameOfLife.ComputeNextIteration();

            Assert.AreEqual(false, _gameOfLife.IsCellAlive(2, 4));
        }

        [Test]
        public void Dead_cell_should_become_alive_if_it_has_exactly_3_live_neighbours()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4);

            _gameOfLife.ComputeNextIteration();

            Assert.AreEqual(false, _gameOfLife.IsCellAlive(3, 3));
        }
    }

    internal class GameOfLife
    {
        private static (int OffsetRow, int OffsetColumn)[] Offsets = {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)};
        private readonly int _n;
        private readonly bool[,] _isAlive;


        public GameOfLife(int n)
        {
            _n = n;
            _isAlive = new bool[n,n];
        }

        public bool IsCellAlive(int row, int col)
        {
            return row >= 0 && row < _n && col >= 0 && col < _n && _isAlive[row, col];
        }

        public GameOfLife ToggleCellState(int row, int col)
        {
            _isAlive[row, col] = !_isAlive[row, col];
            return this;
        }

        public void ComputeNextIteration()
        {
            var neighbours = new int[_n, _n];
            for (var i = 0; i < _n; i++)
            for (var j = 0; j < _n; j++)
                neighbours[i, j] = Offsets.Aggregate(0,
                    (count, tuple) => count + (IsCellAlive(i + tuple.OffsetRow, j + tuple.OffsetColumn) ? 1 : 0));
            for (var i = 0; i < _n; i++)
            for (var j = 0; j < _n; j++)
                _isAlive[i, j] = _isAlive[i, j] && neighbours[i, j] == 2 || neighbours[i, j] == 3;

        }
    }
}
