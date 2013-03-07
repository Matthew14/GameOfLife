using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Assignment1
{
    class PauseButton:Button
    {
        
        public PauseButton(int PosX, int PosY, int SizeX, int SizeY)
        {
            this.PosX = PosX;
            this.PosY = PosY;
            this.SizeX = SizeX;
            this.SizeY = SizeY;

            ButtonRect = new Rectangle(PosX, PosY, SizeX, SizeY);

            lastMouseState = Mouse.GetState();
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (ButtonRect.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed)
            {
                Game1.Paused = Game1.Paused ? false : true; 
            }

            lastMouseState = mouseState;
        
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (Game1.Paused)
            {
                spriteBatch.Draw(Game1.buttonSprite, ButtonRect, Color.Blue);
                ButtonText = "Unpause (P)";
            }
            else
            {
                spriteBatch.Draw(Game1.buttonSprite, ButtonRect, Color.Pink);
                ButtonText = "Pause (P)";
            }
            Vector2 FontOrigin = font.MeasureString(ButtonText) / 2;
            spriteBatch.DrawString(font, ButtonText, new Vector2(PosX+SizeX/2, PosY+SizeY/2), Color.Black, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

    }
}
