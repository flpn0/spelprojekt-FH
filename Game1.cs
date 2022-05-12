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

        int antalLiv = 0;
        int totemOfUndyingSpawnrate = 0;
        int liv = 1;
        int score = 0;
        int eggSpawnRate = 180;
        int slimeSpawnRate = 0;
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

        Texture2D bakgrundBild;
        Rectangle bakgrundPosition;

        Texture2D bakrundsMenyBild;
        Rectangle bakgrundsMenyPosition;

        Texture2D slimePicture;
        Rectangle slimeRectangle;

        Texture2D totemOfUndyingPicture;
        Rectangle totemOfUndyingRectangle;

        Texture2D HP;
        Rectangle HPsize;
        

        SpriteFont MinecraftFont;


        List<Rectangle> eggs = new List<Rectangle>();
        List<Rectangle> slimes = new List<Rectangle>();
        List<Rectangle> totemOfUndyings = new List<Rectangle>();
        List<Rectangle> livs = new List<Rectangle>();

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

            basketPicture = Content.Load<Texture2D>("basket");
            basketRectangle = new Rectangle(100, 550, basketPicture.Width, basketPicture.Height);

            eggPicture = Content.Load<Texture2D>("egg");
            eggRectangle = new Rectangle(100, -100, 100, 100);

            slimePicture = Content.Load<Texture2D>("slimeball");
            slimeRectangle = new Rectangle(100, -100, 100, 100);

            totemOfUndyingPicture = Content.Load<Texture2D>("totem");
            totemOfUndyingRectangle = new Rectangle(100, -100, 100, 100);

            MinecraftFont = Content.Load<SpriteFont>("file");

            buttonPicture = Content.Load<Texture2D>("button");
            buttonRectangle = new Rectangle(640 - buttonPicture.Width / 2, 360, buttonPicture.Width, buttonPicture.Height);
            welcomePosition = new Vector2(640 - MinecraftFont.MeasureString(welcomeText).X / 2, 100);

            bakgrundBild = Content.Load<Texture2D>("minecraft2");
            bakgrundPosition = new Rectangle(0, 0, 1280, 720);

            bakrundsMenyBild = Content.Load<Texture2D>("bakgrundMeny");
            bakgrundsMenyPosition = new Rectangle(0, 0, 1280, 720);

            HP = Content.Load<Texture2D>("heart");
            HPsize = new Rectangle(108, 800 - 108, 50, 50);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gammalTangentbord = tangentbord;
            tangentbord = Keyboard.GetState();
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
                case 2:
                    UpdateGameOver();
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
                case 2:
                    DrawGameOver();
                    break;
            }


            base.Draw(gameTime);
        }

        public void switchMenu(int nyscen)
        {
            scen = nyscen;
        }

        public bool leftMouseClick()
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

            if (leftMouseClick() == true && buttonRectangle.Contains(mus.Position) == true)
            {
                switchMenu(1);
            }
        }

        public void UpdateGame()
        {


            if (eggSpawnRate == 180)
            {
                for (int i = 0; i < 5; i++)
                {
                    Rectangle eggSpawn = new Rectangle(slump.Next(0, 1150), slump.Next(-1000, -100), 128, 128);
                    eggs.Add(eggSpawn);
                }
                eggSpawnRate = 0;
            }
            eggSpawnRate++;

            if (slimeSpawnRate == 300)
            {
                for (int i = 0; i < 1; i++)
                {
                    Rectangle slimeSpawn = new Rectangle(slump.Next(0, 1150), slump.Next(-100, -100), 96, 96);
                    slimes.Add(slimeSpawn);
                }
                slimeSpawnRate = 0;
            }
            slimeSpawnRate++;

            if (totemOfUndyingSpawnrate == 120)
            {
                for (int i = 0; i < 1; i++)
                {
                    Rectangle totemSpawn = new Rectangle(slump.Next(0, 1150), slump.Next(-1000, -50), 128, 128);
                    totemOfUndyings.Add(totemSpawn);
                }
                totemOfUndyingSpawnrate = 0;
            }
            totemOfUndyingSpawnrate++;

            for (int i = 0; i < eggs.Count; i++)
            {
                Rectangle temporary = eggs[i];

                temporary.Y += 3;
                eggs[eggs.IndexOf(eggs[i])] = temporary;
            }

            for (int i = 0; i < slimes.Count; i++)
            {
                Rectangle temporary = slimes[i];

                temporary.Y += 3;
                slimes[slimes.IndexOf(slimes[i])] = temporary;
            }

            for (int i = 0; i < totemOfUndyings.Count; i++)
            {
                Rectangle temporary = totemOfUndyings[i];

                temporary.Y += 3;
                totemOfUndyings[totemOfUndyings.IndexOf(totemOfUndyings[i])] = temporary;
            }

            for (int i = 0; i < slimes.Count; i++)
            {
                if (slimes[i].Intersects(basketRectangle) == true)
                {
                    slimes.RemoveAt(i);
                    liv--;
                    
                    break;
                }
            }

            if (liv == 0)
            {
                switchMenu(2);
            }

            

            for (int i = 0; i < totemOfUndyings.Count; i++)
            {
                if (totemOfUndyings[i].Intersects(basketRectangle) == true)
                {
                    totemOfUndyings.RemoveAt(i);
                    liv++;

                    break;
                }
            }

            for (int i = 0; i < eggs.Count; i++)
            {
                if (eggs[i].Intersects(basketRectangle) == true)
                {
                    eggs.RemoveAt(i);
                    score++;
                    break;
                }
            }

            for (int y = 0; y < eggs.Count; y++)
            {
                for (int i = 0; i < eggs.Count; i++)
                {
                    if (eggs[i].Intersects(eggs[y]) == true && i != y)
                    {
                        eggs.RemoveAt(i);

                        break;
                    }
                }
            }

            for (int y = 0; y < slimes.Count; y++)
            {
                for (int i = 0; i < eggs.Count; i++)
                {
                    if (eggs[i].Intersects(slimes[y]) == true && i != y)
                    {
                        eggs.RemoveAt(i);

                        break;
                    }
                }
            }

            for (int y = 0; y < totemOfUndyings.Count; y++)
            {
                for (int i = 0; i < eggs.Count; i++)
                {
                    if (eggs[i].Intersects(totemOfUndyings[y]) == true && i != y)
                    {
                        eggs.RemoveAt(i);

                        break;
                    }
                }
            }

            for (int y = 0; y < totemOfUndyings.Count; y++)
            {
                for (int i = 0; i < slimes.Count; i++)
                {
                    if (slimes[i].Intersects(totemOfUndyings[y]) == true && i != y)
                    {
                        slimes.RemoveAt(i);

                        break;
                    }
                }
            }


            if (tangentbord.IsKeyDown(Keys.Left) || tangentbord.IsKeyDown(Keys.A))
            {
                basketRectangle.X -= 15;
            }
            if (tangentbord.IsKeyDown(Keys.Right) || tangentbord.IsKeyDown(Keys.D))
            {
                basketRectangle.X += 15;
            }

            if (basketRectangle.X > 1280)
            {
                basketRectangle.X = 1 - basketPicture.Width;
            }
            if (basketRectangle.X < -basketPicture.Width)
            {
                basketRectangle.X =  1279;
            }


        }

        public void UpdateGameOver()
        {

        }

        public void DrawMenu()
        {

            spriteBatch.Begin();
            spriteBatch.Draw(bakrundsMenyBild, bakgrundsMenyPosition, Color.White);
            spriteBatch.DrawString(MinecraftFont, welcomeText, welcomePosition, Color.SandyBrown);
            spriteBatch.Draw(buttonPicture, buttonRectangle, Color.White);
            spriteBatch.End();
        }

        public void DrawGame()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(bakgrundBild, bakgrundPosition, Color.White);

            for (int i = 0; i < eggs.Count; i++)
            {
                spriteBatch.Draw(eggPicture, eggs[i], Color.White);
            }

            for (int i = 0; i < slimes.Count; i++)
            {
                spriteBatch.Draw(slimePicture, slimes[i], Color.White);
            }

            for (int i = 0; i < totemOfUndyings.Count; i++)
            {
                spriteBatch.Draw(totemOfUndyingPicture, totemOfUndyings[i], Color.White);
            }

            for (int i = 0; i < liv; i++)
            {
                spriteBatch.Draw(HP, new Rectangle(50, 650, 50, 50), Color.White);


                if (liv > antalLiv)
                {
                    spriteBatch.Draw(HP, new Rectangle(i + 200, 650, 50, 50), Color.White);
                }
                antalLiv++;
            }

            spriteBatch.Draw(basketPicture, basketRectangle, Color.White);

            spriteBatch.End();

            
        }

        public void DrawGameOver()
        {

        }
    }
}
