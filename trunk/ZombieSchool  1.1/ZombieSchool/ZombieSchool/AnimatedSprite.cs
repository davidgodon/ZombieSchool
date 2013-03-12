using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ZombieSchool
{
    class AnimatedSprite
    {
        public Texture2D texture;
        public Vector2 position;
        public float rotation;
        public Vector2 origin;
        public Vector2 scale;
        public SpriteEffects spriteEffect;
        public bool oneTime;
        public bool alive;
        public Point sheetSize; //Number of frames in the sheet.
        public Point frameSize; //Size in pixels of each frame.
        public Point currentFrame;
        public float frameDuration; //How long one frame should show on the screen.
        public float frameCounter; //Changing delay before the next frame switches.

        public AnimatedSprite(Texture2D texture, Vector2 position, Point sheetSize, Point frameSize, float frameDuration, bool oneTime)
        {
            this.texture = texture;
            this.position = position;
            this.sheetSize = sheetSize;
            this.frameSize = frameSize;
            this.frameDuration = frameDuration;
            this.oneTime = oneTime;
            frameCounter = 0;
            rotation = 0;
            origin = new Vector2(frameSize.X / 2, frameSize.Y / 2);
            scale = Vector2.One;
            spriteEffect = SpriteEffects.None;
            currentFrame = new Point(0, 0);
            alive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                if (frameCounter > 0)
                    frameCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                {
                    ++currentFrame.X;
                    frameCounter += frameDuration;

                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= sheetSize.Y)
                        {
                            currentFrame.Y = 0;
                        }
                    }

                    if (currentFrame.X == 0 && currentFrame.Y == 0 && oneTime == true)
                    {
                        alive = false;
                        currentFrame.X = sheetSize.X;
                        currentFrame.Y = sheetSize.Y;
                    }
                }

                //Any other update logic goes here.
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
                spriteBatch.Draw(texture, position, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, rotation, origin, scale, spriteEffect, position.Y / TextureStorage.screenHeight);
        }
    }
}