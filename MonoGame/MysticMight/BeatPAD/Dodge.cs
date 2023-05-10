using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* <ClassSummary>
 * Class Made by Sam; 
* Players can dodge both left and right to accelerate in that direction
* Dash has a soundeffect
* The dodge is linked to a player to use their stamina and be individually used by the players
* The dodge has a cooldown when used, so it won't be spammable and overused. The cooldown is visible
* The dodge costs the players an amount of their stamina, and has a minimum stamina needed to dash
* <EndClassSummary> */

namespace BeatPAD
{
    public class Dodge : GameObject
    {
        /* Make a player variable to pass onto the constructor */
        private PlayerParent myPlayer;

        /* Sound effect to use when a player dodges */
        private string dodgeSoundEffect;

        /* Variables to let players dodge */
        public bool dashAvailable; //Bool to track if the players are able to use a dash
        private int dashSpeed; //The speed of which a player dashes with
        private float dashDuration; //Duration of the dash
        public float cost; //Float to decide the stamina cost of the dodge

        /* Variables to manage the cooldown of the dodge */
        public float maxCooldown; //Float to decide how long the cooldown of the dodge is
        public float currentCooldown; //Float to track the current cooldown

        /* Visible cooldown variables */
        public Texture2D cooldownBar; //Var to draw the cooldown bar with
        public float barWidth; //Width of the cooldown bar
        public float barHeight; //Height of the cooldown bar

        /* Constructor */
        public Dodge(PlayerParent _player)
        {
            myPlayer = _player; //Assign a player to a dash
            dodgeSoundEffect = "Sounds/Soundeffects/dodge"; //Load the soundeffect file from the contentloader as a string
            dashSpeed = 1500; //Change to change the speed of the dash
            cost = 40; //Change to increase/decrease amount of stamina used when using the dodge 

            maxCooldown = 2; //Change to increase/decrease the cooldown between being able to use the dodge

            cooldownBar = new Texture2D(Global.GraphicsDevice, 1, 1); //Pixel sized texture to draw a rectangle with
            barHeight = 3; //Change to change the height of the visible cooldown bar of the dodge

            //Create a 1D array of color data to fill the pixel texture with.  
            Color[] colorData = {
                        Color.White,
            };

            //Set the texture data with the color information.  
            cooldownBar.SetData<Color>(colorData);

            /* Call the reset method so the dodge is immediatly usable when spawning */
            Reset();
        }
        /* Reset method to reset cooldown of the dodge */
        public override void Reset()
        {
            base.Reset();
            currentCooldown = maxCooldown; //Set the cooldown to maxCooldown so the dodge can immediatly be used
        }

        /* Method to: keep track of timers
         * Change the speed of the players when dodging and not dodging
         * Set the availability of the dash */

        public override void Update(GameTime gameTime)
        {
            /* Used for drawing the cooldown bar */
            position.X = myPlayer.GlobalPosition.X - 98; //X position of the cooldown bar is the same as the stamina bar
            position.Y = myPlayer.GlobalPosition.Y - myPlayer.texture.Height + 15; //Y position of the cooldown bar is under the stamina bar

            /* The bar is only visible when the player used a dodge */
            if (currentCooldown < maxCooldown) //Means the dodge is on cooldown, so the cooldown needs to be shown
            {
                barWidth = 100; // Change to change the width of the bar
            }
            else
            {
                barWidth = 0; //Not visible if the player didn't dodge
            }

            barWidth -= currentCooldown * (barWidth / maxCooldown); //The width of the bar depends on the cooldown of the dodge.

            /* <End of cooldown bar> */

            /* Timers for the duration of the dash and the cooldown */
            dashDuration += (float)gameTime.ElapsedGameTime.TotalSeconds; //dashDuration timer depends on the seconds of the game
            currentCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds; //Cooldown is counted in seconds

            /* How long the dash lasts for, sets the standard movement speed for players */
            if (dashDuration >= 0.15) //If the dash is not being used.. 
            {
                myPlayer.speedIncreaseX = 0; //..The player does not have a speed increase 
            }

            /* Set the dashAvailable boolean to true and false depending on the cooldown of the dodge */
            if (currentCooldown > maxCooldown)
            {
                dashAvailable = true;
            }
            else
            {
                dashAvailable = false;
            }

            /* Players need a minimum stamina amount to be able to dodge, and they cannot be slowed */
            if (myPlayer.stamina < myPlayer.minimumStaminaToDodge || myPlayer.isSlowed)
            {
                dashAvailable = false;
            }
        }

        /* Method to draw the cooldown bar on the screen */
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(cooldownBar, new Rectangle((int)position.X, (int)position.Y, (int)barWidth, (int)barHeight), Color.Orange);
        }

        /* Method that gets called when a player presses a dodge button:
         * One argument to pass into when calling, to decide what direction the player has to dodge into
         * Plays the soundeffect
         * Sets the cooldown and duration to 0 to start their timers
         * Depending on the given direction, the player gets a increase in their movementspeed in that direction
         */
        public void UseDodge(string _direction)
        {
            /* Play the sound effect of the dodge */
            MainGame.AssetManager.PlaySound(dodgeSoundEffect);

            currentCooldown = 0; //Cooldown gets set to 0, when it reaches (maxCooldown) the dodge is usable again
            dashDuration = 0; //Sets the duration to 0, when it reaches (x) the dodge is over

            /* Dash to the left */
            if (_direction == "Left")
            {
                myPlayer.speedIncreaseX = -dashSpeed;
            }
            /* Dash to the right */
            if (_direction == "Right")
            {
                myPlayer.speedIncreaseX = dashSpeed;
            }
        }
    }
}
