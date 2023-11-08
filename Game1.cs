using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace blobby_guys
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static readonly Random RNG = new Random();
        GamePadState currPad, oldPad;

        StaticGraphic background;
        List<FloatingPlatform> platforms;
        SpriteFont debugFont;
        SpinningCoin coin;
        baddie badguy;
        leaf leaf;
        blobby p1Char;


        leaf[] leaves;

        const int leafs = 64;
        const float GRAVITY = 0.3f;
        const int GROUNDLEVEL = 223;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 320;
            _graphics.PreferredBackBufferHeight = 240;
        }

        protected override void Initialize()
        {
            platforms = new List<FloatingPlatform>();
            leaves = new leaf[leafs];

            base.Initialize();
        }

        private void ResetCoin()
        {
            int chosenPlatform = RNG.Next(platforms.Count);

            coin.MoveTo(
                platforms[chosenPlatform].Surface.Center.X - 8,
                platforms[chosenPlatform].Surface.Top - 16);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            debugFont = Content.Load<SpriteFont>("debug");
            background = new StaticGraphic(Content.Load<Texture2D>("area_bkg"), 0, 0);
            coin = new SpinningCoin(Content.Load<Texture2D>("Spinning_coin_gold"), 100, 100,8,24);
            
            platforms = new List<FloatingPlatform>();
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 10, 70));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 30, 140));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 50, 185));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 75, 150));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 85, 100));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 140, 80));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 150, 120));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 190, 90));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 240, 60));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 200, 155));
            platforms.Add(new FloatingPlatform(Content.Load<Texture2D>("platform"), 250, 150));
            ResetCoin();
            for (int i = 0; i < leaves.Length; i++)
            {
                leaves[i] = new leaf(Content.Load<Texture2D>("tinyleaf"),320 , 240);
            }

            badguy = new baddie(Content.Load<Texture2D>("baddieball"),
                _graphics.PreferredBackBufferWidth - 32, 100, 0.8f);
            p1Char = new blobby(Content.Load<Texture2D>("snipe_stand_right"),
                Content.Load <Texture2D>("snipe_jump_right"),
                Content.Load <Texture2D>("snipe_run_right"),
                0, 100, 24);
        }

        protected override void Update(GameTime gameTime)
        {
            currPad = GamePad.GetState(PlayerIndex.One);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < leaves.Length; i++)
            {
                leaves[i].UpdateMe(320, 240);
            }
            badguy.UpdateMe(coin);

            if (badguy.CollisionRect.Intersects(coin.CollisionRect))
            {
                ResetCoin();
            }

            if (p1Char.CollisionRect.Intersects(coin.CollisionRect))
            {
                ResetCoin();
            }

            p1Char.UpdateMe(currPad, oldPad, GraphicsDevice.Viewport.Bounds,GRAVITY,GROUNDLEVEL,platforms);



                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            background.DrawMe(_spriteBatch);
            for (int i = 0; i < platforms.Count; i++)
            {

                platforms[i].DrawMe(_spriteBatch);
                //_spriteBatch.DrawString(debugFont, "Plat: " + i, platforms[i].m_position, Color.White);
                //_spriteBatch.DrawString(debugFont, "Position: " + platforms[i].m_position, platforms[i].m_position, Color.White);
            }


            coin.DrawMe(_spriteBatch, gameTime);
            p1Char.DrawMe(_spriteBatch, gameTime);
            badguy.DrawMe(_spriteBatch);
            for (int i = 0; i < leaves.Length; i++)
            {

                leaves[i].DrawMe(_spriteBatch);
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}