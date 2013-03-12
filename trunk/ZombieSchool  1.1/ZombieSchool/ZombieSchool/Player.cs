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
    class Player : GameObject
    {
        public float speed;
        public Board board;
        private List<Node> pathList;
        private Node lastNode; //last node visited
        private float progress; //Progress from one node position to the next

        public AnimatedSprite sprite;

        public Player()
            : base()
        {
        }

        public Player(Texture2D texture, float speed, Board board, Node startNode)
            : base(texture, startNode.position)
        {
            this.speed = speed;
            this.board = board;
            pathList = new List<Node>();
            progress = 0;
            lastNode = startNode;
        }

        public Player(AnimatedSprite sprite, float speed, Board board, Node startNode)
            : base(TextureStorage.textures[(int)TextureStorage.TEXNAMES.blank], startNode.position)
        {
            this.speed = speed;
            this.board = board;
            this.sprite = sprite;
            pathList = new List<Node>();
            progress = 0;
            lastNode = startNode;
        }

        public void SetPath(List<Node> newList)
        {
            pathList = newList;
            if (lastNode == null)
                lastNode = pathList[pathList.Count - 1];
        }

        public Node GetNextNode()
        {
            if (pathList.Count > 0)
                return pathList[pathList.Count - 1];
            else
                return lastNode;
        }

        public override void Update(GameTime gameTime)
        {
            if (pathList.Count > 0)
            {
                if (position.X > pathList[pathList.Count - 1].position.X)
                    spriteEffect = SpriteEffects.None;
                else if (position.X < pathList[pathList.Count - 1].position.X)
                    spriteEffect = SpriteEffects.FlipHorizontally;

                if (sprite != null)
                {
                    if (position.X > pathList[pathList.Count - 1].position.X)
                        sprite.spriteEffect = SpriteEffects.FlipHorizontally;
                    else if (position.X < pathList[pathList.Count - 1].position.X)
                        sprite.spriteEffect = SpriteEffects.None;
                }

                if (lastNode == pathList[pathList.Count - 1])
                    progress = 1;

                progress += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (progress > 1)
                {
                    position = pathList[pathList.Count - 1].position;
                    progress--;
                    lastNode = pathList[pathList.Count - 1];
                    pathList.RemoveAt(pathList.Count - 1);
                }
                else
                {
                    position = Vector2.Lerp(lastNode.position, pathList[pathList.Count - 1].position, progress);
                }
            }

            if (sprite != null)
            {
                sprite.position = position;
                sprite.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            sprite.Draw(spriteBatch);
        }
    }
}
