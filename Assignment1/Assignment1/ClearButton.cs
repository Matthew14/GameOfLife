using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment1
{
    class ClearButton : Button
    {
        public ClearButton(int PosX, int PosY, int SizeX, int SizeY)
        {
            this.PosX = PosX;
            this.PosY = PosY;
            this.SizeX = SizeX;
            this.SizeY = SizeY;

            ButtonRect = new Rectangle(PosX, PosY, SizeX, SizeY);
            ButtonText = "Clear (C)";
            lastMouseState = Mouse.GetState();
        }
        public void Update(Board board)
        {
            MouseState mouseState = Mouse.GetState();

            if (ButtonRect.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed)
            {
                board.clearAll();
            }

            lastMouseState = mouseState;
        }
        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
           
            spriteBatch.Draw(Game1.buttonSprite, ButtonRect, Color.Red);
            Vector2 FontOrigin = font.MeasureString(ButtonText) / 2;
            spriteBatch.DrawString(font, ButtonText, new Vector2(PosX+SizeX/2, PosY+SizeY/2), Color.Black, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

    }
}
