﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace blobby_guys
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        StaticGraphic background;
        List<FloatingPlatform> platforms;
        SpriteFont debugFont;
        SpinningCoin coin;


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
          

            base.Initialize();
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}