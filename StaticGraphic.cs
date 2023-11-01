using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace blobby_guys
{
    class StaticGraphic
    {

        public Vector2 m_position;
        public Texture2D m_txr;

        public StaticGraphic(Texture2D txr, int xpos, int ypos)
        {
            m_position = new Vector2(xpos, ypos);
            m_txr = txr;
        }

        public void DrawMe(SpriteBatch sb) 
        {
            sb.Draw(m_txr, m_position, Color.White);
        }
    }
}
