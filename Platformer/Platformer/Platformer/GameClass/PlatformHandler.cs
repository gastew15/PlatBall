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
        private int maxNumberOfPlatforms = 8;
        private Platform[] platforms;
        private Vector2[] platformPositions;
        private double platformSpeed = 200; //pixels a second
        private Vector2 platformSize = new Vector2(150, 40); //width, height
        private int yUpperLimit = 500;
        private int yLowerLimit = 100;
        private int xUpperLimit = 1080;
        private int xLowerLimit = 1280;

        public PlatformHandler()
        {

        }

        public void Initilize()
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
                platforms[i] = new Platform(platformTexture, platformSize, Color.Red);

                if(i < 3)
                    platformPositions[i] = new Vector2(200 + (platformSize.X * i), 300);
                else
                    platformPositions[i] = new Vector2(1280 + (platformSize.X * i + rand.Next(40, 100)), GeneratePositionY(yLowerLimit, (int)platformSize.Y, yUpperLimit));
            }
        }

        public void Update(GameTime gameTime, Vector2 drawScale)
        {
            this.drawScale = drawScale;
            Random rand = new Random();

            for (int i = 0; i < platforms.Length; i++)
            {
                if (drawScale.X * (platformPositions[i].X + platformSize.X) < 0)
                {
                    platformPositions[i] = new Vector2(1280 + ( rand.Next(40, 100)), GeneratePositionY(yLowerLimit, (int)platformSize.Y, yUpperLimit));
                    platforms[i].Update(gameTime, platformPositions[i]);

                    for(int j = 0; j < platforms.Length; j++)
                    {
                        if (i != j && platforms[i].isCollision(platforms[j].getRectangle()))
                        {
                            if (platforms[i].getPosition().X < platforms[j].getPosition().X)
                                platformPositions[i].X += 100;
                            else if (platforms[i].getPosition().X > platforms[j].getPosition().X)
                                platformPositions[i].X -= 100;//rand.Next((int)platforms[j].getPosition().X - (int)platforms[i].getPosition().X + 20, (int)platformSize.X + 20);

                            if (platforms[i].getPosition().Y < platforms[j].getPosition().Y)
                                platformPositions[i].Y += 100;
                            else if (platforms[i].getPosition().Y > platforms[j].getPosition().Y)
                                platformPositions[i].Y -= 100;
                            platforms[i].Update(gameTime, platformPositions[i]);
                        }
                    }
                }

                 double moveDistance = Math.Round((platformSpeed / 1000) * gameTime.ElapsedGameTime.Milliseconds);
                 platformPositions[i] = new Vector2((float)(platformPositions[i].X - moveDistance), platformPositions[i].Y);

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
