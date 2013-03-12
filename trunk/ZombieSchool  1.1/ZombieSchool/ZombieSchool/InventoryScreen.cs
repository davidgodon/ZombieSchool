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
    class InventoryScreen
    {

        public float _inventoryWidth;
        public float _inventoryHeight;
        public Rectangle _inventoryRectangle;
        public Texture2D _inventoryTexture;

        public int _positionOnScreen;
        public int _positionOffScreen;

        public bool _isVisible;
        public bool _isMoving;
        public bool _isGoingOnScreen;

        List<Items> _listOfItems;


        public InventoryScreen()
        {
           

            _isVisible = false;
            _isMoving = false;
            _isGoingOnScreen = true;

            _inventoryHeight = (float)Game1.screenHeight * 0.2f;
            _inventoryWidth = (float)Game1.screenWidth * 0.8f;


            _positionOnScreen =  (int)(Game1.screenHeight * 0.8f);
            _positionOffScreen =  Game1.screenHeight;
            _inventoryRectangle = new Rectangle((int)(0.1f * Game1.screenWidth), _positionOffScreen, (int)_inventoryWidth, (int)_inventoryHeight);
            _inventoryTexture = TextureStorage.textures[(int)TextureStorage.TEXNAMES.border];

        }



        public void setTexture(Texture2D texture)
        {
            _inventoryTexture = texture;
        }

        

        public void Update(GameTime gameTime)
        {
            if (_isVisible)
            {
                if (_isMoving)
                {
                    if (_isGoingOnScreen)
                    {
                        int temp =

                        _inventoryRectangle.Y -= (int)(0.25f * (float)gameTime.ElapsedGameTime.Milliseconds);

                        if (_inventoryRectangle.Y < _positionOnScreen)
                        {
                            _inventoryRectangle.Y = _positionOnScreen;
                            _isGoingOnScreen = false;
                            _isMoving = false;
                        }

                    }
                    else
                    {
                        _inventoryRectangle.Y += (int)(0.25f * (float)gameTime.ElapsedGameTime.Milliseconds);

                        if (_inventoryRectangle.Y > _positionOffScreen)
                        {
                            _inventoryRectangle.Y = _positionOffScreen;
                            _isGoingOnScreen = true;
                            _isMoving = false;
                            _isVisible = false;
                        }
                    }
                }
            }
        }



        public void toggleMoving()
        {
            _isMoving = !_isMoving;
            _isVisible = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isVisible)
            {                
                spriteBatch.Draw(_inventoryTexture, _inventoryRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            }
        }

    }

}
