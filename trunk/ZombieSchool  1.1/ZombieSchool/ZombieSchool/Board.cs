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

using System.IO;

/*Pathfinding should be a static class which takes a grid, start and end node, and returns a list of nodes which is the path.
/Player class should have a path list which does the following:
 * Lerps towards the "next" node constantly at a speed dependent on the previous node's distanceTo property
 * Pops nodes from the list after arriving at each one
 * Supports total replacement of an existing list (but keeps the last destination node in memory and adds it to the beginning of the new list)
*/

namespace ZombieSchool
{
    class Board
    {
        public static Texture2D startTex;
        public static Texture2D endTex;
        public static Texture2D visitTex;
        public static Texture2D pathTex;
        public static Texture2D blankTex;
        public static Texture2D wallTex;
        public List<Node> nodes;
        public int rows;
        public int columns;
        public int start;
        public int end;
        public Player player;

        public Board()
        {
            /*startTex = content.Load<Texture2D>("StartNode");
            endTex = content.Load<Texture2D>("EndNode");
            visitTex = content.Load<Texture2D>("VisitedNode");
            pathTex = content.Load<Texture2D>("PathNode");
            blankTex = content.Load<Texture2D>("BlankNode");
            wallTex = content.Load<Texture2D>("WallNode");*/
            startTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.path];
            endTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.end];
            visitTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.dot];
            pathTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.path];
            blankTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.dot];
            wallTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.wall];
            nodes = new List<Node>();
            rows = TextureStorage.screenHeight / 16;
            columns = TextureStorage.screenWidth / 16;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    nodes.Add(new Node(blankTex, new Vector2(x * 16 + 16 / 2, y * 16 + 16 / 2), this));
                    nodes[nodes.Count - 1].index = x + y * columns;
                    nodes[nodes.Count - 1].hitbox = new Rectangle(-8, -8, 16, 16);
                }
            }

            SetAdjacencies();

            //nodes[2 + 2 * columns].texture = visitTex;
            //Random rand = new Random();
            //start = rand.Next(0, nodes.Count);
            start = nodes[25 * columns + 20].index;
            end = start;
            //end = rand.Next(0, nodes.Count);
            //Console.WriteLine("Pathfinding from " + nodes[start].position / 16 + " to " + nodes[end].position / 16);
            //Dijkstra(nodes[start], nodes[end]);
            //AStar(nodes[start], nodes[end]);
            player = new Player(TextureStorage.textures[(int)TextureStorage.TEXNAMES.player], 10, this, nodes[start]);
            player.scale = new Vector2(0.1f, 0.1f);
            player.origin = new Vector2(player.texture.Width / 2, player.texture.Height - 100);
            LoadFromFile("Room1.txt");
        }

        public Board(String textfile)
        {
            /*startTex = content.Load<Texture2D>("StartNode");
            endTex = content.Load<Texture2D>("EndNode");
            visitTex = content.Load<Texture2D>("VisitedNode");
            pathTex = content.Load<Texture2D>("PathNode");
            blankTex = content.Load<Texture2D>("BlankNode");
            wallTex = content.Load<Texture2D>("WallNode");*/
            startTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.path];
            endTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.end];
            visitTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.dot];
            pathTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.path];
            blankTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.dot];
            wallTex = TextureStorage.textures[(int)TextureStorage.TEXNAMES.wall];
            nodes = new List<Node>();
            rows = TextureStorage.screenHeight / 16;
            columns = TextureStorage.screenWidth / 16;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    nodes.Add(new Node(blankTex, new Vector2(x * 16 + 16 / 2, y * 16 + 16 / 2), this));
                    nodes[nodes.Count - 1].index = x + y * columns;
                    nodes[nodes.Count - 1].hitbox = new Rectangle(-8, -8, 16, 16);
                }
            }

            SetAdjacencies();

            //nodes[2 + 2 * columns].texture = visitTex;
            //Random rand = new Random();
            //start = rand.Next(0, nodes.Count);
            start = nodes[25 * columns + 20].index;
            end = start;
            //end = rand.Next(0, nodes.Count);
            //Console.WriteLine("Pathfinding from " + nodes[start].position / 16 + " to " + nodes[end].position / 16);
            //Dijkstra(nodes[start], nodes[end]);
            //AStar(nodes[start], nodes[end]);
            //player = new Player(TextureStorage.textures[(int)TextureStorage.TEXNAMES.player], 10, this, nodes[start]);
            AnimatedSprite playerSprite = new AnimatedSprite(TextureStorage.textures[(int)TextureStorage.TEXNAMES.walkanimation], Vector2.Zero, new Point(6, 1), new Point(500, 537), 0.033f, false);
            playerSprite.origin = new Vector2(playerSprite.origin.X, playerSprite.frameSize.Y);
            playerSprite.scale = new Vector2(0.5f, 0.5f);
            player = new Player(playerSprite, 10, this, nodes[start]);

            player.scale = new Vector2(0.1f, 0.1f);
            player.origin = new Vector2(player.texture.Width / 2, player.texture.Height - 100);
            LoadFromFile(textfile);
        }

        public void AStar(Node start, Node end)
        {
            foreach (Node n in nodes)
            {
                n.from = null;
                n.distanceFrom = 0;
                n.gScore = 0;
                n.fScore = 0;
            }

            List<Node> closedList = new List<Node>();
            List<Node> openList = new List<Node>();
            openList.Add(start);
            start.gScore = 0;
            start.fScore = start.gScore + Vector2.Distance(start.position / 16, end.position / 16);
            Node currentNode = start;

            /*Console.WriteLine(Vector2.Distance(start.position / 16, end.position / 16));
            foreach (Node n in start.adjacentNodes)
            {
                Console.WriteLine(Vector2.Distance(n.position / 16, end.position / 16));
            }*/

            while (openList.Count > 0)
            {
                currentNode = openList[0];
                foreach (Node n in openList) //Set currentNode to the node in openList with the lowest F-score.
                {
                    if (n.fScore < currentNode.fScore)
                        currentNode = n;
                }

                if (currentNode == end)
                    break;

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (Node n in currentNode.adjacentNodes)
                {
                    if (closedList.Contains(n))
                        continue;

                    if (!n.impassable && !openList.Contains(n) || currentNode.gScore + currentNode.distanceTo[currentNode.adjacentNodes.IndexOf(n)] < n.gScore)
                    {
                        n.from = currentNode;
                        n.distanceFrom = currentNode.distanceTo[currentNode.adjacentNodes.IndexOf(n)];
                        n.gScore = currentNode.gScore + n.distanceFrom;
                        n.fScore = n.gScore + Vector2.Distance(n.position / 16, end.position / 16);

                        if (!openList.Contains(n))
                        {
                            openList.Add(n);
                            n.texture = visitTex;
                        }
                    }
                }
            }

            if (end.from != null)
            {
                Node reconstruct = end.from;
                List<Node> playerPath = new List<Node>();
                playerPath.Add(end);
                playerPath.Add(reconstruct);
                while (reconstruct != start)
                {
                    reconstruct.texture = pathTex;
                    reconstruct = reconstruct.from;
                    playerPath.Add(reconstruct);
                }
                player.SetPath(playerPath);
                end.texture = endTex;
            }
            else
                end.unreachable = true;

            start.texture = startTex;
        }

        /*public void Dijkstra(Node start, Node end)
        {
            foreach (Node n in nodes)
            {
                n.cost = float.MaxValue;
                n.from = null;
                n.distanceFrom = 0;
            }

            Queue<Node> searchQueue = new Queue<Node>();
            Queue<Node> shortestPath = new Queue<Node>();
            Node currentNode = start;
            currentNode.cost = 0;

            bool success = false;

            while (!success)
            {
                for (int n = 0; n < currentNode.adjacentNodes.Count; n++)
                {
                    if (!currentNode.adjacentNodes[n].impassable && currentNode.cost + currentNode.distanceTo[n] < currentNode.adjacentNodes[n].cost)
                    {
                        currentNode.adjacentNodes[n].cost = currentNode.cost + currentNode.distanceTo[n];
                        searchQueue.Enqueue(currentNode.adjacentNodes[n]);
                        currentNode.adjacentNodes[n].from = currentNode;
                        currentNode.adjacentNodes[n].texture = visitTex;
                    }
                }

                searchQueue.Dequeue();
                currentNode = searchQueue.Peek();

                if (currentNode == end)
                {
                    success = true;

                    while (currentNode != start)
                    {
                        shortestPath.Enqueue(currentNode);
                        currentNode = currentNode.from;
                    }
                    shortestPath.Enqueue(start);
                }

                while (shortestPath.Count > 0)
                    shortestPath.Dequeue().texture = pathTex;

                end.texture = endTex;
                start.texture = startTex;
            }
        }*/

        public void SetDestination(Node destNode)
        {
            if (destNode.index != end)
            {
                start = player.GetNextNode().index;
                end = destNode.index;

                foreach (Node n in nodes)
                {
                    if (!n.impassable)
                        n.texture = blankTex;
                }

                AStar(nodes[start], nodes[end]);
            }
        }

        public bool LoadFromFile(string fileName)
        {
            columns = 0;
            string fileData = string.Empty;
            List<List<char>> roomData = new List<List<char>>();

            try
            {
                using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\Content\\" + fileName))
                {
                    while ((fileData = sr.ReadLine()) != null) //All other lines are treated as shape data.
                    {
                        if (fileData.Length > columns)
                            columns = fileData.Length;

                        List<char> line = new List<char>();
                        for (int i = 0; i < fileData.Length; i++)
                        {
                            char data = '0';
                            data = fileData[i];
                            line.Add(data);
                            //Console.Write(line[i] + " ");
                        }
                        // Console.WriteLine();

                        roomData.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            rows = roomData.Count;

            nodes = new List<Node>();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    nodes.Add(new Node(blankTex, new Vector2(x * 16 + 16 / 2, y * 16 + 16 / 2), this));
                    nodes[nodes.Count - 1].index = x + y * columns;
                    nodes[nodes.Count - 1].hitbox = new Rectangle(-8, -8, 16, 16);
                    if (roomData[y][x] == '1')
                    {
                        nodes[nodes.Count - 1].impassable = true;
                        nodes[nodes.Count - 1].texture = wallTex;
                    }
                }
            }

            SetAdjacencies();

            return true;
        }

        public void SetAdjacencies()
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (y != 0)
                    {
                        nodes[x + y * columns].adjacentNodes.Add(nodes[x + (y - 1) * columns]);
                        nodes[x + y * columns].distanceTo.Add(1.0f);
                        if (x != columns - 1)
                        {
                            nodes[x + y * columns].adjacentNodes.Add(nodes[x + 1 + (y - 1) * columns]);
                            nodes[x + y * columns].distanceTo.Add(1.4f);
                        }
                    }
                    if (x != columns - 1)
                    {
                        nodes[x + y * columns].adjacentNodes.Add(nodes[x + 1 + (y) * columns]);
                        nodes[x + y * columns].distanceTo.Add(1.0f);
                        if (y != rows - 1)
                        {
                            nodes[x + y * columns].adjacentNodes.Add(nodes[x + 1 + (y + 1) * columns]);
                            nodes[x + y * columns].distanceTo.Add(1.4f);
                        }
                    }
                    if (y != rows - 1)
                    {
                        nodes[x + y * columns].adjacentNodes.Add(nodes[x + (y + 1) * columns]);
                        nodes[x + y * columns].distanceTo.Add(1.0f);
                        if (x != 0)
                        {
                            nodes[x + y * columns].adjacentNodes.Add(nodes[x - 1 + (y + 1) * columns]);
                            nodes[x + y * columns].distanceTo.Add(1.4f);
                        }
                    }
                    if (x != 0)
                    {
                        nodes[x + y * columns].adjacentNodes.Add(nodes[x - 1 + (y) * columns]);
                        nodes[x + y * columns].distanceTo.Add(1.0f);
                        if (y != 0)
                        {
                            nodes[x + y * columns].adjacentNodes.Add(nodes[x - 1 + (y - 1) * columns]);
                            nodes[x + y * columns].distanceTo.Add(1.4f);
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Node n in nodes)
                n.Update(gameTime);

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Space))
            {
                foreach (Node n in nodes)
                {
                    if (!n.impassable)
                        n.texture = blankTex;
                }
                //Random rand = new Random();
                //start = rand.Next(0, nodes.Count);
                //end = rand.Next(0, nodes.Count);
                AStar(nodes[start], nodes[end]);
            }

            player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //foreach (Node n in nodes)
            //    n.Draw(spriteBatch);

            nodes[end].Draw(spriteBatch);

            player.Draw(spriteBatch);
        }
    }
}
