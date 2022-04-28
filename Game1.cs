using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace spelprojekt_Felix_H
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
       

        KeyboardState tangentbord = Keyboard.GetState();
        KeyboardState gammalTangentbord = Keyboard.GetState();
        MouseState mus = Mouse.GetState();
        MouseState gammalMus = Mouse.GetState();

        int enemySpawnrate = 600;
        int scen = 0;
        Random slump = new Random();
        
        Texture2D basketPicture;
        Rectangle basketRectangle;
        Color basketColor = Color.White;
        Texture2D eggPicture;
        Rectangle eggRectangle;
        Texture2D buttonPicture;
        Rectangle buttonRectangle;
        string welcomeText = "Collect The Eggs!";
        Vector2 welcomePosition;
        SpriteFont arial;

        List<Rectangle> eggs = new List<Rectangle>();

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

            basketPicture = Content.Load<Texture2D>("parrot");
            basketRectangle = new Rectangle(100, 550, basketPicture.Width, basketPicture.Height);

            eggPicture = Content.Load<Texture2D>("zebra");
            eggRectangle = new Rectangle(100, -5, eggPicture.Width, eggPicture.Height);

            arial = Content.Load<SpriteFont>("file");

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

            if (enemySpawnrate == 600)
            {
                for (int i = 0; i < 5; i++)
                {
                    Rectangle eggSpawn = new Rectangle(slump.Next(0, graphics.PreferredBackBufferWidth), slump.Next(-200, -50), eggPicture.Width, eggPicture.Height);
                    eggs.Add(eggSpawn);
                }
                enemySpawnrate = 0;
            }
            enemySpawnrate++;

            for (int i = 0; i < eggs.Count; i++)
            {
                Rectangle temporary = eggs[eggs.IndexOf(eggs[i])];

                temporary.Y += 3;
                eggs[eggs.IndexOf(eggs[i])] = temporary;
            }

            for (int i = 0; i < eggs.Count; i++)
            {
                if (eggs[i].Intersects(basketRectangle) == true)
                {
                    eggs.RemoveAt(i);
                    break;
                }
            }

            switch(scen)
            {
                case 0:
                    UpdateMenu();
                    break;
                case 1:
                    UpdateGame();
                    break;
            }

            if (basketRectangle.Contains(eggRectangle) == true)
            {
                basketColor = Color.Red;
            }
            else
            {
                basketColor = Color.White;
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

        public void switchMenu(int nyscen)
        {
            scen = nyscen;
        }

        bool leftMouseClick()
        {
            if (mus.LeftButton == ButtonState.Pressed && gammalMus.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void UpdateMenu()
        {
            //if (vänstra musknapp precis trycktes && muspekaren är över knappen)
            //byt scen till 1

            if (leftMouseClick() == true && buttonRectangle.Contains(mus.Position) == true)
            {
                switchMenu(1);
            }
        }

        public void UpdateGame()
        {

        }

        public void DrawMenu()
        {
            GraphicsDevice.Clear(Color.LightCyan);

            spriteBatch.Begin();
            spriteBatch.DrawString(arial, welcomeText, welcomePosition, Color.BlueViolet);
            spriteBatch.Draw(buttonPicture, buttonRectangle, Color.White);
            spriteBatch.End();
        }

        public void DrawGame()
        {
            for (int i = 0; i < eggs.Count; i++)
            {
                spriteBatch.Draw(eggPicture, eggs[i], Color.White);
            }
        }
    }
}
