using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD
{

    class MeleeEnemy : Enemy
    {
        public float attackTimer; // the time between attacks to let the animation play
        bool firstbox = true; //to make sure it doesn't give an error when it spawns the first hitbox
        string currentAnimation;//the animation that's currently being played
        string newAnimation;//the animation it will switch to
        public MeleeEnemy(PlayingState playingState, float followtime, float swaptime) : base(playingState, followtime, swaptime)
        {

            //loads the animations in and set the frame speed
            LoadAnimation("GoblinSprites/attack/goblinattack@4x1", "attack", true, 0.3f);
            LoadAnimation("GoblinSprites/walk/goblinwalk@4x1", "walk", true, 0.2f);
            LoadAnimation("GoblinSprites/hurt/goblinhurt@2x1", "hurt", true, 0.1f);

            //makes the enemy start doing it's walking animation
            newAnimation = "walk";
            HandleAnimation();
        }

        public override void BaseState()
        {
            base.BaseState(); //uses the movement of it's parent
            //makes it do it's walking animation
            newAnimation = "walk";
            //remove any lingering hitboxes once it switches back to idling
            RemoveAttackHitbox();

        }

        /*Method to remove the hitbox that are made by this enemy on attack*/
        public void RemoveAttackHitbox()
        {
            if (playstate.enemyAttackHitboxes.Children.OfType<Hitbox>().Where(x => x.enemy == this).Any())//looks if there are any hitboxes that have this enemy associated
            {
                //takes the first hitbox it finds with this enemy associated and then removes it
                Hitbox hbox = playstate.enemyAttackHitboxes.Children.OfType<Hitbox>().Where(x => x.enemy == this).First();
                playstate.enemyAttackHitboxes.Remove(hbox);
            }
        }


        /*Method to make the enemy start melee attacking*/
        public override void Attack(GameTime gameTime)
        {
            base.Attack(gameTime);// takes anything that is in the parents
            attackTimer--; // makes the attacktimer go down once every frame
            if (!firstbox)// removes the hitbox only if there are any made
            {
                RemoveAttackHitbox();
            }

            //if the player is on the left side of the of the enemy makes it attack on the left
            if (PlayerToFollow.Position.X < position.X && attackTimer <= 0)
            {
                //start the animation
                newAnimation = "attack";
                if (animations["attack"].SheetIndex == 2)//if it reaches it's "impact" frame start spawning the hitbox and play the sound
                {
                    MainGame.AssetManager.PlaySound("Sounds/Soundeffects/meleeswingEnemy");//plays the swinging sound
                    velocity = Vector2.Zero;//makes the enemy stop moving for the enemy to fit
                    playstate.AddEnemyAttackHitboxesLeft(this, position);//spawns the hitbox on the left 
                    attackTimer = 70;//gives it a delay between attacks
                }
            }
            //if the player is on the right side of the of the enemy makes it attack on the right
            else if (PlayerToFollow.Position.X > position.X && attackTimer <= 0)
            {
                //start the animation
                newAnimation = "attack";
                if (animations["attack"].SheetIndex == 2)//if it reaches it's "impact" frame start spawning the hitbox and play the sound
                {
                    MainGame.AssetManager.PlaySound("Sounds/Soundeffects/meleeswingEnemy");//plays the swinging sound
                    velocity = Vector2.Zero;//makes the enemy stop moving for the enemy to fit
                    playstate.AddEnemyAttackHitboxesRight(this, position);//spawns the hitbox on the right
                    attackTimer = 70;//gives it a delay between attacks
                }
            }
            //makes the enemy turn around for the attack if it's on the left side of the player
            if (PlayerToFollow.Position.X > position.X)
            {
                Mirror = false;
            }
            //removes the first hitbox check so it can remove them acordingly
            firstbox = false;
        }


        /*Method that makes the enemy folow the closest player and continue till it reaches the player*/
        public override void FollowPlayerState()
        {
            RemoveAttackHitbox();//removes the attack hitboxes while it switches to this state to prevent lingering hitboxes
            base.FollowPlayerState();
        }

        /*Method that makes the enemy stop moving and let the animation play*/
        public override void DamagedState()
        {
            base.DamagedState();
            newAnimation = "hurt";//starts the new hurt animation
        }


        
        public override void Update(GameTime gameTime)
        {
            //checks on which side it's walking and turns around the animation based on what way it's walking
            if (velocity.X > 0)
            {
                newAnimation = "walk";//plays the animtaion if it's moving to show it walking
                Mirror = false;
            }
            else
            {
                Mirror = true;
            }


            base.Update(gameTime);
            HandleAnimation();//constantly checks if it changed animations
            
            if (Health < 0)//if the enemy dies remove the hitboxes it left
            {
                RemoveAttackHitbox();
            }

        }
        /*Method to check what animation is being played and changes once new animation gets updated*/
        public void HandleAnimation()
        {

            if (newAnimation != currentAnimation)//checks if the animation being played is the same as the new one
            {
                //plays the animation being played
                PlayAnimation(newAnimation);
                //makes the current animation the newly assigned one
                currentAnimation = newAnimation;
            }
        }
    }
}
