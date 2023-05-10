using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MageGame.GameObjects
{
     class Platform :SpriteGameObject
    {
        public Platform(string assetName, Vector2 startPosition) : base(assetName)
        {
            position = startPosition;
        }
    }
}
