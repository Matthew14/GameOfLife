using System;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment1
{
    class Board
    {
        private int SizeX, SizeY, i, j, randomNumber;
        //Storing all the cells in a 2D array
        public Cell[,] cellsArray; 

        Random random = new Random();

        public Board(int SizeX, int SizeY)
        {
            this.SizeX = SizeX;
            this.SizeY = SizeY;
            populate();
        }

        public Board()
        {
            this.SizeX = 100;
            this.SizeY = 100;
            populate();
        }

        public void populate()
        {
            cellsArray = new Cell[SizeX, SizeY];
            for (i = 0; i < SizeX; i++)
                for (j = 0; j < SizeY; j++)
                {
                    cellsArray[i, j] = new Cell(i * Cell.length, j * Cell.length);

                    //Randomly make a cell alive or dead
                    randomNumber = random.Next(0, 4);
                    if (randomNumber == 1) //one in four chance of being alive
                        cellsArray[i, j].Alive = true;
                }
        }

        public void clearAll()
        {
            for (i = 0; i < SizeX; i++)
                for (j = 0; j < SizeY; j++)
                {
                    cellsArray[i, j].Alive = false;
                    cellsArray[i, j].nextAlive = false;
                }
            Game1.Paused = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (i = 0; i < SizeX; i++)
                for (j = 0; j < SizeY-10; j++)
                {
                    cellsArray[i, j].Draw(spriteBatch);
                }
        }

        public void update()
        {
            
            int noNeighbours;

            for (i = 0; i < SizeX; i++)
            {
                for (j = 0; j < SizeY; j++)
                {
                    noNeighbours = liveNeighbours(i, j);
                    if (cellsArray[i, j].Alive)
                    {
                        //Any live cell with fewer than two live neighbours dies, as if caused by under-population.
                        if (noNeighbours < 2)
                            cellsArray[i, j].nextAlive = false;
                        //Any live cell with two or three live neighbours lives on to the next generation.
                        else if (noNeighbours < 4)
                            cellsArray[i, j].nextAlive = true;
                        //Any live cell with more than three live neighbours dies, as if by overcrowding.
                        else if (noNeighbours > 3)
                            cellsArray[i, j].nextAlive = false;
                    }
                    //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                    else if (noNeighbours == 3)
                        cellsArray[i, j].nextAlive = true;      
                }
                
            }

            //Next Generation:
            for (i = 0; i < SizeX; i++)
            {
                for (j = 0; j < SizeY; j++)
                {
                    cellsArray[i, j].Alive = cellsArray[i, j].nextAlive;
                }
            }
        }

        private int liveNeighbours(int x, int y)
        {
            int noNeighbours = 0;

            if (x > 0)
                if (cellsArray[x - 1, y].Alive) //Left
                    noNeighbours++;
            
            if (x > 0 && y > 0)
                if (cellsArray[x - 1, y - 1].Alive) //Top Left
                    noNeighbours++;
            
            if (y > 0)
                if (cellsArray[x, y - 1].Alive) //Top
                    noNeighbours++;
            
            if (x < SizeX - 1)
                if (cellsArray[x + 1, y].Alive) //Right
                    noNeighbours++;
            
            if (x < SizeX - 1 && y < SizeY - 1)
                if (cellsArray[x + 1, y + 1].Alive) //Bottom Right
                    noNeighbours++;
            
            if (y < SizeY - 1)
                if (cellsArray[x, y + 1].Alive) //Bottom
                    noNeighbours++;

            if (x > 0 && y < SizeY - 2)
                if (cellsArray[x - 1, y + 1].Alive) //Bottom Left
                    noNeighbours++;

            if (x < SizeX - 1 && y > 0)
                if (cellsArray[x + 1, y - 1].Alive) // Top Right
                    noNeighbours++;

            return noNeighbours;
        }
    }
}