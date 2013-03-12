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

    class Slot
    {
        public String _slotName;
        public Vector2 _slotPosition;
        public bool _complete;

        public Slot()
        {}
    }


    class PuzzleScreen
    {
        public float _puzzleWidth;
        public float _puzzleHeight;
        public Rectangle _puzzleRectangle;
        public Texture2D _puzzleTexture;
        public Texture2D _puzzleBackground;
        public bool _isVisible;
        public Items _itemBeingMoved;
        public bool _isComplete;
        public bool _isMovingItem;
        public List<Items> _itemList;
        public List<Slot> _itemSlotsList;
        public Rectangle _startPuzzleRect;
        public bool exists;

        public int _bounceCount;

        public string _puzzleType;

        public PuzzleScreen()
        {
            _puzzleType = RoomStats._puzzleType;
            //if puzzle type == 1
            {
                exists = RoomStats.exists;
                _isVisible = false;
                _isMovingItem = false;
                _puzzleHeight = (float)Game1.screenHeight * 0.8f;
                _puzzleWidth = (float)Game1.screenWidth * 0.8f;
                _puzzleRectangle = new Rectangle((int)(0.1f * Game1.screenWidth), 0, (int)_puzzleWidth, (int)_puzzleHeight);
                _itemList = new List<Items>();
                _itemSlotsList = new List<Slot>();
                _puzzleBackground = TextureStorage.textures[(int)TextureStorage.TEXNAMES.mathpuzzle01];
                _puzzleTexture = TextureStorage.textures[(int)TextureStorage.TEXNAMES.border];

                for (int i = 0; i < RoomStats.numberOfSlots; i++)
                {
                    Slot temp = new Slot();
                    temp._slotName = RoomStats.slotNames[i];                    
                    temp._slotPosition = puzzleToWorldPosition((int)RoomStats.slotPositions[i].X, (int)RoomStats.slotPositions[i].Y);
                    temp._complete = false;
                    _itemSlotsList.Add(temp);
                }

                for (int i = 0; i < RoomStats.numberOfItems; i++)
                {
                    CreateItem(RoomStats.itemTextures[i], i, RoomStats.itemNames[i]);
                }
            }
            //if puzzle type == 2
            //{
            //
            //}
        }

        
        public virtual void CreateItem(Texture2D texture)
        {
            _itemList.Add(new Items(_itemSlotsList[2]._slotPosition, texture));
        }

        public virtual void CreateItem(Texture2D texture, int itemSlot, String name)
        {
            _itemList.Add(new Items(_itemSlotsList[itemSlot]._slotPosition, texture, name));
            if (_itemSlotsList[itemSlot]._slotName == name)
                _itemSlotsList[itemSlot]._complete = true;
        }

        public virtual void setTexture(Texture2D texture)
        {
            _puzzleTexture = texture;
        }

        public virtual void SetBackground(Texture2D texture)
        {
            _puzzleBackground = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!_isVisible && !_isComplete)
            {
                //draw the box to start the puzzle
            }

            if (_isVisible && _puzzleType == "Math" || _puzzleType == "Jigsaw")
            {
                foreach (Items item in _itemList)
                {
                    item.Update(gameTime);
                }

                if (_isComplete)
                {
                    _isVisible = false;
                }

                if (_isComplete != true)
                {
                    _isComplete = true;
                    foreach (Slot slot in _itemSlotsList)
                    {
                        if (slot._slotName != "")
                        {
                            if (slot._complete == false)
                            {
                                _isComplete = false;
                            }
                        }
                    }
                }
            }
            else if (_isVisible && _puzzleType == "Physics")
            {
                updateBall(gameTime);
                if (_isComplete)
                    _isVisible = false;
            }
        }


        public void updateBall(GameTime gametime)
        {
            foreach (Items item in _itemList)
            {
                if (item._itemName == "Ball")
                {
                    item._velocity.Y = item._velocity.Y + (-0.98f * (float)gametime.ElapsedGameTime.TotalSeconds);
                    item._height += (item._velocity.Y * (float)gametime.ElapsedGameTime.TotalSeconds) + (0.5f * -0.98f * (float)gametime.ElapsedGameTime.TotalSeconds * (float)gametime.ElapsedGameTime.TotalSeconds);

                    item._restPosition = new Vector2(item._restPosition.X + (item._velocity.X * (float)gametime.ElapsedGameTime.TotalSeconds), item._restPosition.Y + (item._velocity.Z * (float)gametime.ElapsedGameTime.TotalSeconds));

                    item._itemRectangle = new Rectangle((int)item._restPosition.X, (int)item._restPosition.Y, item._itemRectangle.Width, item._itemRectangle.Height);

                    
                    if (item._height > 4 && item._height < 6)
                    {
                        if (_itemBeingMoved != null && _itemBeingMoved._itemRectangle.Intersects(item._itemRectangle))
                        {
                            //item._velocity.Y = 3;
                            // Console.WriteLine("velocity: " + item._velocity);

                            item._velocity = new Vector3((item._itemRectangle.X + item._itemRectangle.Width / 2) - (_itemBeingMoved._itemRectangle.X + _itemBeingMoved._itemRectangle.Width / 2), 3, (item._itemRectangle.Y + item._itemRectangle.Height / 2) - (_itemBeingMoved._itemRectangle.Y + _itemBeingMoved._itemRectangle.Height / 3));
                            Console.WriteLine("Height" + _itemBeingMoved._itemRectangle.Width / 2);
                            _bounceCount++;

                            if (_bounceCount == 4)
                                _isComplete = true;
                        }

                    }
                    else if (item._height < 0)
                    {
                        _bounceCount = 0;
                        item._restPosition = new Vector2(_itemSlotsList[1]._slotPosition.X, (int)_itemSlotsList[1]._slotPosition.Y);
                        item._itemRectangle = new Rectangle((int)_itemSlotsList[1]._slotPosition.X, (int)_itemSlotsList[1]._slotPosition.Y, item._itemRectangle.Width, item._itemRectangle.Height);
                        item._velocity = new Vector3(0, 0, 0);
                        item._height = 10;
                    }

                }
            }
        }

        public virtual void moveItem(int x, int y)
        {
            if (!_isMovingItem)
            {
                foreach (Items item in _itemList)
                {
                    if (item._itemRectangle.Contains(new Point(x, y)))
                    {
                        _isMovingItem = true;
                        item.setBeingDragged(true);
                        _itemBeingMoved = item;
                        break;
                    }
                }
            }
            else
            {
                  if(RoomStats._puzzleType != "Physics")
                     _itemBeingMoved.setOffsetPosition(new Vector2(x - (_itemBeingMoved._itemTexture.Width / 2), y - (_itemBeingMoved._itemTexture.Height / 2)));
                  else
                      _itemBeingMoved.setOffsetPosition(new Vector2(x - (_itemBeingMoved._itemTexture.Width / 2), y - (_itemBeingMoved._itemTexture.Height * 4 / 5)));

            }
        }

        public virtual void releasedItem()
        {
            _isMovingItem = false;
            _itemBeingMoved._isBeingDragged = false;

            foreach (Slot slot in _itemSlotsList)
           {
                //make slot rectangle later
                if (_itemBeingMoved._itemRectangle.Intersects(new Rectangle((int)slot._slotPosition.X, (int)slot._slotPosition.Y, (int)80, 80)) && _itemBeingMoved.getItemName() == slot._slotName)
                {
                    _itemBeingMoved.setRestPosition(slot._slotPosition);
                    slot._complete = true;                    
                }
            }
        }

        public virtual void releasedItemPhysics(int x, int y)
        {
            _isMovingItem = false;
            _itemBeingMoved._isBeingDragged = false;

            _itemBeingMoved.setRestPosition(new Vector2(x - _itemBeingMoved._itemTexture.Width / 2, y - _itemBeingMoved._itemTexture.Height / 2));

        }

        public virtual Vector2 mouseToPuzzlePosition(int x, int y)
        {
            if (_puzzleRectangle.Contains(new Point(x, y)))
            {
               Vector2 temp = new Vector2(x - (int)(0.1f * Game1.screenWidth), y);
               //Console.WriteLine("Mouse Position On Puzzle: " + temp);
               return temp;
            }
            else
                return Vector2.Zero;
        }

        public virtual Vector2 puzzleToWorldPosition(Vector2 position)
        {
            Vector2 temp = new Vector2(position.X + (int)(0.1f * Game1.screenWidth), position.Y);
            return temp;
        }

        public virtual Vector2 puzzleToWorldPosition(int x, int y)
        {
            Vector2 temp = new Vector2(x + (int)(0.1f * Game1.screenWidth), y);
            return temp;
        }

        public virtual void toggleVisible()
        {
            _isVisible = !_isVisible;

            foreach (Items item in _itemList)
            {
                item.toggleVisible();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(exists)
            {
                if (_isVisible)
                {
                    if (_puzzleBackground != null)
                    {
                        spriteBatch.Draw(_puzzleBackground, _puzzleRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.9f);
                    }
                    spriteBatch.Draw(TextureStorage.textures[(int)TextureStorage.TEXNAMES.border], _puzzleRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.9f);

                    //to see the slots, for debugging
                    // foreach (Slot slot in _itemSlotsList)
                    //{
                    //int height = 163;
                    //int width = 65;
                    //spriteBatch.Draw(_puzzleTexture, new Rectangle((int)slot._slotPosition.X, (int)slot._slotPosition.Y, width, height), Color.White);              
                    // }

                    if (_itemList.Count != 0)
                    {
                        foreach (Items item in _itemList)
                        {
                            item.Draw(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}
