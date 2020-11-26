using NUnit.Framework;

namespace lifeSolution
{
    [TestFixture]
    public class GameOfLifeTest
    {
        private GameOfLife _gameOfLife = new GameOfLife();

        [SetUp]
        public void SetUp()
        {
            _gameOfLife = new GameOfLife();
        }

        [Test]
        public void Should_ensure_all_the_cells_are_initially_dead()
        {
            Assert.IsFalse(_gameOfLife.IsCellAlive(2, 3));
        }

        [Test]
        public void Should_be_able_to_make_any_dead_cell_alive_by_toggling_its_state()
        {
            _gameOfLife.ToggleCellState(2, 3);

            Assert.IsTrue(_gameOfLife.IsCellAlive(2, 3));
        }

        [Test]
        public void Should_be_able_to_make_any_alive_cell_die_by_toggling_its_state()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 3);

            Assert.IsFalse(_gameOfLife.IsCellAlive(2, 3));
        }

        [Test]
        public void Should_make_an_alive_cell_with_less_than_two_live_neighbours_dead_in_next_generation()
        {
            _gameOfLife.ToggleCellState(2, 3);

            _gameOfLife.Tick();

            Assert.IsFalse(_gameOfLife.IsCellAlive(2, 3));
        }

        [Test]
        public void Should_make_an_alive_cell_with_exactly_two_live_neighbours_survive_in_next_generation()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4).ToggleCellState(2, 5);

            _gameOfLife.Tick();

            Assert.IsTrue(_gameOfLife.IsCellAlive(2, 4));
        }

        [Test]
        public void Should_make_an_alive_cell_with_exactly_three_live_neighbours_survive_in_next_generation()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4).ToggleCellState(2, 5)
                       .ToggleCellState(3, 3);

            _gameOfLife.Tick();

            Assert.IsTrue(_gameOfLife.IsCellAlive(2, 4));
        }

        [Test]
        public void Should_make_an_alive_cell_with_more_than_three_live_neighbours_die_in_next_generation()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4).ToggleCellState(2, 5)
                       .ToggleCellState(3, 3).ToggleCellState(3, 4);

            _gameOfLife.Tick();

            Assert.IsFalse(_gameOfLife.IsCellAlive(2, 4));
        }

        [Test]
        public void Should_make_a_dead_cell_with_exactly_three_live_neighbours_become_alive_in_next_generation()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4).ToggleCellState(2, 5);

            _gameOfLife.Tick();

            Assert.IsTrue(_gameOfLife.IsCellAlive(3, 4));
        }

        [Test]
        public void Should_make_a_dead_cell_with_exactly_two_live_neighbours_dead_in_next_generation()
        {
            _gameOfLife.ToggleCellState(2, 3).ToggleCellState(2, 4).ToggleCellState(2, 5);

            _gameOfLife.Tick();

            Assert.IsFalse(_gameOfLife.IsCellAlive(3, 3));
        }
    }
}
