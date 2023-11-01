using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace blobby_guys
{
    class SpinningCoin
    {
        private Texture2D m_SpriteSheet;
        private Vector2 m_position;
        private Rectangle m_animCell;
        private float m_frameTimer;
        private float m_fps;
        public SpinningCoin(Texture2D spriteSheet, int xpos, int ypos, int frameCount, int fps)
        {
            m_SpriteSheet = spriteSheet;
            m_animCell = new Rectangle(0,0,spriteSheet.Width / frameCount,spriteSheet.Height);
            m_position = new Vector2(xpos, ypos); 
            m_frameTimer = 1;
            m_fps = fps;
        }

        public void DrawMe(SpriteBatch sb, GameTime gt)
        {
            /* m_animCell.X = (m_animCell.X + m_animCell.Width);
             if (m_animCell.X >= m_SpriteSheet.Width)
                 m_animCell.X = 0;*/
            //modify the drawme method of spinning coin
            if (m_frameTimer <= 0)
            {
                m_animCell.X = (m_animCell.X + m_animCell.Width);
                if (m_animCell.X >= m_SpriteSheet.Width) 
                    m_animCell.X = 0;
                
                m_frameTimer = 1;
            }

            else
            {
                m_frameTimer -=  (float)gt.ElapsedGameTime.TotalSeconds * m_fps;
            }

            sb.Draw(m_SpriteSheet,m_position, m_animCell, Color.White);
        }
    }

    
}
