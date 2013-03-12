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
    class GameObject
    {
        public Vector2 position;
        public Texture2D texture;
        public Vector2 origin;
        public Vector2 scale;
        public SpriteEffects spriteEffect;
        public bool removeBool;

        public GameObject()
        {

        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            scale = Vector2.One;
            spriteEffect = SpriteEffects.None;           
        }

        public GameObject(Texture2D texture, Vector2 position, Vector2 drawScale)
        {
            this.texture = texture;
            this.position = position;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            scale = Vector2.One;
            spriteEffect = SpriteEffects.None;
            scale = drawScale;
        }

        public GameObject(Texture2D texture, Vector2 position, Vector2 drawScale, bool removable)
        {
            this.texture = texture;
            this.position = position;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            scale = Vector2.One;
            spriteEffect = SpriteEffects.None;
            scale = drawScale;
            removeBool = removable;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {            
           spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, spriteEffect, position.Y / TextureStorage.screenHeight);
            
        }
    }
}
