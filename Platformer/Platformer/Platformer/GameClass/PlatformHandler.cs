using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer.GameClass
{
    class PlatformHandler
    {
        private Vector2 drawScale;
        private Texture2D platformTexture;
        private int maxNumberOfPlatforms = 3;
        private Platform[] platforms;
        private Vector2[] platformPositions;
        private double platformSpeed = 200; //pixels a second
        private Vector2 platformSize = new Vector2(150, 40); //width, height

        public PlatformHandler()
        {

        }

        public void Initialize()
        {
            platforms = new Platform[maxNumberOfPlatforms];
            platformPositions = new Vector2[maxNumberOfPlatforms];
        }

        public void LoadContent(ContentManager Content)
        {
            platformTexture = Content.Load<Texture2D>("Textures/BlankTexture");
            Random rand = new Random();

            for (int i = 0; i < platforms.Length; i++)
            {
                platformPositions[i] = new Vector2(200 + (i * 200), 300);
                platforms[i] = new Platform(platformTexture, platformSize, Color.Red);
            }
        }

        public void Update(GameTime gameTime, Vector2 drawScale)
        {
            this.drawScale = drawScale;
            Random rand = new Random();

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].Update(gameTime, platformPositions[i]);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].Draw(spriteBatch, drawScale);
            }
        }

        private int GeneratePositionY(int yLowerPosition, int yUpperPosition, int yVariationLimit)
        {
            Random random = new Random();

            return yLowerPosition + yUpperPosition + random.Next(0, yVariationLimit);
        }

        private int GeneratePositionX(int xLowerPosition, int xUpperPosition, int xVariationLimit)
        {
            Random random = new Random();

            return xLowerPosition + xUpperPosition + random.Next(0, xVariationLimit);
        }
    }
}
