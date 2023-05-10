using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD
{
    public class Enemy : AnimatedGameObject
    {
        float timer;//timer that takes the elapsed time of the current game session and makes the player swap between states based on it
        public int State;//the state at which the enemy is at
        protected int speed = 5;//the base speed of the enemy
        protected PlayerParent PlayerToFollow;//the player it's going to folow and target
        protected PlayingState playstate;//the current playstate
        float StateswitchCD;//the cooldown for which the enemy will need to switch states, for example between folowing and walking around
        float FollowTime;// the amount of the time the enemy will spend following it's target player
        public float Health;// the amount of hits the enemy can take
        protected float playerOffset = 200;//the distance between the player position and the enemy position so that the enemy isn't on top of the player

        public Enemy(PlayingState playingState, float followtime = 13, float swaptime = 6) : base()
        {
            FollowTime = followtime; // sets the amount of time the enemy will follow the player
            StateswitchCD = swaptime;//the amount of time it takes between states
            this.playstate = playingState;//gives the enemy the current playing state
            velocity = new Vector2(speed, 0);//sets the enemy to move using the speed 
            position = new Vector2(Global.Random(0, Global.width), Global.Random(0, Global.height - 200));//sets the position of the enemy to a random spot on the screen
            Health = 2;//sets it to 2 so it only takes 2 hits
        }

        //Method to Reset the enemy back to it's starting variables once the game restarts
        public override void Reset()
        {
            base.Reset();
            position = new Vector2(Global.Random(0, Global.width - sprite.Width), Global.Random(0, Global.height - 200));
            timer = 0;
            State = 0;
            UpdateState();
        }

        public void BorderCollision()
        {
            // Makes sure the player never goes out of the screen.

            // "clamp" the x-position to make sure it never goes out of screen bounds           
            position.X = MathHelper.Clamp(position.X, 0, Global.width);

            // "clamp" the y-position to make sure it never goes out of screen bounds           
            position.Y = MathHelper.Clamp(position.Y, 300, Global.height );
        }

        public override void Update(GameTime gameTime)
        {
            BorderCollision();//checks constantly to not go out of bounds
            Getplayer();//initiates the method to check for the nearest player
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;//gets the time that elapsed in the game and puts it in a variable to be used

            if (timer > StateswitchCD && timer < FollowTime) //checks if the time has elapsed for it to start folowing aslong as it isn't already folowing
            {
                //makes the enemy follow the player and attack
                State = 1;
            }
            else if (timer > FollowTime)//after the randomly generated followtime in seconds makes the enemy go back to idle and resets the timer
            {
                
                GoTo(new Vector2(Global.Random(10, Global.width), Global.Random(10, Global.height)));//makes the player go to a random location
                State = 0;//the "idle" state
                timer = 0;//resets timer
            }

            position += velocity; //makes it so the enemy moves at a decent speed
            UpdateState();//checks if the state has been updated and swaps if needed
            base.Update(gameTime);
        }


        /*Method to swap the enemy into it's required state
         * includes the base state where the enemy just moves around not targeting enemies
         * the folowing state where it targets the enemy
         * the state at which the enemy will start doing it's attack
         * the state at which the enemy will stand still to show it taking damage
         */
        public void UpdateState()
        {
            switch (State)
            {
                case 0:
                    //idle state

                    BaseState();
                    break;
                case 1:
                    //chase state
                    FollowPlayerState();
                    break;
                case 2:
                    //attacking state
                    AttackPlayerState();
                    break;
                case 3:
                    //stunned state
                    DamagedState();
                    break;
            }
        }
        /*Method to set the player into idle and determine it's movement to be from side to side*/
        public virtual void BaseState()
        {
            //makes the player bounce from side to side
            velocity.Y = 0;
            position.X += velocity.X;




            if ((Position.X > Global.width) || (Position.X < 0))//if it hits either side of the screen turn the player around
            {
                position.X -= velocity.X;
                velocity.X = -velocity.X;
            }
        }

        /*Method that checks which player is the closest and puts it in the class within enemy so it can target the player that's closest*/
        public void Getplayer()
        {
            Vector2 distanceOne = playstate.playerOne.Position - position;
            Vector2 distanceTwo = playstate.playerTwo.Position - position;

            //Method to check if the player is closer to player one
            if (distanceOne.X < distanceTwo.X || distanceOne.Y < distanceTwo.Y && !playstate.playerOne.IsDead)
            {
                //gives the enemy player one as player to follow
                PlayerToFollow = playstate.playerOne;
            }
            //Method to check if the player is closer to player two
            else if (distanceTwo.X < distanceOne.X || distanceTwo.Y < distanceOne.Y && !playstate.playerTwo.IsDead)
            {
                //gives the enemy player Two as player to follow
                PlayerToFollow = playstate.playerTwo;
            }
        }

        /*Method that makes the enemy folow the closest player and continue till it reaches the player*/
        public virtual void FollowPlayerState()
        {
            Vector2 playercenter = Vector2.Zero;// variable to use for extra movement adjustments
            if (PlayerToFollow == null)//checks if there is a player around
            {
                return;
            }

            if (PlayerToFollow.IsDead)//checks if the player that's being followed isn't dead already
            {
                return;
            }

            if (PlayerToFollow.Position.X < position.X)//if the player is on the left side of the enemy
            {
                // make the enemy move a bit to the right of the player so it can they can hit eachother properly
                playercenter = new Vector2(PlayerToFollow.Position.X + PlayerToFollow.Sprite.Width - playerOffset, PlayerToFollow.Position.Y);
            }
            else if (PlayerToFollow.Position.X > position.X)
            {
                // make the enemy move a bit to the left of the player so it can they can hit eachother properly
                playercenter = new Vector2(PlayerToFollow.Position.X - PlayerToFollow.Sprite.Width + playerOffset, PlayerToFollow.Position.Y);
            }



            //check the distance of the received player above
            Vector2 distance = playercenter - position;
            //changes the velocity to make the enemy go to the player
            Vector2 directionvelocity = Vector2.Normalize(distance);

            velocity = directionvelocity * speed;


            float atplayer = Vector2.Distance(position, playercenter);//looks at the distance between the enemy's expected position and the enemy's current position
            //makes it stop moving once it reaches the player and make it start attacking
            if (atplayer < 4)
            {
                //makes it stand still
                velocity = new Vector2(0, 0);
                //makes it start attacking
                State = 2;
                UpdateState();
            }
        }
        /*Method that makes the enemy start doing attacks*/
        public virtual void AttackPlayerState()
        {
            Attack(new GameTime());
        }
        /*Method that makes the enemy stop moving and let the animation play*/
        public virtual void DamagedState()
        {
            velocity = Vector2.Zero;
        }

        /*Method that makes the enemy move towards the vector give to it*/
        public virtual void GoTo(Vector2 targetposition)
        {
            //check the distance of the received player above
            Vector2 distance = targetposition + position;
            //changes the velocity to make the enemy go to the player
            Vector2 targetvelocity = Vector2.Normalize(distance);
            velocity = targetvelocity * speed;
        }

        /*Sets up the attack method for it to use in the childrens classes*/
        public virtual void Attack(GameTime gameTime)
        {
        }

        /*makes the enemy lose health move a bit back and make a noise*/
        public virtual void TakeDamage(float amount)
        {
            Health -= amount;
            position.X -= 40;
            //swaps it to where it can't move for a couple seconds
            State = 3;
            UpdateState();
            MainGame.AssetManager.PlaySound("Sounds/Soundeffects/goblinhurt");

        }
    }
}
