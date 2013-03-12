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
    class Door
    {
        public Rectangle doorRect;
        public Vector2 position;
        public Texture2D texture;
        public String nextRoom;
        public Vector2 doorEntrancePoint;

        public Door(Rectangle _doorRect, Vector2 _position, Texture2D _texture, String _nextRoom)
        {

        }

        public Door(Rectangle _doorRect, Vector2 _position, String _nextRoom, Vector2 _doorEntrancePoint)
        {
            nextRoom = _nextRoom;
            doorRect = _doorRect;
            position = _position;
            doorEntrancePoint = _doorEntrancePoint;
        }

        public void LeaveRoom()
        {
            RoomStats.LoadRoom(nextRoom);
        }


    }
}
