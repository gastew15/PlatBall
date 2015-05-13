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
        private Random colorRandom;
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
            blockColor = Color.Wheat;
            blockRect = new Rectangle((int)point1.X, (int)point1.Y, 5, 5);
        }
        public void LoadContent(ContentManager Content)
        {
            Random colorRandom = new Random();
            blockTexture = Content.Load<Texture2D>("Textures/SkyBlock");
            font1 = Content.Load<SpriteFont>("Fonts/Font1");
            blockRect = new Rectangle((int)point1.X, (int)point1.Y,blockTexture.Width, blockTexture.Height);
            blockColor = new Color((float)colorRandom.Next(255) / 255, (float)colorRandom.Next(255) / 255, (float)colorRandom.Next(255) / 255);
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


            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
            //    point2.Y = point2.Y - 4;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //{
            //    point2.Y = point2.Y + 4;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    point2.X = point2.X - 4;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    point2.X = point2.X + 4;
            //}
            point2 = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
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
            #region Comments / Explanation
            //This Code is for making sure the blocks follow the line between point1 and point2 at all angles
            //Instead of doing a simple distance kind of thing we have to do this so that the squares line up with eachother at all angles and dont collide like example 1

            /*
              ___     EXAMPLE 1              ___
             |   | ___                      |   |_
             |___||   | <---GOOD     BAD--->|___| |
                  |___|                       |___|
            


            
            |----------EXAMPLE 2--------------------|                                        
            |     Top Quadrant v                    |  The "O" represents point1,            
            | left quadrant -> O <- Right quadrant  |  and the quadrants are based on the    
            | Bottom Quadtrant ^                    |  angle between point1 and point2  
            |---------------------------------------|    
            
                        
            
            |EXAMPLE 3|
            | P1 O    |  In this example(3), the line of blocks would be
            |     \   |  somewhere in between the top and left quad
            |   P2 O  |
            |---------|
             

            //WARNING: this currently only works well with square blocks, rectangles work bad but can be used if you really want
            //this works with circles as well but they will use the same sort of linear alignment
            */
            #endregion
            //gets the angle between the two points and converts it to a 360 degree type thing so I can actually use it. This not efficient so should be replaced by something less terrible
            lineAngle = (float)Math.Atan2(slope.X, slope.Y) * (180f / 3.140f); 

            //Right quadrant of the angles, so point1's X value can be less than point2's                                                                  
            if (lineAngle <= 135 && lineAngle >= 45)        
            {
                straightVarY = (slope.X / slope.Y);
                straightVarX = 1;
                revVarX = 1;
                revVarY = 1;
            }
            //Bottom quadrant of the angles, so point1's Y value can be less than point2's
            else if (lineAngle < 45 && lineAngle > -45)
            {
                straightVarY = 1;
                straightVarX = (slope.Y / slope.X);
                revVarX = 1;
                revVarY = 1;
            }
            //Left quadrant of the angles, so point1's X value can be more than point2's
            else if (lineAngle <= -45 && lineAngle >= -135)
            {
                straightVarY = (slope.X / slope.Y);
                straightVarX = 1;
                revVarX = -1;
                revVarY = -1;
            }
            //Top quadrant of the angles, so point1's Y value can be more than point2's
            else if (lineAngle < -135 || lineAngle > 135)
            {
                revVarY = -1;
                revVarX = -1;
                straightVarY = 1;
                straightVarX = (slope.Y / slope.X);
            }
           
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Random colorRandom = new Random();
            for (int i = 0; i < Math.Abs(smallestDiff); i++)
            {
                
                
                //if ((float)Math.Abs(Math.Atan2(slope.X, slope.Y) * (180f / 3.140f)) <= 135 && (float)Math.Abs(Math.Atan2(slope.X, slope.Y) * (180f / 3.140f)) > 45)
                spriteBatch.Draw(blockTexture, 
                    new Vector2(point1.X + (((i * revVarX) * blockRect.Width) / (straightVarX)), point1.Y + (((i * revVarY) * blockRect.Height) / (straightVarY))),
                    new Rectangle(0, 0, blockRect.Width, blockRect.Height), 
                    new Color((float)colorRandom.Next(255) / 255, (float)colorRandom.Next(255) / 255, (float)colorRandom.Next(255) / 255),
                    //blockColor,
                    0f,
                    new Vector2(5, 5),
                    1f,
                    SpriteEffects.None,
                    0f);

                //spriteBatch.Draw(blockTexture, new Vector2(point1.X + (i * slope.X), point1.Y + (i * slope.Y)), new Rectangle(0, 0, 10, 10), blockColor, 0f, new Vector2(5, 5), 0f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(blockTexture, point2, new Rectangle(0, 0, blockRect.Width, blockRect.Height), Color.Orange, 0f, new Vector2(5, 5), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(blockTexture, point1, new Rectangle(0, 0, blockRect.Width, blockRect.Height), Color.Purple, 0f, new Vector2(5, 5), 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font1, lineAngle.ToString(), new Vector2(600, 600), Color.Black);
            spriteBatch.DrawString(font1, "slope:  " + (slope).ToString(), new Vector2(300, 600), Color.Black);
            spriteBatch.DrawString(font1, "block#:  " + ((int)smallestDiff).ToString(), new Vector2(100, 600), Color.Black);
            spriteBatch.DrawString(font1, (point1).ToString(), point1, Color.Black);
            spriteBatch.DrawString(font1, (point2).ToString(), point2, Color.Black);
        }
    }
}
