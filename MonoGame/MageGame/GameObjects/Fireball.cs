using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MageGame.GameStates;

namespace MageGame.GameObjects
{
    public class Fireball : SpriteGameObject
    {
        public Fireball(Vector2 startPosition, Vector2 startVelocity, string assetname = "fireball") : base(assetname)
        {
            origin= Center;
            position = startPosition;
            velocity = startVelocity;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
