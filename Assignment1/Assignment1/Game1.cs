using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace Assignment1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics { get; set; }
        public SpriteBatch spriteBatch;
        public float speed = 20f;
        KeyboardState lastState;

        public static bool Paused;
        public static Texture2D cellSprite, buttonSprite;
        Board board;
        Song song;
        PauseButton pauseButton;
        ClearButton clearButton;
        ResetButton resetButton;
        SpeedButton speedControl;
        SpriteFont ButtonFont;
        SoundEffect soundEffect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            Window.Title = "Game of Life";
            this.IsMouseVisible = true;
            lastState = Keyboard.GetState();

            graphics.IsFullScreen = false;

            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Zoviet France - E18 (released under creative commons, dowloaded from freemusicarchive.org)
            song = Content.Load<Song>("E18");

            //From freesound.org
            soundEffect = Content.Load<SoundEffect>("sound");

            ButtonFont = Content.Load<SpriteFont>("pauseButton");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cellSprite = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            cellSprite.SetData(new[] { Color.White });

            buttonSprite = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            buttonSprite.SetData(new[] { Color.White });

            graphics.PreferredBackBufferWidth = 900 - (900 % Cell.length);
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            board = new Board(graphics.PreferredBackBufferWidth / Cell.length, graphics.PreferredBackBufferHeight / Cell.length);
            pauseButton = new PauseButton(0, graphics.PreferredBackBufferHeight - 68, graphics.PreferredBackBufferWidth / 4, 68);
            clearButton = new ClearButton(pauseButton.SizeX, pauseButton.PosY, pauseButton.SizeX, pauseButton.SizeY);
            resetButton = new ResetButton(pauseButton.SizeX * 2, clearButton.PosY, pauseButton.SizeX, pauseButton.SizeY);
            speedControl = new SpeedButton(speed, pauseButton.SizeX * 3, clearButton.PosY, pauseButton.SizeX, pauseButton.SizeY);

            MediaPlayer.Play(song);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (state.IsKeyDown(Keys.P) && lastState.IsKeyUp(Keys.P)) // Pause game
                Paused = Paused ? false : true;
            if (state.IsKeyDown(Keys.R) && lastState.IsKeyUp(Keys.R)) //Randomize Board
                board.populate();
            if (state.IsKeyDown(Keys.C) && lastState.IsKeyUp(Keys.C)) //Clear Board
                board.clearAll();
            if (state.IsKeyDown(Keys.M) && lastState.IsKeyUp(Keys.M)) //Mute Music
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;

            speedControl.Update(state);
            lastState = state;

            //Controls the speed the update method is called:
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / speedControl.speed);

            if (!Paused)
            {
                MediaPlayer.Resume();
                board.update();
                base.Update(gameTime);
            }
            else
            {
                MediaPlayer.Pause();
            }

            foreach (Cell cell in board.cellsArray)
                cell.Update(soundEffect);

            pauseButton.Update();
            clearButton.Update(board);
            resetButton.Update(board);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            spriteBatch.Begin();

            board.Draw(spriteBatch);
            pauseButton.Draw(spriteBatch, ButtonFont);
            speedControl.Draw(spriteBatch, ButtonFont);
            clearButton.Draw(spriteBatch, ButtonFont);
            resetButton.Draw(spriteBatch, ButtonFont);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
