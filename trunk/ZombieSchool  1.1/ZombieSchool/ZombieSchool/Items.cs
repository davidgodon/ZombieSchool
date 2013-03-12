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
    class Items
    {

        public Rectangle _itemRectangle;

        public Vector2 _restPosition;

        public bool _isBeingDragged;
        public bool _isVisible;

        public Texture2D _itemTexture;

        public String _itemName;
        public String _itemDestinationName;

        public Vector3 _velocity;
        public float _height;

        public Items(Vector2 position, Texture2D texture)
        {
            _itemName = "Item";
            _restPosition = position;
            _isBeingDragged = false;
            _isVisible = false;
            _itemTexture = texture;

            _itemRectangle = new Rectangle((int)_restPosition.X, (int)_restPosition.Y, _itemTexture.Width, _itemTexture.Height);
        }

        public Items(Vector2 position, Texture2D texture, String name)
        {
            _itemName = name;
            _restPosition = position;
            _isBeingDragged = false;
            _isVisible = false;
            _itemTexture = texture;
            
            _itemRectangle = new Rectangle((int)_restPosition.X, (int)_restPosition.Y, _itemTexture.Width, _itemTexture.Height);

            _height = 0;

            if (name == "Ball")
            {
                _height = 10;
                _velocity = new Vector3();
            }

        }



        public virtual void toggleVisible()
        {
            _isVisible = !_isVisible;
            //Console.WriteLine("ItemPOsition: " + _restPosition);
        }

        public String getItemName()
        {
            return _itemName;
        }

        public void SetName(String name)
        {
            _itemName = name;
        }
        public void setTexture(Texture2D texture)
        {
            _itemTexture = texture;
        }

        public void Update(GameTime gametime)
        {
            if (_isVisible)
                if (!_isBeingDragged)
                    setOffsetPosition(_restPosition);
        }

        public void setBeingDragged(bool dragged)
        {
            _isBeingDragged = dragged;
        }
       

        public void setOffsetPosition(Vector2 position)
        {
            _itemRectangle.Location = new Point((int)(position.X), (int)(position.Y));
        }


        public void setRestPosition(Vector2 position)
        {
            _restPosition = position;
        }

        public void setPosition(int x, int y)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isVisible)
            {

                if (_itemName != "Ball")
                    spriteBatch.Draw(_itemTexture, _itemRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.95f);
                else
                    spriteBatch.Draw(_itemTexture, new Vector2(_itemRectangle.X, _itemRectangle.Y), null, Color.White, 0, Vector2.Zero, new Vector2(_height / 5, _height / 5), SpriteEffects.None, 1);
            }
        }

    }
}
