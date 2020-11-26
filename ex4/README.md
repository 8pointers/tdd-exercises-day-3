# Game of Life

Your task is to test-drive the design and the implementation of class for Conway's Game of Life. More info about the game itself can be found here:

https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life

The class you implement should have the following capabilities:

- It should be able to keep track the current state (whether they are dead or alive) of all the cells on the grid.

- All the cells should initially be dead.

- There should be a way to query whether a particular cell on the grid is dead or alive.

- There should be a way to "manually" toggle the state of any cell on the grid - if it was dead, it should become alive; if it was alive it should become dead.

- There should be a way to compute the next state of the game (the state in next "iteration"), according to the rules of the game:
    - Any live cell with fewer than two live neighbours dies, as if by underpopulation.
    - Any live cell with two or three live neighbours lives on to the next generation.
    - Any live cell with more than three live neighbours dies, as if by overpopulation.
    - Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.  
