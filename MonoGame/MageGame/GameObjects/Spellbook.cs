using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MageGame.GameObjects
{
    public class Spellbook : SpriteGameObject
    {
        string assetName;
        public Spellbook(Vector2 spawnposition, string assetName = "SpellBooks/Basebook") : base(assetName)
        {
            this.assetName = assetName;
            position = spawnposition;
        }
    }
}
