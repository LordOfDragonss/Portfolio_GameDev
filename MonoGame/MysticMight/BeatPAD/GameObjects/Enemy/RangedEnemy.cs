using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD
{
    public class RangedEnemy : Enemy
    {
        PlayingState _state;//the current playstate
        public float timer;//timer that takes the elapsed time of the current game session and makes the player swap between states based on it
        public bool fired;// boolean to check if the enemy has already fired it's projectile or not
        public float firedCooldown;//time between shots
        public RangedEnemy(PlayingState playingState, float followtime, float swaptime = 6) : base(playingState, followtime, swaptime)
        {
            //loads animation
            LoadAnimation("GoblinSprites/RangedGoblin", "idle", true);
            //initiates the current playstate
            _state = playingState;
            Reset();
        }

        public void Reset()
        {
            // Reset all the variables
            firedCooldown = 1.7f;
            fired = false;
        }

        public override void Attack(GameTime gameTime)
        {
            Getplayer();
            // if fired is false and the timer is higher than the firedCooldown, then the enemy shoots an arrow. and sets fired to true
            if (!fired && timer > firedCooldown)
            {
                Arrow arrow = new Arrow(position, new Vector2(10), PlayerToFollow);
                _state.enemyProjectiles.Add(arrow);
                fired = true;
            }
            //if fired is true reset the timer to 0 seconds and set fired to false.
            if (fired)
            {
                timer = 0;
                fired = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            PlayAnimation("idle");
            // makes sure the timer moves up with the amount of time in seconds
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            //makes it the right orientation
            Mirror = true;
            origin = Center;
        }
        public override void FollowPlayerState()
        {
            //makes it stand still to shoot
            velocity.X = 0;
            velocity.Y = 0;

            State = 2;
            UpdateState();
        }
    }
}
