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

namespace SnakeGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SnakeGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldKeyBoardState = Keyboard.GetState();
        Snake.Direction direction;
        const int gameWidth = 900;
        const int gameHeight = 700;
        public static GameState state = GameState.Menu;

        MenuButton playButton;
        MenuButton helpButton;
        MenuButton quitButton;
        MenuButton pauseButton;
        MenuButton resumeButton;

        Texture2D score;
        Texture2D menuBanner;
        Texture2D inGameBanner;
        Texture2D help;


        SpriteFont Font;

        Snake snake;
        Color bcolor = new Color(170, 225, 255);

        public static int scorePoints = 0;


        public enum GameState
        {
            Menu,
            InGame,
            Help,
            GameOver,
            Paused,
            Resumed,
            Quit
        };

        public enum Buttons
        {
            Play = 1,
            Help = 2,
            Quit = 3,
            Pause = 4,
            Resume = 5,

        };


        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = gameWidth;
            graphics.PreferredBackBufferHeight = gameHeight;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            playButton = new MenuButton(Content.Load<Texture2D>("playbutton"), GraphicsDevice, 1);
            helpButton = new MenuButton(Content.Load<Texture2D>("helpbutton"), GraphicsDevice, 2);
            quitButton = new MenuButton(Content.Load<Texture2D>("quitbutton"), GraphicsDevice, 3);
            pauseButton = new MenuButton(Content.Load<Texture2D>("pausebutton"), GraphicsDevice, 4, new Vector2(650, 10));
            resumeButton = new MenuButton(Content.Load<Texture2D>("resumebutton"), GraphicsDevice, 5, new Vector2(650, 10));

            direction = Snake.Direction.right;
            snake = new Snake(Content);



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Font");
            score = Content.Load<Texture2D>("scorebutton");
            menuBanner = Content.Load<Texture2D>("menubanner");
            inGameBanner = Content.Load<Texture2D>("ingamebanner");
            help = Content.Load<Texture2D>("help");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState currentKeyBoardState = Keyboard.GetState();

            switch (state)
            {
                case GameState.Menu:
                    MenuButton.Update(mouse);
                    break;
                case GameState.InGame:
                    if (currentKeyBoardState.IsKeyDown(Keys.Left))
                        direction = Snake.Direction.left;
                    if (currentKeyBoardState.IsKeyDown(Keys.Right))
                        direction = Snake.Direction.right;
                    if (currentKeyBoardState.IsKeyDown(Keys.Up))
                        direction = Snake.Direction.up;
                    if (currentKeyBoardState.IsKeyDown(Keys.Down))
                        direction = Snake.Direction.down;
                    pauseButton.updateSingleButton(mouse);
                    snake.Update(gameTime, direction);
                    break;
                case GameState.GameOver:
                    state = GameState.Menu;
                    direction = Snake.Direction.right;
                    break;
                case GameState.Paused:
                    resumeButton.updateSingleButton(mouse);
                    break;
                case GameState.Resumed:
                    pauseButton.updateSingleButton(mouse);
                    snake.Update(gameTime, direction);
                    state = GameState.InGame;
                    break;
                case GameState.Quit:
                    this.Exit();
                    break;
                case GameState.Help:
                    if (currentKeyBoardState.IsKeyDown(Keys.Escape)) state = GameState.Menu;
                    break;
            }
            oldKeyBoardState = currentKeyBoardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bcolor);

            spriteBatch.Begin();
            switch (state)
            {
                case GameState.Menu:
                    spriteBatch.Draw(menuBanner, Vector2.Zero, Color.White);
                    MenuButton.Draw(spriteBatch);

                    break;
                case GameState.InGame:
                    spriteBatch.Draw(inGameBanner, Vector2.Zero, Color.White);
                    pauseButton.drawSingleButton(spriteBatch, new Vector2(650, 10));
                    spriteBatch.Draw(score, new Vector2(650, 60), Color.White);
                    snake.Draw(spriteBatch);
                    spriteBatch.DrawString(Font, scorePoints.ToString(), new Vector2(760, 50), Color.LightSkyBlue);
                    break;
                case GameState.Paused:
                    spriteBatch.Draw(inGameBanner, Vector2.Zero, Color.White);
                    resumeButton.drawSingleButton(spriteBatch, new Vector2(650, 10));
                    spriteBatch.Draw(score, new Vector2(650, 60), Color.White);
                    snake.Draw(spriteBatch);
                    spriteBatch.DrawString(Font, scorePoints.ToString(), new Vector2(760, 50), Color.LightSkyBlue);
                    break;
                case GameState.Help:
                    spriteBatch.Draw(help, Vector2.Zero, Color.White);
                    break;

            }





            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
