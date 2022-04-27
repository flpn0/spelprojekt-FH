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
       

        KeyboardState tangentbord = Keyboard.GetState();
        KeyboardState gammalTangentbord = Keyboard.GetState();

        Random slump = new Random();
        int scen = 0;
        Texture2D Basket;
        Texture2D parrotPicture;
        Rectangle parrotRectangle;
        Color parrotColor = Color.White;
        Texture2D eggPicture;
        Rectangle eggRectangle;
        Texture2D buttonPicture;
        Rectangle buttonRectangle;
        string welcomeText = "Collect The Eggs!";
        Vector2 welcomePosition;
        SpriteFont arial;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (scen == 1)
            {
                IsMouseVisible = false;
            }
            else
            {
                IsMouseVisible = true;
            }
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

            parrotPicture = Content.Load<Texture2D>("parrot");
            parrotRectangle = new Rectangle(100, 100, parrotPicture.Width, parrotPicture.Height);

            eggPicture = Content.Load<Texture2D>("zebra");
            eggRectangle = new Rectangle(100, -5, eggPicture.Width, eggPicture.Height);

            buttonPicture = Content.Load<Texture2D>("button");
            buttonRectangle = new Rectangle(640 - buttonPicture.Width / 2, 360, buttonPicture.Width, buttonPicture.Height);
            welcomePosition = new Vector2(640 - arial.MeasureString(welcomeText).X / 2, 100);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gammalTangentbord = tangentbord;
            tangentbord = Keyboard.GetState();

            switch(scen)
            {
                case 0:
                    UpdateMenu();
                    break;
                case 1:
                    UpdateGame();
                    break;
            }

            if (parrotRectangle.Contains(eggRectangle) == true)
            {
                parrotColor = Color.Red;
            }
            else
            {
                parrotColor = Color.White;
            }

            base.Update(gameTime);



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
