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
    class BackgroundBlock
    {
        private SpriteFont font1;
        private Texture2D blockTexture;
        private Rectangle blockRect;
        private Color blockColor;
        public Vector2 point1, point2, slope;
        private float smallestDiff, lineAngle;
        private float straightVarX, straightVarY;
        private short revVarX, revVarY;
        public BackgroundBlock()
        {
            //this.blockTexture = blockTexture;
            //this.blockColor = blockColor;
            //blockRect.Width = (int)blockSize.X;
            //blockRect.Height = (int)blockSize.Y;
        }
        public void Initialize()
        {
            point1 = new Vector2(0, 0);
            point2 = new Vector2(200, 400);
            blockColor = Color.Green;
            blockRect = new Rectangle((int)point1.X, (int)point1.Y, 5, 5);
        }
        public void LoadContent(ContentManager Content)
        {
            blockTexture = Content.Load<Texture2D>("Textures/SkyBlock");
            font1 = Content.Load<SpriteFont>("Fonts/Font1");
            blockRect = new Rectangle((int)point1.X, (int)point1.Y, blockTexture.Width, blockTexture.Height);
        }
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                point1.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                point1.Y++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                point1.X--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                point1.X++;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                point2.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                point2.Y++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                point2.X--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                point2.X++;
            }

            slope = new Vector2((point2.X - point1.X), (point2.Y - point1.Y));
            if (Math.Abs(slope.X) > Math.Abs(slope.Y))
            {
                smallestDiff = slope.X / blockRect.Width;
            }
            else
            {
                
                smallestDiff = slope.Y / blockRect.Height;
            }
            
            
            #region Line Angle Code
            lineAngle = (float)Math.Atan2(slope.X, slope.Y) * (180f / 3.140f); 
            //---------------------------------------                                          ________
            //     Top Quadrant v                    |  The "O" represents point1,            | P1 O   |  In this example, the line of blocks would be
            // left quadrant -> O <- Right quadrant  |  and the quadrants are based on the    |     \  |  somewhere in between the top and left quad
            // Bottom Quadtrant ^                    |  angle between point1 and point2       |   P2 O |
            //---------------------------------------                                         ----------
            //Right quadrant of the angles, so point1's X value can be less than point2's                                                                  
            if (lineAngle <= 135 && lineAngle >= 45)        
            {
                straightVarY = (slope.X / slope.Y);
                straightVarX = 1;
                revVarX = 1;
            }
            //Bottom quadrant of the angles, so point1's Y value can be less than point2's
            else if (lineAngle < 45 && lineAngle > -45)
            {
                straightVarY = 1;
                straightVarX = (slope.Y / slope.X);
                revVarY = 1;
            }
            //Left quadrant of the angles, so point1's X value can be more than point2's
            else if (lineAngle <= -45 && lineAngle >= -135)
            {
                straightVarY = (slope.X / slope.Y);
                straightVarX = 1;
                revVarX = -1;
            }
            //Top quadrant of the angles, so point1's Y value can be more than point2's
            else if (lineAngle < -135 || lineAngle > 135)
            {
                revVarY = -1;
                straightVarY = 1;
                straightVarX = (slope.Y / slope.X);
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Math.Abs(smallestDiff); i++)
            {
                //if ((float)Math.Abs(Math.Atan2(slope.X, slope.Y) * (180f / 3.140f)) <= 135 && (float)Math.Abs(Math.Atan2(slope.X, slope.Y) * (180f / 3.140f)) > 45)
                spriteBatch.Draw(blockTexture, new Vector2(point1.X + (((i * revVarX) * blockRect.Width) / (straightVarX)), point1.Y + (((i * revVarY) * blockRect.Height) / (straightVarY))), new Rectangle(0, 0, blockRect.Width, blockRect.Height), blockColor, 0f, new Vector2(5, 5), 1f, SpriteEffects.None, 0f);

                //spriteBatch.Draw(blockTexture, new Vector2(point1.X + (i * slope.X), point1.Y + (i * slope.Y)), new Rectangle(0, 0, 10, 10), blockColor, 0f, new Vector2(5, 5), 0f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(blockTexture, point2, new Rectangle(0, 0, blockRect.Width, blockRect.Height), Color.Orange, 0f, new Vector2(5, 5), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(blockTexture, point1, new Rectangle(0, 0, blockRect.Width, blockRect.Height), Color.Purple, 0f, new Vector2(5, 5), 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font1, lineAngle.ToString(), new Vector2(600, 600), Color.Black);
            spriteBatch.DrawString(font1, "slope:  " + (slope).ToString(), new Vector2(300, 600), Color.Black);
            spriteBatch.DrawString(font1, (point1).ToString(), point1, Color.Black);
            spriteBatch.DrawString(font1, (point2).ToString(), point2, Color.Black);
        }
    }
}
