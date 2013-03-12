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
    class RoomStats
    {
        //things to load upon a new puzzle
        public static int numberOfItems;
        public static int numberOfSlots;
        public static List<Vector2> slotPositions;
        public static List<String> slotNames;
        //public static String[] slotNames;
        public static List<Texture2D> itemTextures;
        public static List<String> itemNames;
        //public static String[] itemNames;

        //things to load upon a new room
        public static Texture2D backGround;
        public static List<GameObject> gameObjects;
        public static List<Door> doorList;
        public static int numberOfDoors;
        public static List<Vector2> doorPositions;
        public static List<Rectangle> doorRectangles;
        public static Board board;
        public static InventoryScreen inventoryScreen;
        public static bool exists;
        public static Vector2 puzzleStartPoint;
        public static Rectangle startRect;
        public static Vector2 startRectStartPoint;
        public static Texture2D startRectTexture;
        public static List<AnimatedSprite> animations;
        public static string _puzzleType;
            

        public RoomStats()
        {
            _puzzleType = "Math";
            startRect = new Rectangle(120, 120, 100, 100);
            puzzleStartPoint = new Vector2(10, 20);
            exists = true;
            numberOfSlots = 4;
            numberOfItems = 3;
            //
            slotPositions = new List<Vector2>();
            slotPositions.Add(new Vector2(150, 210));
            slotPositions.Add(new Vector2(300, 210));
            slotPositions.Add(new Vector2(450, 210));
            slotPositions.Add(new Vector2(350, 100));
            //slot names go in the same order as the slot positions
            slotNames = new List<String>();
            slotNames.Add("");
            slotNames.Add("");
            slotNames.Add("");
            slotNames.Add("Five");            
            //item textures go in the same order as item names
            itemTextures = new List<Texture2D>();
            itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem02]);
            itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem01]);
            itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem03]);
            //item names go in the same order as textures
            itemNames = new List<String>();
            itemNames.Add("Five");
            itemNames.Add("Seven");
            itemNames.Add("Three");
            //
            backGround = TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathroom];
            //
            gameObjects = new List<GameObject>();
            gameObjects.Add(new GameObject(TextureStorage.textures[(int)TextureStorage.TEXNAMES.placeholderzombie], new Vector2(700, 300), new Vector2(0.45f, 0.45f), true));
            //
            board = new Board("Room1.txt");
            //
            inventoryScreen = new InventoryScreen();   
            //
            doorList = new List<Door>();
            doorList.Add(new Door(new Rectangle(700, 300, 50, 100), new Vector2(700, 400), "map", new Vector2(40, 25)));
            //
            startRectTexture = TextureStorage.textures[(int)TextureStorage.TEXNAMES.puzzle];

            animations = new List<AnimatedSprite>();
             
        }

        public static void LoadRoom(String roomName)
        {
            //JSON stuff goes here
            if (roomName == "map")
            {
                numberOfSlots = 0;
                numberOfItems = 0;
                slotPositions = new List<Vector2>();
                itemTextures = new List<Texture2D>();
                itemNames = null;
                board = new Board("Map.txt");
                backGround = TextureStorage.textures[(int)TextureStorage.TEXNAMES.placeholdermap];                
                inventoryScreen = new InventoryScreen();
                exists = false;
                doorList = new List<Door>();
                doorList.Add(new Door(new Rectangle(50, 50, 50, 100), new Vector2(700, 400), "mathroom", new Vector2(5, 5)));
                doorList.Add(new Door(new Rectangle(250, 50, 50, 100), new Vector2(700, 400), "jigsaw", new Vector2(15, 5)));
                doorList.Add(new Door(new Rectangle(450, 50, 50, 100), new Vector2(700, 400), "Physics", new Vector2(25, 5)));
                startRect = Rectangle.Empty;
                gameObjects = new List<GameObject>(); ;
                //everything else is null
            }

            if (roomName == "mathroom")
            {
                startRect = new Rectangle(120, 120, 100, 100);
                puzzleStartPoint = new Vector2(10, 20);
                startRectTexture = TextureStorage.textures[(int)TextureStorage.TEXNAMES.puzzle];
                exists = true;
                numberOfSlots = 4;
                numberOfItems = 3;
                //
                slotPositions = new List<Vector2>();
                //for( int i = 0; i < numerOfSlots; i++)
                slotPositions.Add(new Vector2(150, 210));
                slotPositions.Add(new Vector2(300, 210));
                slotPositions.Add(new Vector2(450, 210));
                slotPositions.Add(new Vector2(350, 100));
                //slot names go in the same order as the slot positions
                slotNames = new List<String>();
                slotNames.Add("");
                slotNames.Add("");
                slotNames.Add("");
                slotNames.Add("Five");
                //item textures go in the same order as item names
                itemTextures = new List<Texture2D>();
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem02]);
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem01]);
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem03]);
                //item names go in the same order as textures
                itemNames = new List<String>();
                itemNames.Add("Five");
                itemNames.Add("Seven");
                itemNames.Add("Three");
                //
                backGround = TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathroom];
                //
                gameObjects = new List<GameObject>();
                gameObjects.Add(new GameObject(TextureStorage.textures[(int)TextureStorage.TEXNAMES.placeholderzombie], new Vector2(700, 300), new Vector2(0.45f, 0.45f)));
                //
                board = new Board("Room1.txt");
                //
                inventoryScreen = new InventoryScreen();     
                //
                doorList = new List<Door>();
                doorList.Add(new Door(new Rectangle(700, 300, 50, 100), new Vector2(700, 400), "map", new Vector2(40, 25)));
            }

            if (roomName == "library")
            {
                //puzzle stuff
                startRect = new Rectangle(120, 120, 100, 100);
                puzzleStartPoint = new Vector2(10, 20);
                startRectTexture = TextureStorage.textures[(int)TextureStorage.TEXNAMES.puzzle];
                exists = true;
                numberOfSlots = 4;
                numberOfItems = 3;
                //
                slotPositions = new List<Vector2>();
                //for( int i = 0; i < numerOfSlots; i++)
                slotPositions.Add(new Vector2(150, 210));
                slotPositions.Add(new Vector2(300, 210));
                slotPositions.Add(new Vector2(450, 210));
                slotPositions.Add(new Vector2(350, 100));
                //slot names go in the same order as the slot positions
                slotNames = new List<String>();
                slotNames.Add("");
                slotNames.Add("");
                slotNames.Add("");
                slotNames.Add("Five");
                //item textures go in the same order as item names
                itemTextures = new List<Texture2D>();
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem02]);
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem01]);
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem03]);
                //item names go in the same order as textures
                itemNames = new List<String>();
                itemNames.Add("Five");
                itemNames.Add("Seven");
                itemNames.Add("Three");
                //////
                //room stuff
                backGround = TextureStorage.textures[(int)TextureStorage.TEXNAMES.library];
                gameObjects = new List<GameObject>();
                doorList = new List<Door>();
                doorList.Add(new Door(new Rectangle(400, 120, 50, 100), new Vector2(700, 400), "map", new Vector2(25, 15)));
                numberOfDoors = 1;
                doorPositions = new List<Vector2>();
                doorRectangles = new List<Rectangle>();
                board = new Board("Map.txt");
                inventoryScreen = null;
                exists = true;
               
            }

            if (roomName == "OtherRoom")
            {
                //puzzle stuff
                startRect = new Rectangle(120, 120, 100, 100);
                puzzleStartPoint = new Vector2(10, 20);
                startRectTexture = TextureStorage.textures[(int)TextureStorage.TEXNAMES.puzzle];
                exists = true;
                numberOfSlots = 4;
                numberOfItems = 3;
                //
                slotPositions = new List<Vector2>();
                //for( int i = 0; i < numerOfSlots; i++)
                slotPositions.Add(new Vector2(150, 210));
                slotPositions.Add(new Vector2(300, 210));
                slotPositions.Add(new Vector2(450, 210));
                slotPositions.Add(new Vector2(350, 100));
                //slot names go in the same order as the slot positions
                slotNames = new List<String>();
                slotNames.Add("");
                slotNames.Add("");
                slotNames.Add("");
                slotNames.Add("Five");
                //item textures go in the same order as item names
                itemTextures = new List<Texture2D>();
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem02]);
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem01]);
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzleitem03]);
                //item names go in the same order as textures
                itemNames = new List<String>();
                itemNames.Add("Five");
                itemNames.Add("Seven");
                itemNames.Add("Three");
                //////
                //room stuff
                backGround = TextureStorage.textures[(int)TextureStorage.TEXNAMES.classroom];
                gameObjects = new List<GameObject>();
                doorList = new List<Door>();
                doorList.Add(new Door(new Rectangle(700, 120, 50, 100), new Vector2(750, 400), "map", new Vector2(40, 12)));
                numberOfDoors = 1;
                doorPositions = new List<Vector2>();
                doorRectangles = new List<Rectangle>();
                board = new Board("Map.txt");
                inventoryScreen = null;
                exists = true;
            }


            if (roomName == "Physics")
            {
                startRect = new Rectangle(120, 120, 100, 100);
                puzzleStartPoint = new Vector2(10, 20);
                startRectTexture = TextureStorage.textures[(int)TextureStorage.TEXNAMES.puzzle];
                exists = true;

                numberOfSlots = 2;
                numberOfItems = 2;
                //
                slotPositions = new List<Vector2>();
                slotPositions.Add(new Vector2(150, 150));
                slotPositions.Add(new Vector2(300, 100));
                //slot names go in the same order as the slot positions
                slotNames = new List<String>();
                slotNames.Add("");
                slotNames.Add("");
                //item textures go in the same order as item names
                itemTextures = new List<Texture2D>();
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.Paddle]);
                itemTextures.Add(TextureStorage.textures[(int)TextureStorage.TEXNAMES.Brain]);
                //item names go in the same order as textures
                itemNames = new List<String>();
                itemNames.Add("Paddle");
                itemNames.Add("Ball");
                //
                backGround = TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathroom];
                //
                gameObjects = new List<GameObject>();
                //
                doorList = new List<Door>();
                doorList.Add(new Door(new Rectangle(700, 120, 50, 100), new Vector2(750, 400), "map", new Vector2(40, 12)));
                numberOfDoors = 1;
                doorPositions = new List<Vector2>();
                doorRectangles = new List<Rectangle>();
                board = new Board("Map.txt");
                inventoryScreen = null;
                //
               // doorList = new List<Door>();
                //doorList.Add(new Door(new Rectangle(700, 300, 50, 100), new Vector2(700, 400), "map"));

                _puzzleType = "Physics";

            }


            if (roomName == "jigsaw")
            {

                startRect = new Rectangle(120, 120, 100, 100);
                puzzleStartPoint = new Vector2(10, 20);
                startRectTexture = TextureStorage.textures[(int)TextureStorage.TEXNAMES.puzzle];
                exists = true;

                _puzzleType = "Jigsaw";
                int rows = 5;
                int columns = 8;

                //difficulty from 1 - 10
                int totalDifficult = 10;

                //difficulty from 0 - totalDifficult
                int difficulty = 2;

                numberOfItems = rows * columns;

                //percent of unkown pieces
                int numOfUnknownsPieces = (int)(numberOfItems * 0.6f);
                numberOfSlots = numberOfItems;
                //dimension of image
                int dimension = 50;

                //centering the puzzle
                float puzzleScreenWidth = Game1.screenWidth * 0.8f;
                float puzzleScreenHeight = Game1.screenHeight * 0.8f;
                float jigsawWidth = dimension * columns;
                float jigsawOffset = (puzzleScreenWidth - jigsawWidth) / 2.0f;

                slotPositions = new List<Vector2>();

                Random rand = new Random();

                //temp of empty sltos
                List<Vector2> placeHolder = new List<Vector2>();
                slotNames = new List<String>();

                //x and y value for  pieces at bottom
                int unkownX = 1;
                int unkownY = 1;
                Vector2 tempVec;

                for (int i = 0; i < rows; i++)
                    for (int k = 0; k < columns; k++)
                    {
                        int temp = rand.Next(0, totalDifficult);
                        if (temp < difficulty)
                        {
                            //make sure no overlaping on bottom

                            bool loop = false;
                            do
                            {
                                rand = new Random();
                                tempVec = new Vector2((dimension * rand.Next(0, 1 + (int)(puzzleScreenWidth / dimension))), puzzleScreenHeight - (dimension * rand.Next(1, 3)));

                                foreach (Vector2 vec in slotPositions)
                                {
                                    if (vec == tempVec)
                                    {
                                        loop = true;
                                        break;
                                    }
                                    else
                                        loop = false;
                                }

                            } while (loop);

                            slotPositions.Add(tempVec);
                            slotNames.Add("");
                            placeHolder.Add(new Vector2(k, i));
                            unkownY++;
                            if (unkownY % 3 == 0)
                            {
                                unkownX++;
                                unkownY = 1;
                            }
                        }
                        else
                        {
                            slotPositions.Add(new Vector2(jigsawOffset + (dimension * k), dimension * i));
                            slotNames.Add(k.ToString() + i.ToString());
                        }
                    }

                //add the slots in the x by y gird which are empty
                foreach (Vector2 vec in placeHolder)
                {
                    slotPositions.Add(new Vector2(jigsawOffset + (vec.X * dimension), (vec.Y * dimension)));
                    slotNames.Add(vec.X.ToString() + vec.Y.ToString());
                }

                //adds addition slots for bottom piece holders
                numberOfSlots += placeHolder.Count;



                itemTextures = new List<Texture2D>();

                for (int i = 0; i < numberOfItems; i++)
                    itemTextures.Add(TextureStorage.textures[20 + i]);

                itemNames = new List<String>();

                //givin item names based on their coordinate
                int x = 0;
                int y = 0;

                for (int i = 0; i < numberOfItems; i++)
                {
                    itemNames.Add(x.ToString() + y.ToString());
                    x++;
                    if (x == columns)
                    {
                        x = 0;
                        y++;
                    }
                }

                backGround = TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathroom];
                //
                gameObjects = new List<GameObject>();
                //
                doorList = new List<Door>();
                doorList.Add(new Door(new Rectangle(700, 120, 50, 100), new Vector2(750, 400), "map", new Vector2(40, 12)));
                numberOfDoors = 1;
                doorPositions = new List<Vector2>();
                doorRectangles = new List<Rectangle>();
                board = new Board("Map.txt");
                inventoryScreen = null;



            }



        }
    }
}
