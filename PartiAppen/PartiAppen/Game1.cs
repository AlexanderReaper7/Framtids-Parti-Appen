﻿using System.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tools_XNA_dotNET_Framework;

namespace PartiAppen
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MenuManager menuManager;
        public static Camera2D camera;


        private Song song;
        private SoundPlayer player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // Set to borderless window
            Window.IsBorderless = true;
            // Set window title
            Window.Title = "Framtids Parti Appen";
            // Show mouse
            IsMouseVisible = true;
            // Enable multisampling
            graphics.PreferMultiSampling = true;

            // Window size
            int windowHeight = 1080;
            int windowWidth = 720;
            // Display size
            int displayWidth = graphics.GraphicsDevice.DisplayMode.Width;
            int displayHeight = graphics.GraphicsDevice.DisplayMode.Height;

            // Set window size
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            // Move window to the center
            Window.Position = new Point((displayWidth / 2) - (windowWidth / 2), 0);

            camera = new Camera2D(this) {Origin = Vector2.Zero};

            // Apply changes
            graphics.ApplyChanges();

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

            song = Content.Load<Song>(@"Sounds/track1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            menuManager = new MenuManager();
            menuManager.LoadMenu(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            menuManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null,null,null,null, camera.GetViewMatrix());

            menuManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
