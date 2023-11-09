using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace blobby_guys
{
    internal class scorepile
    {
        private static Texture2D m_txr;
        public int Score;
        private Vector2 m_pileLoc;

        public scorepile(Texture2D txr, int xpos, int ypos)
        {
            m_txr = txr;
            Score = 0;
            m_pileLoc = new Vector2(xpos, ypos);
        }

        public void DrawMe(SpriteBatch sb, Random RNG)
        {
            Vector2 tempLoc = m_pileLoc;

            for (int i = 1; i <= Score; i++)
            {
                sb.Draw(m_txr, tempLoc, Color.White);
                tempLoc.Y -= m_txr.Height;
                if (i % 10 == 0)
                {
                    tempLoc.Y = m_pileLoc.Y;
                    tempLoc.X += m_txr.Width / 2;
                }
            }
        }
    }
}
