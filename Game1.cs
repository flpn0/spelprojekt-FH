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
        MouseState mus = Mouse.GetState();
        MouseState gammalMus = Mouse.GetState();

        int totemOfUndyingSpawnrate = 0;
        int lives = 1;
        int score = 0;
        int eggSpawnRate = 180;
        int slimeSpawnRate = 0;
        int scene = 0;
        Random random = new Random();
        
        Texture2D basketPicture;
        Rectangle basketRectangle;

        Texture2D eggPicture;
        Rectangle eggRectangle;

        Texture2D buttonPicture;
        Rectangle buttonRectangle;

        Texture2D buttonPicture2;
        Rectangle buttonRectangle2;

        
        string gameOverText = "Game Over";
        string scoreText = "";
        string welcomeText = "Collect The Eggs!";
        Vector2 welcomePosition;
        Vector2 scoreTextPosition;
        Vector2 gameOverPosition;
        

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (scene == 1)
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // All pictures and their positions

            basketPicture = Content.Load<Texture2D>("basket");
            basketRectangle = new Rectangle(graphics.GraphicsDevice.Viewport.Width / 2 - basketPicture.Width / 2, 550, basketPicture.Width, basketPicture.Height);

            eggPicture = Content.Load<Texture2D>("egg");
            eggRectangle = new Rectangle(100, -100, 100, 100);

            slimePicture = Content.Load<Texture2D>("slimeball");
            slimeRectangle = new Rectangle(100, -100, 100, 100);

            totemOfUndyingPicture = Content.Load<Texture2D>("totem");
            totemOfUndyingRectangle = new Rectangle(100, -100, 100, 100);

            buttonPicture = Content.Load<Texture2D>("button");
            buttonRectangle = new Rectangle(640 - buttonPicture.Width / 2, 360, buttonPicture.Width, buttonPicture.Height);

            buttonPicture2 = Content.Load<Texture2D>("button2");
            buttonRectangle2 = new Rectangle(640 - buttonPicture.Width / 2, 360, buttonPicture.Width, buttonPicture.Height);

            bakgrundBild = Content.Load<Texture2D>("minecraft2");
            bakgrundPosition = new Rectangle(0, 0, 1280, 720);

            bakrundsMenyBild = Content.Load<Texture2D>("bakgrundMeny");
            bakgrundsMenyPosition = new Rectangle(0, 0, 1280, 720);

            HP = Content.Load<Texture2D>("heart");
            HPsize = new Rectangle(108, 800 - 108, 50, 50);

            // Font and the position of all text's

            MinecraftFont = Content.Load<SpriteFont>("file");
            scoreTextPosition = new Vector2(100, 100);
            welcomePosition = new Vector2(640 - MinecraftFont.MeasureString(welcomeText).X / 2, 100);
            gameOverPosition = new Vector2(640 - MinecraftFont.MeasureString(gameOverText).X / 2, 100);
                      
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            tangentbord = Keyboard.GetState();
            gammalMus = mus;
            mus = Mouse.GetState();

            // Menysystem (kod)

            switch(scene)
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

            // Menysystemet (Grafik)

            switch (scene)
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
            scene = nyscen;
        }

        public void UpdateMenu()
        {
            if (leftMouseClick() == true && buttonRectangle.Contains(mus.Position) == true)
            {
                switchMenu(1);
            }
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

        public void restartGame()
        {
            //Restarts 

            totemOfUndyingSpawnrate = 0;
            lives = 1;
            score = 0;
            eggSpawnRate = 180;
            slimeSpawnRate = 0;

            basketRectangle.Y = 550;
            basketRectangle.X = graphics.GraphicsDevice.Viewport.Width / 2 - basketRectangle.Width / 2;
    
            slimes.Clear();
            eggs.Clear();
            totemOfUndyings.Clear();
        }

        public void PointCounter()
        {
            scoreText = score.ToString();

            for (int i = 0; i < eggs.Count; i++)
            {
                if (eggs[i].Intersects(basketRectangle) == true)
                {
                    eggs.RemoveAt(i);
                    score++;
                    break;
                }
            }
        }

        public void UpdateGame()
        {
            PointCounter();

            // Spawning system

            if (eggSpawnRate == 180)
            {
                for (int i = 0; i < 3; i++)
                {
                    Rectangle eggSpawn = new Rectangle(random.Next(0, 1150), random.Next(-1000, -100), 128, 128);
                    eggs.Add(eggSpawn);
                }
                eggSpawnRate = 0;
            }
            eggSpawnRate++;

            if (slimeSpawnRate == 300)
            {
                for (int i = 0; i < 2; i++)
                {
                    Rectangle slimeSpawn = new Rectangle(random.Next(0, 1150), random.Next(-1000, -100), 96, 96);
                    slimes.Add(slimeSpawn);
                }
                slimeSpawnRate = 0;
            }
            slimeSpawnRate++;

            if (totemOfUndyingSpawnrate == 3000)
            {
                for (int i = 0; i < 1; i++)
                {
                    Rectangle totemSpawn = new Rectangle(random.Next(0, 1150), random.Next(-1000, -100), 128, 128);
                    totemOfUndyings.Add(totemSpawn);
                }
                totemOfUndyingSpawnrate = 0;
            }
            totemOfUndyingSpawnrate++;

            // Gives the spawned objects a velocity

            for (int i = 0; i < eggs.Count; i++)
            {
                Rectangle egg = eggs[i];

                egg.Y += 3;
                eggs[i] = egg;
            }

            for (int i = 0; i < slimes.Count; i++)
            {
                Rectangle slime = slimes[i];

                slime.Y += 3;
                slimes[i] = slime;
            }

            for (int i = 0; i < totemOfUndyings.Count; i++)
            {
                Rectangle totem = totemOfUndyings[i];

                totem.Y += 3;
                totemOfUndyings[i] = totem;
            }

            // Makes sure no objects collide after spawning

            for (int i = 0; i < totemOfUndyings.Count; i++)
            {
                if (totemOfUndyings[i].Intersects(basketRectangle))
                {
                    totemOfUndyings.RemoveAt(i);
                    lives++;

                    break;
                }
            }

            for (int i = 0; i < slimes.Count; i++)
            {
                if (slimes[i].Intersects(basketRectangle) == true)
                {
                    slimes.RemoveAt(i);
                    lives--;

                    break;
                }
            }

            for (int y = 0; y < eggs.Count; y++)
            {
                for (int i = 0; i < eggs.Count; i++)
                {
                    if (eggs[i].Intersects(eggs[y]) && i != y)
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
                    if (eggs[i].Intersects(slimes[y]))
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
                    if (eggs[i].Intersects(totemOfUndyings[y]))
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
                    if (slimes[i].Intersects(totemOfUndyings[y]))
                    {
                        slimes.RemoveAt(i);

                        break;
                    }
                }
            }

            for (int y = 0; y < totemOfUndyings.Count; y++)
            {
                for (int i = 0; i < totemOfUndyings.Count; i++)
                {
                    if (totemOfUndyings[i].Intersects(totemOfUndyings[y]) == true && i != y)
                    {
                        totemOfUndyings.RemoveAt(i);

                        break;
                    }
                }
            }

            // Life system

            if (lives == 0)
            {
                switchMenu(2);
                restartGame();
            }

            if (lives > 3)
            {
                lives = 3;
            }

            // Smooth from side to side system used by the basket to travel smoothly from one side to the other

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
            if (leftMouseClick() == true && buttonRectangle2.Contains(mus.Position) == true)
            {
                switchMenu(0);
            }
            lives = 1;
        }

        public void DrawMenu()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bakrundsMenyBild, bakgrundsMenyPosition, Color.White);
            spriteBatch.DrawString(MinecraftFont, welcomeText, welcomePosition, Color.Brown);
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

            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(HP, new Rectangle(i * 50 + 100, 650, 50, 50), Color.White);
            }

            spriteBatch.DrawString(MinecraftFont, scoreText, scoreTextPosition, Color.White);
            spriteBatch.Draw(basketPicture, basketRectangle, Color.White);


            spriteBatch.End();
        }

        public void DrawGameOver()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bakrundsMenyBild, bakgrundsMenyPosition, Color.White);
            spriteBatch.DrawString(MinecraftFont, gameOverText, gameOverPosition, Color.Brown);
            spriteBatch.Draw(buttonPicture2, buttonRectangle2, Color.White);
            spriteBatch.End();
        }
    }
}
