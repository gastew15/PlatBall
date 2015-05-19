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
        private int maxNumberOfPlatforms = 6;
        private int numberOfStartPlatforms = 3;
        private List<Platform> platforms;
        private List<Vector2> platformPositions;
        private double platformSpeed = 150; //pixels a second
        private Vector2 platformSize = new Vector2(150, 40); //width, height
        private Vector2 screenSize = new Vector2(1080, 720); //Temp

        public PlatformHandler()
        {

        }

        public void Initialize()
        {
            platforms = new List<Platform>();
            platformPositions = new List<Vector2>();
        }

        public void LoadContent(ContentManager Content)
        {
            platformTexture = Content.Load<Texture2D>("Textures/BlankTexture");
            Random rand = new Random();

            for (int i = 0; i < numberOfStartPlatforms; i++)
            {
                platformPositions.Add(new Vector2(200 + (i * 200), 300));
                platforms.Add(new Platform(platformTexture, platformSize, Color.Red));
            }

            if (maxNumberOfPlatforms > numberOfStartPlatforms)
            {
                //Generate other platforms using random junk generator
                for (int i = 0; i <= maxNumberOfPlatforms - numberOfStartPlatforms; i++)
                {
                    platformPositions.Add(new Vector2(platformPositions[0].X -(i * 200), 300));
                    platforms.Add(new Platform(platformTexture, platformSize, Color.Red));
                }
            }
        }

        public void Update(GameTime gameTime, Vector2 drawScale)
        {
            this.drawScale = drawScale;
            Random rand = new Random();

            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Update(gameTime, new Vector2(platformPositions[i].X * drawScale.X, platformPositions[i].Y * drawScale.Y));
                platformPositions[i] = new Vector2(platformPositions[i].X + (float)(platformSpeed * gameTime.ElapsedGameTime.TotalSeconds), platformPositions[i].Y);

                //Test
                if (platformPositions[i].X * drawScale.X > screenSize.X)
                {
                    //Replace with random generated
                    //platformPositions[i] = new Vector2(-platformSize.X - 50, 300);
                    if (i > 0)
                        platformPositions[i] = randPlatformPos(new Rectangle((int)platformPositions[i - 1].X, (int)platformPositions[i - 1].Y, (int)platformSize.X, (int)platformSize.Y), 3, 4, screenSize, new Vector2(0, 100));
                    else
                        platformPositions[i] = randPlatformPos(new Rectangle((int)platformPositions[i].X, (int)platformPositions[i].Y, (int)platformSize.X, (int)platformSize.Y), 3, 4, screenSize, new Vector2(0, 100));
                }
            }
        }

        public Vector2 randPlatformPos(Rectangle lastPlatform, int maxPlatVarianceWidth, int maxPlatVarianceHeight, Vector2 screenSize, Vector2 boundSize)
        {
            Vector2 genPos = new Vector2();
            Random rand = new Random();
            int genNum;

            genNum = rand.Next(-maxPlatVarianceWidth * 3, maxPlatVarianceWidth * 3);
            genPos.X = -lastPlatform.Width - 50;//(genNum * lastPlatform.Width) + lastPlatform.Width;
            if(lastPlatform.Y < boundSize.Y + 50)
                genNum = rand.Next(0, maxPlatVarianceHeight * 3);
            else if(lastPlatform.Y > screenSize.Y - boundSize.Y - 50)
                genNum = rand.Next(-maxPlatVarianceHeight * 3, 0);
            else
                genNum = rand.Next(-maxPlatVarianceHeight * 3, maxPlatVarianceHeight * 3);
            genPos.Y = ((genNum / 3) * lastPlatform.Height) + lastPlatform.Y + lastPlatform.Height;

            if (genPos.Y + lastPlatform.Height > screenSize.Y - boundSize.Y)
                genPos.Y = screenSize.Y - boundSize.Y - lastPlatform.Height;
            else if (genPos.Y < boundSize.Y)
                genPos.Y = boundSize.Y;

            return genPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Draw(spriteBatch, drawScale);
            }
        }
    }
}
