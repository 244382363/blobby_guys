using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace blobby_guys
{

    

    class blobby

    {
        enum AnimState
        {
            WalkingRight,
            WalkingLeft,
            AirborneRight,
            AirborneLeft,
            FacingRight,
            FacingLeft
        }
        private AnimState m_currState;

        private Texture2D m_StandingSprite;
        private Texture2D m_runningSpriteSheet;
        private Texture2D m_JumpingSprite;

        public Rectangle CollisionRect;
        private Rectangle m_feetPos;
        private Rectangle m_animCell;
        private Vector2 m_position;

        private float m_walkSpeed;
        private float m_frameTimer;
        private float m_fps;
        private const int FOOTMARGIN = 3;
        private Vector2 m_velocity;

        public blobby(Texture2D standSprite, Texture2D jumpSprite, Texture2D runSheet, int xpos, int ypos, int fps)
        {
            m_StandingSprite = standSprite;
            m_runningSpriteSheet = runSheet;
            m_JumpingSprite = jumpSprite;

            m_animCell = new Rectangle(0, 0, standSprite.Width, standSprite.Height);
            m_frameTimer = 1;
            m_fps = fps;

            CollisionRect = new Rectangle(xpos, ypos, standSprite.Width, standSprite.Height);
            m_position = new Vector2(xpos, ypos);

            m_walkSpeed = 2f;
            m_velocity = Vector2.Zero;
            m_feetPos = new Rectangle(CollisionRect.X + FOOTMARGIN,
                CollisionRect.Y + CollisionRect.Height - 2,
                CollisionRect.Width - (FOOTMARGIN * 2),
                2);
            m_currState = AnimState.FacingRight;

        }

        public void UpdateMe(GamePadState currPad, GamePadState oldPad,
            Rectangle screenSize,float gravity, int ground, List<FloatingPlatform> plats) 
        {
            //apply gravity

            
           if (CollisionRect.Bottom < ground)
           {

                
                if (m_velocity.Y < gravity * 15)
                    m_velocity.Y += gravity;

                for (int i = 0; i < plats.Count; i++)
                
                  if (plats[i].Surface.Intersects(m_feetPos))
                  {
                      if (m_velocity.Y > 0)
                      {
                            m_velocity.Y = 0;
                            m_position.Y = plats[i].Surface.Top -
                                CollisionRect.Height + 1;
                      }
                  }
                
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
                if (m_velocity.Y != 0)
                    m_currState = AnimState.AirborneRight;
                else
                    m_currState = AnimState.WalkingRight;
                m_velocity.X = m_walkSpeed;
            }
            else if (currPad.ThumbSticks.Left.X < 0)
            {
                if (m_velocity.Y != 0)
                    m_currState = AnimState.AirborneLeft;
                else
                    m_currState = AnimState.WalkingLeft;
                m_velocity.X = -m_walkSpeed;
            }
            else if ((m_currState == AnimState.WalkingLeft) || (m_currState == AnimState.AirborneLeft))
                m_currState = AnimState.FacingLeft;
            else if ((m_currState == AnimState.WalkingRight) || (m_currState == AnimState.AirborneRight))
                m_currState = AnimState.FacingRight;

            //handle player jumping
            if ((currPad.Buttons.B == ButtonState.Pressed) && (oldPad.Buttons.B == ButtonState.Released) &&
                (m_velocity.Y == 0)) 
                 m_velocity.Y = -7;

            m_position += m_velocity;
            CollisionRect.X = (int)m_position.X;
            CollisionRect.Y = (int)m_position.Y;

            m_feetPos.X = CollisionRect.X + FOOTMARGIN;
            m_feetPos.Y = CollisionRect.Y + CollisionRect.Height - 2;

        }

        public void DrawMe(SpriteBatch sb, GameTime gt)
        {
            switch (m_currState)
            {
                case AnimState.WalkingRight:
                    sb.Draw(m_runningSpriteSheet, m_position, m_animCell, Color.White);
                    break;
                case AnimState.WalkingLeft:
                    sb.Draw(m_runningSpriteSheet, m_position, m_animCell, Color.White,
                        0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally,0);
                    break;
                case AnimState.FacingRight:
                    sb.Draw(m_StandingSprite, m_position, null, Color.White);
                    break;
                case AnimState.FacingLeft:
                    sb.Draw(m_StandingSprite, m_position,null, Color.White,
                        0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally,0);
                    break;
                case AnimState.AirborneRight:
                    sb.Draw(m_JumpingSprite, m_position, null, Color.White);
                    break;
                case AnimState.AirborneLeft:
                    sb.Draw(m_JumpingSprite, m_position,null, Color.White,
                        0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally,0);
                    break;


            }

            if (m_frameTimer <= 0)
            {
                m_animCell.X = (m_animCell.X + m_animCell.Width);
                if (m_animCell.X >= m_runningSpriteSheet.Width)
                    m_animCell.X = 0;

                m_frameTimer = 1;
            }
            else
            {
                m_frameTimer -= (float)gt.ElapsedGameTime.TotalSeconds * m_fps;
            }
                
        }
    }
}
