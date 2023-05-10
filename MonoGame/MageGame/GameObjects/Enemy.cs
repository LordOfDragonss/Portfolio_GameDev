using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MageGame.GameStates;
using Microsoft.Xna.Framework.Graphics;

namespace MageGame.GameObjects
{
    class Enemy : SpriteGameObject
    {
        float gravity = 9.81f;
        public bool burning;
        public Enemy(Vector2 spawnposition) : base("beano")
        {
            position = spawnposition;
            velocity.Y += gravity;
        }

        public void stopFalling()
        {
            velocity.Y = 0;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (burning)
            {
                sprite = new SpriteSheet("beano_burn", 0);
            }
        }
    }
}
