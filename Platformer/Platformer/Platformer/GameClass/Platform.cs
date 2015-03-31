using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer.GameClass
{
    class Platform
    {
        private Texture2D platformTexture;
        private Rectangle platformRect;
        private Color platformColor;

        public Platform(Texture2D platformTexture, Vector2 platformSize, Color platformColor)
        {
            this.platformTexture = platformTexture;
            this.platformColor = platformColor;
            platformRect.Width = (int)platformSize.X;
            platformRect.Height = (int)platformSize.Y;
        }

        public void Update(GameTime gameTime, Vector2 platformPosition)
        {
            platformRect.X = (int)platformPosition.X;
            platformRect.Y = (int)platformPosition.Y;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawScale)
        {
            spriteBatch.Draw(platformTexture, platformRect, platformColor);
        }

        public void setColor(Color color)
        {
            this.platformColor = color;
        }

        public void setTexture(Texture2D texture)
        {
            this.platformTexture = texture;
        }

        public void setSize(Vector2 size)
        {
            platformRect.Width = (int)size.X;
            platformRect.Height = (int)size.Y;
        }

        public Rectangle getRectangle()
        {
            return platformRect;
        }

        public Vector2 getPosition()
        {
            return new Vector2(platformRect.X, platformRect.Y);
        }

        public Boolean isCollision(Platform obj)
        {
            return platformRect.Intersects(obj.getRectangle());
        }
    }
}
