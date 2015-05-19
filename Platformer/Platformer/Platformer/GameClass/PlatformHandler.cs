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
                int screenWidth = 1080;
                if (platformPositions[i].X * drawScale.X > screenWidth)
                {
                    //Replace with random generated
                    platformPositions[i] = new Vector2(-platformSize.X - 50, 300);
                }
            }
        }

        public Vector2 randPlatformPos(Rectangle lastPlatform, int maxPlatVarianceWidth, int maxPlatVarianceHeight)
        {
            Vector2 genPos = new Vector2();
            Random rand = new Random();
            int genNum;

            genNum = rand.Next(-maxPlatVarianceWidth, maxPlatVarianceWidth);
            genPos.X = (genNum * lastPlatform.Width) + lastPlatform.Width;
            genPos.X = genPos.X - (1080 - genPos.X);
            genNum = rand.Next(-maxPlatVarianceHeight, maxPlatVarianceHeight);
            genPos.Y = (genNum * lastPlatform.Height) + lastPlatform.Y + lastPlatform.Height;

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
