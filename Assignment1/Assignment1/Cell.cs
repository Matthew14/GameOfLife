using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Assignment1
{
    class Cell
    {
        //Size of each cell
        public static int length = 9; //size of cell in pixels
        private int PosX, PosY; // Cell position
        //Last mouse state to prevent problems with holding button
        private MouseState lastMouseState; 

        private Rectangle cellRect;

        //This is to show if the cell is dead or alive:
        public bool Alive {get; set;}
        //This is to show if the cell will be dead or alive on the next generation:
        public bool nextAlive { get; set; }

        public Cell(int PosX, int PosY)
        {
            this.PosX = PosX;
            this.PosY = PosY;

            cellRect = new Rectangle(PosX, PosY, length, length);
        }

        public void Update(SoundEffect soundEffect)
        {
            MouseState mouseState = Mouse.GetState();
            //Check if clicked:
            if (cellRect.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed)
            {
                soundEffect.Play();
                Alive = Alive ? false : true;
            }
            lastMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            if (Alive)
                spriteBatch.Draw(Game1.cellSprite, cellRect, Color.Black);
            else
                spriteBatch.Draw(Game1.cellSprite, cellRect, Color.Green);
        }
        
    }
}