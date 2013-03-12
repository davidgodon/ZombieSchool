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
    class TextureStorage
    {
        public static Texture2D[] textures;
        public static int screenWidth;
        public static int screenHeight;
        public static Rectangle screenRect;

        public enum TEXNAMES
        {
            blank,
            dot,
            player,
            path,
            end,
            puzzle,
            mathpuzzle01,
            mathpuzzleitem01,
            mathpuzzleitem02,
            mathpuzzleitem03,
            border,
            wall,
            placeholderzombie,
            placeholdermap,
            mathroom,
            library,
            doorBorder,
            walkanimation,
            groundsmoke,
            classroom,
            Jigsaw00,
            Jigsaw01,
            Jigsaw02,
            Jigsaw03,
            Jigsaw04,
            Jigsaw05,
            Jigsaw06,
            Jigsaw07,
            Jigsaw10,
            Jigsaw11,
            Jigsaw12,
            Jigsaw13,
            Jigsaw14,
            Jigsaw15,
            Jigsaw16,
            Jigsaw17,
            Jigsaw20,
            Jigsaw21,
            Jigsaw22,
            Jigsaw23,
            Jigsaw24,
            Jigsaw25,
            Jigsaw26,
            Jigsaw27,
            Jigsaw30,
            Jigsaw31,
            Jigsaw32,
            Jigsaw33,
            Jigsaw34,
            Jigsaw35,
            Jigsaw36,
            Jigsaw37,
            Jigsaw40,
            Jigsaw41,
            Jigsaw42,
            Jigsaw43,
            Jigsaw44,
            Jigsaw45,
            Jigsaw46,
            Jigsaw47,
            Paddle,
            Brain
            

        }

        public TextureStorage(ContentManager content, int width, int height)
        {
            textures = new Texture2D[62];
            textures[0] = content.Load<Texture2D>("Blank");
            textures[1] = content.Load<Texture2D>("Dot");
            textures[2] = content.Load<Texture2D>("Kid");
            textures[3] = content.Load<Texture2D>("Path");
            textures[4] = content.Load<Texture2D>("End");
            textures[5] = content.Load<Texture2D>("puzzle");
            textures[6] = content.Load<Texture2D>("Puzzle01");
            textures[7] = content.Load<Texture2D>("Puzzle01Item01");
            textures[8] = content.Load<Texture2D>("Puzzle01Item02");
            textures[9] = content.Load<Texture2D>("Puzzle01Item03");
            textures[10] = content.Load<Texture2D>("border");
            textures[11] = content.Load<Texture2D>("Wall");
            textures[12] = content.Load<Texture2D>("PlaceholderZombie");
            textures[13] = content.Load<Texture2D>("PlaceholderMap");
            textures[14] = content.Load<Texture2D>("MathRoom");
            textures[15] = content.Load<Texture2D>("tempLibrary");
            textures[16] = content.Load<Texture2D>("DoorBorder");
            textures[17] = content.Load<Texture2D>("charactersprite");
            textures[18] = content.Load<Texture2D>("GroundSmoke");
            textures[19] = content.Load<Texture2D>("classroom");
            textures[20] = content.Load<Texture2D>("0-0");
            textures[21] = content.Load<Texture2D>("0-1");
            textures[22] = content.Load<Texture2D>("0-2");
            textures[23] = content.Load<Texture2D>("0-3");
            textures[24] = content.Load<Texture2D>("0-4");
            textures[25] = content.Load<Texture2D>("0-5");
            textures[26] = content.Load<Texture2D>("0-6");
            textures[27] = content.Load<Texture2D>("0-7");
            textures[28] = content.Load<Texture2D>("1-0");
            textures[29] = content.Load<Texture2D>("1-1");
            textures[30] = content.Load<Texture2D>("1-2");
            textures[31] = content.Load<Texture2D>("1-3");
            textures[32] = content.Load<Texture2D>("1-4");
            textures[33] = content.Load<Texture2D>("1-5");
            textures[34] = content.Load<Texture2D>("1-6");
            textures[35] = content.Load<Texture2D>("1-7");
            textures[36] = content.Load<Texture2D>("2-0");
            textures[37] = content.Load<Texture2D>("2-1");
            textures[38] = content.Load<Texture2D>("2-2");
            textures[39] = content.Load<Texture2D>("2-3");
            textures[40] = content.Load<Texture2D>("2-4");
            textures[41] = content.Load<Texture2D>("2-5");
            textures[42] = content.Load<Texture2D>("2-6");
            textures[43] = content.Load<Texture2D>("2-7");
            textures[44] = content.Load<Texture2D>("3-0");
            textures[45] = content.Load<Texture2D>("3-1");
            textures[46] = content.Load<Texture2D>("3-2");
            textures[47] = content.Load<Texture2D>("3-3");
            textures[48] = content.Load<Texture2D>("3-4");
            textures[49] = content.Load<Texture2D>("3-5");
            textures[50] = content.Load<Texture2D>("3-6");
            textures[51] = content.Load<Texture2D>("3-7");
            textures[52] = content.Load<Texture2D>("4-0");
            textures[53] = content.Load<Texture2D>("4-1");
            textures[54] = content.Load<Texture2D>("4-2");
            textures[55] = content.Load<Texture2D>("4-3");
            textures[56] = content.Load<Texture2D>("4-4");
            textures[57] = content.Load<Texture2D>("4-5");
            textures[58] = content.Load<Texture2D>("4-6");
            textures[59] = content.Load<Texture2D>("4-7");
            textures[60] = content.Load<Texture2D>("Paddle");
            textures[61] = content.Load<Texture2D>("Brain");
           



            screenWidth = width;
            screenHeight = height;
            screenRect = new Rectangle(0, 0, width, height);
        }

        //Explosion calls
        //MedExplosion: updateHandler.particles.Add(new AnimatedSprite(TextureStorage.textures[(int)TextureStorage.TEXNAMES.medexplosion], position, 0, true, ScrollingBackground.scrollVelocity, new Point(24, 24), new Point(0, 0), new Point(17, 1), new Vector2(2, 2), 0));
        //LargeExplosionFast: updateHandler.particles.Add(new AnimatedSprite(TextureStorage.textures[(int)TextureStorage.TEXNAMES.largeexplosionfast], position, 3, true, ScrollingBackground.scrollVelocity, new Point(80, 80), new Point(0, 0), new Point(4, 1), new Vector2(1, 1), 0));
        //SparksSmall: updateHandler.particles.Add(new AnimatedSprite(TextureStorage.textures[(int)TextureStorage.TEXNAMES.smallsparks], position, 2, true, ScrollingBackground.scrollVelocity, new Point(32, 32), new Point(0, 0), new Point(5, 1), new Vector2(1, 1), 0));
        //SparksLarge: updateHandler.particles.Add(new AnimatedSprite(TextureStorage.textures[(int)TextureStorage.TEXNAMES.largesparks], position, 2, true, ScrollingBackground.scrollVelocity, new Point(80, 80), new Point(0, 0), new Point(6, 1), new Vector2(1, 1), 0));
        //LargeWaterSplash: updateHandler.particles.Add(new AnimatedSprite(TextureStorage.textures[(int)TextureStorage.TEXNAMES.largewatersplash], position, 0, 0.5f, true, Vector2.Zero, new Point(204, 204), new Point(0, 0), new Point(7, 2), new Vector2(1, 1), 0));

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 a, Vector2 b, Color col)
        {
            Vector2 Origin = new Vector2(0.5f, 0.0f);
            Vector2 diff = b - a;
            float angle;
            Vector2 Scale = new Vector2(1.0f, diff.Length() / TextureStorage.textures[(int)TextureStorage.TEXNAMES.blank].Height);

            angle = (float)(Math.Atan2(diff.Y, diff.X)) - MathHelper.PiOver2;

            spriteBatch.Draw(TextureStorage.textures[(int)TextureStorage.TEXNAMES.blank], a, null, col, angle, Origin, Scale, SpriteEffects.None, 1.0f);
        }
    }
}