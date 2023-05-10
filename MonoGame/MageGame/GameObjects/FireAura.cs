using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MageGame.GameStates;
using Microsoft.Xna.Framework.Graphics;

namespace MageGame.GameObjects
{
    public class FireAura :SpriteGameObject
    {
        public FireAura(Vector2 startposition) : base("sphere")
        {
            position = startposition;
            origin = Center;
            visible = false;
        }
    }
}
