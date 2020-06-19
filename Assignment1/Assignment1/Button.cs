using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment1
{
    abstract class Button
    {
        public int SizeX, SizeY;
        public int PosX { get; set; }
        public int PosY { get; set; }

        public string ButtonText;

        public MouseState lastMouseState;

        public Rectangle ButtonRect;

        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);
    }
}
