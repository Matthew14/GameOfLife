using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment1
{
    class SpeedButton : Button
    {
        public float speed;

        public SpeedButton(float startSpeed, int PosX, int PosY, int SizeX, int SizeY)
        {
            this.speed = startSpeed;

            this.PosX = PosX;
            this.PosY = PosY;
            this.SizeX = SizeX;
            this.SizeY = SizeY;

            ButtonRect = new Rectangle(PosX, PosY, SizeX, SizeY);
            lastMouseState = Mouse.GetState();
        }

        public void Update(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Up))
            {
                if (speed < 50f)
                    speed++;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                if (speed > 5)
                    speed--;
            }

            MouseState mouseState = Mouse.GetState();

            if (ButtonRect.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed)
            {
                speed++;
            }

            lastMouseState = mouseState;
        }

        public override void  Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            ButtonText = "Speed (UP/DOWN): " + speed.ToString();

            spriteBatch.Draw(Game1.buttonSprite, ButtonRect, Color.Brown);
            Vector2 FontOrigin = font.MeasureString(ButtonText) / 2;
            spriteBatch.DrawString(font, ButtonText, new Vector2(PosX + SizeX / 2, PosY + SizeY / 2), Color.Black, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
