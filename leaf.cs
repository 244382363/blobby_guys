using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace blobby_guys
{
    class leaf
    {
        Texture2D _txr;

        Vector2 _pos;
        Vector2 _vel;

        float _rotation;
        float _rotationSpeed;

        public leaf(Texture2D txr, int maxX, int maxY)
        {
            _rotation = 0;
            _rotationSpeed = (float)(Game1.RNG.NextDouble() - 0.5f);
            _txr = txr;


            _pos = new Vector2(Game1.RNG.Next(0, maxX), Game1.RNG.Next(100, maxY));
            _vel = new Vector2((float)Game1.RNG.NextDouble() + 0.25f);

        }

        public void UpdateMe(int maxX, int maxY)
        {
            _pos = _pos + _vel;
            _rotation = _rotation + _rotationSpeed;
            if(_pos.X > maxX)
            {
                _pos = new Vector2(0,  Game1.RNG.Next(100,maxY));
                _vel = new Vector2((float)Game1.RNG.NextDouble() + 0.75f, 0.5f - (float)Game1.RNG.NextDouble());
                _rotationSpeed = ((float)(Game1.RNG.NextDouble() - 0.9f) / 6);
            }
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(_txr, _pos, null, Color.White * 0.75f,
               _rotation, Vector2.Zero, _vel.X,
               SpriteEffects.None, 0);
        }
    }
}
