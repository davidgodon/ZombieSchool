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
    

    class Room
    {
        public List<GameObject> gameObjects;        
        public Texture2D backGround;
        public PuzzleScreen puzzleScreen;
        public InventoryScreen inventoryScreen;
        public Board board;
        public bool paused = false;        
        public Rectangle startRect;
        public List<Door> doorList;
        public Vector2 puzzleStartPoint;
        public List<AnimatedSprite> animations;

        MouseState currentMouseState;
        MouseState previousMouseState;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Room()
        {
            ChangeRoom(); 
        }
        
        public void ChangeRoom()
        {            
            ChangeGameObjects();
            ChangeTexture();
            ChangePuzzle();
            ChangeBoard();
            LoadInventory();

            doorList = RoomStats.doorList;
            startRect = RoomStats.startRect;
            puzzleStartPoint = RoomStats.puzzleStartPoint;
            animations = RoomStats.animations;
        }
        
        public void ChangePuzzle()
        {
            puzzleScreen = new PuzzleScreen();
        }

        public void LoadInventory()
        {
            inventoryScreen = RoomStats.inventoryScreen;
        }

        public void ChangeTexture()
        {
            backGround = RoomStats.backGround;
        }

        public void ChangeGameObjects()
        {
            gameObjects = RoomStats.gameObjects;
        }

        public void ChangeBoard()
        {
            board = RoomStats.board;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (backGround != null)
            {
                spriteBatch.Draw(backGround, new Rectangle(0, 0, TextureStorage.screenWidth, TextureStorage.screenHeight), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }

            if (startRect != Rectangle.Empty && startRect != null && puzzleScreen._isComplete != true)
            {
                spriteBatch.Draw(RoomStats.startRectTexture, startRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
            }

            if (puzzleScreen != null)
            {
                puzzleScreen.Draw(spriteBatch);
            }

            if (inventoryScreen != null)
            {
                inventoryScreen.Draw(spriteBatch);
            }

            if (puzzleScreen._isComplete != true)
            {
               
            }

            if (board != null)
            {
                board.Draw(spriteBatch);
            }

            if (doorList != null)
            {
                foreach (Door door in doorList)
                {
                    spriteBatch.Draw(TextureStorage.textures[(int)TextureStorage.TEXNAMES.doorBorder], door.doorRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
                }
            }

            if (gameObjects.Count > 0)
            {
                foreach (GameObject i in gameObjects)
                {
                    i.Draw(spriteBatch);
                }
            }

            if (animations.Count > 0)
            {
                for (int i = 0; i < animations.Count; i++)
                    animations[i].Draw(spriteBatch);
            }
        }


        public void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();

            if (!paused)
            {
                if (board != null)
                {
                    board.Update(gameTime);
                }


                if (animations.Count > 0)
                {
                    for (int i = 0; i < animations.Count; i++)
                    {
                        animations[i].Update(gameTime);

                        if (!animations[i].alive)
                        {
                            animations.RemoveAt(i);
                            if (i < animations.Count - 1)
                                i--;
                        }
                    }
                }
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed && !paused && currentMouseState != previousMouseState)
            {
                Point pos = new Point(currentMouseState.X, currentMouseState.Y);
                if(startRect.Contains(pos))
                {          
                    //in room stats, add a vector2 that indicates the position the player walks to
                    board.SetDestination(board.nodes[(int)puzzleStartPoint.Y * board.columns + (int)puzzleStartPoint.X]);

                    if ((board.player.position - board.nodes[(int)puzzleStartPoint.Y * board.columns + (int)puzzleStartPoint.X].position).Length() < 50)
                    {
                        if (puzzleScreen != null)
                        {
                            puzzleScreen.toggleVisible();
                        }
                    }
                }

                if (doorList != null)
                {
                    foreach (Door door in doorList)
                    {
                        if (door.doorRect.Contains(pos))
                        {
                            board.SetDestination(board.nodes[(int)door.doorEntrancePoint.Y * board.columns + (int)door.doorEntrancePoint.X]);
                            if ((board.player.position - board.nodes[(int)door.doorEntrancePoint.Y * board.columns + (int)door.doorEntrancePoint.X].position).Length() < 50)
                            {
                                RoomStats.LoadRoom(door.nextRoom);
                                ChangeRoom();
                            }
                        }
                    }
                }
            }

            MouseControls();
            

            if (puzzleScreen != null)
            {
                if (puzzleScreen._isVisible == false)
                {
                    paused = false;
                }
                else
                {
                    paused = true;
                }
                
                puzzleScreen.Update(gameTime);
                
                if (inventoryScreen != null)
                {
                    inventoryScreen.Update(gameTime);
                }

                if (puzzleScreen._isComplete)
                {
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        if (gameObjects[i].removeBool == true)
                        {
                            animations.Add(new AnimatedSprite(TextureStorage.textures[(int)TextureStorage.TEXNAMES.groundsmoke], gameObjects[i].position, new Point(3, 3), new Point(243, 152), 0.033f, true));
                            gameObjects.RemoveAt(i);

                            if (i < gameObjects.Count - 1)
                                i--;
                        }
                    }
                }
            }

            
            if (gameObjects.Count > 0)
            {
                foreach(GameObject i in gameObjects)
                {
                    i.Update(gameTime);                    
                }
            }

            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;

        }


        public void MouseControls()
        {
            //if (puzzleScreen.GetType == "PointAndClick")
            {
                if (puzzleScreen._puzzleType == "Math" || puzzleScreen._puzzleType == "Jigsaw")
                {

                    if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    {
                        puzzleScreen.mouseToPuzzlePosition(currentMouseState.X, currentMouseState.Y);

                    }

                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        puzzleScreen.mouseToPuzzlePosition(currentMouseState.X, currentMouseState.Y);
                        puzzleScreen.moveItem(currentMouseState.X, currentMouseState.Y);
                    }


                    if (puzzleScreen._isMovingItem && currentMouseState.LeftButton == ButtonState.Released)
                    {
                        puzzleScreen.releasedItem();
                    }

                }
                else if (puzzleScreen._puzzleType == "Physics")
                {
                    if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    {
                        puzzleScreen.mouseToPuzzlePosition(currentMouseState.X, currentMouseState.Y);

                    }

                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        puzzleScreen.mouseToPuzzlePosition(currentMouseState.X, currentMouseState.Y);
                        puzzleScreen.moveItem(currentMouseState.X, currentMouseState.Y);
                    }


                    if (puzzleScreen._isMovingItem && currentMouseState.LeftButton == ButtonState.Released)
                    {
                        puzzleScreen.releasedItemPhysics(currentMouseState.X, currentMouseState.Y);
                    }



                }




                if (currentKeyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
                {
                    puzzleScreen.toggleVisible();
                }

                if (currentKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                {
                    inventoryScreen.toggleMoving();
                }
            }
        }

    }
}
