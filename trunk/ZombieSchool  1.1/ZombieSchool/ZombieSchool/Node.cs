using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ZombieSchool
{
    class Node
    {
        public Texture2D texture;
        public Vector2 position;
        public Board board;
        public List<Node> adjacentNodes; //All nodes that can be reached from this node in one step.
        public List<float> distanceTo; //The distances between this node and those nodes.
        public float cost;
        public Node from;
        public float distanceFrom;
        public float gScore;
        public float fScore;
        public int index;
        public bool impassable;
        public bool unreachable; //This is used to prevent repeat pathfinding to inaccessible areas.
        public Rectangle hitbox;

        public Node(Texture2D tex, Vector2 pos, Board board)
        {
            texture = tex;
            position = pos;
            this.board = board;
            adjacentNodes = new List<Node>();
            distanceTo = new List<float>();
            cost = 9999999999;
            impassable = false;
            unreachable = false;
            hitbox = new Rectangle((int)position.X - texture.Width / 2, (int)position.Y - texture.Height / 2, texture.Width, texture.Height);
        }

        public void UpdateBoardChange() //Call this for each node whenever the board changes structure.
        {
            unreachable = false;
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            if (!impassable && !unreachable)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Point pos = new Point(mouse.X, mouse.Y);
                    if (new Rectangle((int)(position.X + hitbox.X), (int)(position.Y + hitbox.Y), (int)hitbox.Width, (int)hitbox.Height).Contains(pos))
                    {
                        //impassable = false;
                        //texture = Board.endTex;
                        board.SetDestination(this);
                    }
                }
            }
            /*else if (mouse.RightButton == ButtonState.Pressed)
            {
                Point pos = new Point(mouse.X, mouse.Y);
                if (new Rectangle((int)(position.X + hitbox.X), (int)(position.Y + hitbox.Y), (int)hitbox.Width, (int)hitbox.Height).Contains(pos))
                {
                    impassable = true;
                    texture = Board.wallTex;
                }
            }*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //if (!impassable)
            spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), Vector2.One, SpriteEffects.None, 0.1f);
        }
    }
}
