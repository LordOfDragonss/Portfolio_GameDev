using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MageGame.GameStates;
using System.Diagnostics;

namespace MageGame.GameObjects
{
    class Player : AnimatedGameObject
    {
        bool isGrounded;
        float gravity = 9.81f;
        public Player() : base()
        {
            LoadAnimation("PlayerSprites/Chuppy@1x1", "idle", false);
            LoadAnimation("PlayerSprites/chuppywalk@3x1", "walk", true);
            position = new Vector2(Game1.Screen.X / 4, Game1.Screen.Y / 4);
            velocity.Y += gravity;
            PlayAnimation("idle");
            origin = Center;
        }

        public void stopFalling()
        {
            velocity.Y = 0;
            isGrounded = true;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.IsKeyDown(Keys.A))
            {
                velocity.X = -100;
                PlayAnimation("walk");
                Mirror = true;
            }

            // Move Right
            else if (inputHelper.IsKeyDown(Keys.D))
            {
                velocity.X = 100;
                PlayAnimation("walk");
                Mirror = false;
            }
            else
            {
                PlayAnimation("idle");
            }

            if (inputHelper.KeyPressed(Keys.Space) && isGrounded)
            {
                velocity.Y = -300;
                isGrounded = false;
            }



            base.HandleInput(inputHelper);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            velocity.Y += gravity;

            velocity.X = 0;
        }
    }
}
