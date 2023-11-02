using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace blobby_guys
{
    class baddie
    {
        private Texture2D m_txr;
        private Vector2 m_position;
        public Rectangle CollisionRect;
        private Vector2 m_velocity;

        private float m_baseSpeed;
        private const int COLLISIONBORDER = 4;

        public baddie(Texture2D txr, int xpos, int ypos, float baseSpeed)
        {
            m_txr = txr;
            m_position = new Vector2(xpos, ypos);

            m_baseSpeed = baseSpeed;
            m_velocity = Vector2.Zero;
            //CollisionRect = new Rectangle(xpos, ypos, txr.Width, txr.Height);
            CollisionRect = new Rectangle(xpos + COLLISIONBORDER, ypos + COLLISIONBORDER,
                txr.Width - (COLLISIONBORDER * 2), txr.Height - (COLLISIONBORDER * 2));
        }

        public void UpdateMe(SpinningCoin target)
        {
            /*CollisionRect.X = (int)m_position.X;
            CollisionRect.Y = (int)m_position.Y;*/

            CollisionRect.X = (int)m_position.X + COLLISIONBORDER;
            CollisionRect.Y = (int)m_position.Y + COLLISIONBORDER;

            //simple way but more coding
            /*if (this.CollisionRect.Center.Y > target.CollisionRect.Center.Y)
            {
                m_position.Y -= m_baseSpeed;
            }
            else
            {
                m_position.Y += m_baseSpeed;
            }

            if (this.CollisionRect.Center.X > target.CollisionRect.Center.X)
            {
                m_position.X -= m_baseSpeed;
            }
            else
            {
                m_position.X += m_baseSpeed;
            }*/

            //less code but more elegent
            //calculate the unit vector of the line between this and it's target
            m_velocity.X = target.CollisionRect.Center.X - this.CollisionRect.Center.X;
            m_velocity.Y = target.CollisionRect.Center.Y - this.CollisionRect.Center.Y;
            m_velocity.Normalize();

            // multiply the unit vector by the baddie's speed
            m_velocity *= m_baseSpeed;

            //add it's velocity onto it's position
            m_position += m_velocity;
        }
        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_txr, m_position, Color.White);
        }
    }
}
