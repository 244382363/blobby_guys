using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace blobby_guys
{
    class blobby
    {
        private Texture2D m_StandingSprite;

        public Rectangle CollisionRect;
        private Vector2 m_position;

        private float m_walkSpeed;
        private Vector2 m_velocity;

        public blobby(Texture2D standSprite, int xpos, int ypos)
        {
            m_StandingSprite = standSprite;

            CollisionRect = new Rectangle(xpos, ypos, standSprite.Width, standSprite.Height);
            m_position = new Vector2(xpos, ypos);

            m_walkSpeed = 5f;
            m_velocity = Vector2.Zero;

        }

        public void UpdateMe(GamePadState currPad, GamePadState oldPad,
            Rectangle screenSize,float gravity, int ground) 
        {
            //apply gravity
            if (CollisionRect.Bottom < ground)
            {
                if (m_velocity.Y < gravity * 15)
                    m_velocity.Y += gravity;
            }
            else
            {
                m_velocity.Y = 0;
                m_position.Y = ground - CollisionRect.Height;
            }

            //handle screen srap
            if (m_position.X + CollisionRect.Width < 0)
                m_position.X = screenSize.Width - 1;
            if (m_position.X > screenSize.Width)
                m_position.X = 1 - CollisionRect.Width;
            
            //handle player-controlled movement
            m_velocity.X = 0;
            if (currPad.ThumbSticks.Left.X > 0)
            {
                m_velocity.X = m_walkSpeed;
            }
            else if (currPad.ThumbSticks.Left.X < 0)
            {
                m_velocity.X = -m_walkSpeed;
            }

            //handle player jumping
            if ((currPad.Buttons.B == ButtonState.Pressed) && (oldPad.Buttons.B == ButtonState.Released) &&
                (m_velocity.Y == 0)) 
                 m_velocity.Y = -7;

            m_position += m_velocity;
            CollisionRect.X = (int)m_position.X;
            CollisionRect.Y = (int)m_position.Y;

        }

        public void DrawMe(SpriteBatch sb, GameTime gt)
        {
            sb.Draw(m_StandingSprite, m_position, null, Color.White);
        }
    }
}
