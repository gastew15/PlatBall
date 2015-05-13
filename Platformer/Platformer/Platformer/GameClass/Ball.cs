using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer.GameClass
{
    class Ball
    {
        private Texture2D texture;
        private double radius, gravity, velocity;
        private Color color;
        private Rectangle boundingBox;
        private Vector2 position, drawScale;
        private double GravityTimer;

        public Ball(Vector2 position, double gravity, double velocity, Texture2D texture, double radius, Color color)
        {
            this.position = position;
            this.gravity = gravity;
            this.velocity = velocity;
            this.texture = texture;
            this.radius = radius;
            this.color = color;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, (int)(radius * 2), (int)(radius * 2));
        }

        public void Update(GameTime gameTime, Vector2 drawScale)
        {
            this.drawScale = drawScale;

            position.Y -= (float)(velocity * drawScale.Y);

            boundingBox = new Rectangle((int)position.X, (int)position.Y, (int)(radius * 2), (int)(radius * 2));
            velocity -= gravity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0f, new Vector2(), drawScale, SpriteEffects.None, 1f);
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void bounce(double velocity)
        {
            this.velocity = velocity;
        }

        public Boolean isCollision(Platform obj)
        {
            if (boundingBox.Intersects(obj.getRectangle()))
            {
                return true;

                //Add in checking for actual sphere shape here and if not valid then return false
                //return false;
            }

            return false;
        }
    }
}
