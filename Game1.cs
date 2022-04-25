using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace spelprojekt_Felix_H
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        MouseState mus = Mouse.GetState();
        MouseState gammalMus = Mouse.GetState();
        Random slump = new Random();
        int scen = 0;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gammalMus = mus;
            mus = Mouse.GetState();

            switch(scen)
            {
                case 0:
                    UpdateMenu();
                    break;
                case 1:
                    UpdateGame();
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (scen)
            {
                case 0:
                    DrawMenu();
                    break;
                case 1:
                    DrawGame();
                    break;
            }


            base.Draw(gameTime);
        }

        public void UpdateMenu()
        {

        }

        public void UpdateGame()
        {

        }

        public void DrawMenu()
        {

        }

        public void DrawGame()
        {

        }
    }
}
