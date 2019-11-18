using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TypeInvaders.Classes;

namespace TypeInvaders {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        private int width = 1280;
        private int height = 800;
        private int sideBarWidth = 100;
        
        private SpriteFont BasicWordFont;
        private SpriteFont MarkedWordFont;

        private Texture2D grayBackground;
        private Texture2D redBackground;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Height and width of the window
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;

            // Frametime not limited to 16.66 Hz / 60 FPS
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load a font that can be used to draw text
            BasicWordFont = Content.Load<SpriteFont>("BasicWord");
            MarkedWordFont = Content.Load<SpriteFont>("MarkedWord");

            // Load 2d textures
            grayBackground = Content.Load<Texture2D>("GrayBackground");
            redBackground = Content.Load<Texture2D>("2dTextures/red");

            // Set all parameters that the EnemyManager needs
            EnemyManager.graphicsDevice = GraphicsDevice;
            EnemyManager.BasicWordFont = BasicWordFont;
            EnemyManager.MarkedWordFont = MarkedWordFont;
            EnemyManager.Background = redBackground;
            EnemyManager.GameArea = new Rectangle(sideBarWidth, 0, width-sideBarWidth, height);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            KeyboardState state = Keyboard.GetState();
            EnemyManager.Update(state);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            DrawSideBar();
            EnemyManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawSideBar() {
            //spriteBatch.Draw(grayBackground, new Rectangle(sideBarWidth, 0, width - sideBarWidth, height), Color.White);
            spriteBatch.Draw(grayBackground, new Rectangle(0, 0, sideBarWidth, height), Color.White);
        }
    }
}
