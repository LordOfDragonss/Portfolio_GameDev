using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* <ClassSummary> 
 * Class Made by Sam; 
 * A class for a stamina bar for each player individually, the bar needs to follow their player
 * Stamina bar needs to be visible and constantly updated
 * When a player uses a dodge or attacks, it is visible how much stamina is used
 * The bar needs to refill after no stamina is used
 * The bar needs vertical lines for clarity reasons
 * If a player runs out of stamina, they have to be punished.
 * The bar needs to flash to indicate they are being punished and to show players how long it lasts
 * <EndClassSummary> */

namespace BeatPAD
{
    public class StaminaBar : GameObject
    {
        private PlayerParent myPlayer;//Var to 'attach' the stamina bar to a player
        private Texture2D border; //Var to draw the border of the stamina bar
        private Texture2D value; //Var to draw the inside of the stamina bar
        private Texture2D flashBorder; //Var to draw the outline of the stamina bar, flashes when out of staminá
        private Texture2D dividingLines; //Var to draw the vertical lines inside the stamina bar

        /* Variables to draw the stamina bar */
        private float width; //Width of the stamina bar
        private int height; //Height of the stamina bar
        private int offsetPos; //Var to fit the value inside the border of the stamina bar; starting position
        private int offsetVal; //Var to fit the value inside the border of the stamina bar; Value itself

        public float outOfStaminaDecrease; //Var to decrease the players movement speed when they are put of stamina

        public float goRefillTime; //Float to decide when the bar has to start refilling themselves after using stamina
        public float refillAmount; //Flaot to decide how much stamina a player refills
        private float frameCounter; //Float to keep track of the amount of frames, used for the border flash

        public Color dividingLinesColor; //Color of the vertical lines inside the bar

        /* Constructor that takes a player as argument to 'attach onto'*/
        public StaminaBar(PlayerParent _player)
        {
            myPlayer = _player;//Give the stamina bar a player to track and attach onto
            border = new Texture2D(Global.GraphicsDevice, 1, 1); //Make a sprite for the outline of the stamina bar, pixel size, to draw a rectangle
            value = new Texture2D(Global.GraphicsDevice, 1, 1); //Make a sprite for the filling of the stamina bar, pixel size, to draw a rectangle
            flashBorder = new Texture2D(Global.GraphicsDevice, 1, 1); //Make a sprite for the outline of the stamina bar, pixel size, to draw a rectangle
            dividingLines = new Texture2D(Global.GraphicsDevice, 1, 1);  //Make a sprite for the vertical lines inside of the stamina bar, pixel size, to draw a line

            /* Create a 1D array of color data to fill the pixel texture with */
            Color[] colorData = {
                        Color.White,
            };

            /* Set the texture data with the color information */
            value.SetData<Color>(colorData);
            border.SetData<Color>(colorData);
            flashBorder.SetData<Color>(colorData);
            dividingLines.SetData<Color>(colorData);

            Reset();
        }

        /* Method to call in the constructor and to reset possible made changes to variables */
        public override void Reset()
        {
            base.Reset();

            position = myPlayer.GlobalPosition;//The stamina bar follows their player

            width = myPlayer.maxStamina; //The width of the bar starts of as the same size as the maximum of stamina their player has
            height = 12; //Change to change the Y size of the stamina bar (height)
            offsetPos = 2; //Do not change 
            offsetVal = 4; //Do not change
            outOfStaminaDecrease = 3; //Players' movement speed gets divided by this amount to slow them down

            goRefillTime = 2; //Change to increase/decrease amount of seconds after which the bar starts to refill
            refillAmount = 30; //Change to increase/decrease amount of stamina a player refills

            frameCounter = 0; //Framecounter starts as 0
        }

        /* Update method for the stamina bar; follow their player and call StaminaManager(); */
        public override void Update(GameTime gameTime)
        {
            position.X = myPlayer.GlobalPosition.X - width / 2;
            position.Y = myPlayer.GlobalPosition.Y - myPlayer.texture.Height;
            StaminaManager(gameTime);
        }

        /* Method to draw the stamina bar*/
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);


            //If the player is slowed the method to flash the border gets called
            if (myPlayer.isSlowed)
            {
                OutOfStaminaBorderFlash(spriteBatch);
            }

            /* Draw the stamina bar */
            spriteBatch.Draw(border, new Rectangle((int)position.X, (int)position.Y, (int)width, height), Color.Black); //Draw the border of the stamina bar, in black
            spriteBatch.Draw(value, new Rectangle((int)position.X + offsetPos, (int)position.Y + offsetPos, (int)myPlayer.stamina - offsetVal, height - offsetVal), myPlayer.staminaColor); //Draw the inside of the stamina bar, the amount of stamina a player currently has

            /* For loop to draw dividing lines inside the staminabar to indicate how much stamina is needed to perform a dodge */
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(dividingLines, new Rectangle((int)position.X + (int)myPlayer.dodgeStaminaCost + ((int)myPlayer.dodgeStaminaCost * i), (int)position.Y, 1, height), Color.Black);
            }
        }

        /* Method to call in Update to: 
         * refill the stamina bar of the player
         * not slow their player down when they have enough stamina */
        public void StaminaManager(GameTime gameTime)
        {
            myPlayer.stamina = MathHelper.Clamp(myPlayer.stamina, -1, myPlayer.maxStamina); //Make sure stamina can't go below -1 or max stamina size.

            myPlayer.waitForRefillTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //The time  before the staminabar starts to refill is x seconds

            /* If the timer exceeds x seconds, the stamina bar starts to refill */
            if (myPlayer.waitForRefillTime > goRefillTime)
            {
                myPlayer.stamina += refillAmount * (float)gameTime.ElapsedGameTime.TotalSeconds; //Refill amount per seconds
            }

            /* If a player has more than 1 fifth of the total stamina, they are no longer slowed (if they were) */
            if (myPlayer.stamina > myPlayer.maxStamina / 5)
            {
                myPlayer.isSlowed = false;
            }
        }

        /* Method to call when a player performs an action that costs stamina 
         * Uses the given amount in the argument as the cost for the stamina
         * Sets the refilltimer to 0 so it starts x amount of time after the action to refill the stamina bar.
         */
        public void UseStamina(float _amount)
        {
            myPlayer.stamina -= _amount; //When calling this method an amount of stamina (cost) goes with it to decrease their players' stamina with
            myPlayer.waitForRefillTime = 0; //Set to 0 to start the wait time before refilling

            /* When a player goes below a certain amount, they will get punished for not managing their stamina correctly */
            if (myPlayer.stamina <= 3) //Set to 3 instead of 0 to feel more smoothly
            {
                OutOfStaminaBurn(); //Call the punishment method! 
            }
        }

        /* Method to slow players down when they run out of stammina */
        public void OutOfStaminaBurn()
        {
            myPlayer.isSlowed = true; //Set to true to slow players down
        }

        /* Method to display a red flashing border around the stamina bar when the player runs out of stamina */
        public void OutOfStaminaBorderFlash(SpriteBatch spriteBatch)
        {

            /* Local var to dictate below and above how many frames the flash should happen */
            float frames = 15; //Change to get longer / shorter flashes

            /* Border is red when the framecounter is below the frames size */
            if (frameCounter < frames)
            {
                spriteBatch.Draw(flashBorder, new Rectangle((int)position.X - 1, (int)position.Y - 1, (int)width + offsetPos, height + offsetPos), Color.Red);
            }

            /*Border is black when the framecounter is above a certain amount of frames */
            if (frameCounter > frames * 0.85)
            {
                spriteBatch.Draw(flashBorder, new Rectangle((int)position.X - 1, (int)position.Y - 1, (int)width + offsetPos, height + offsetPos), Color.Black);

                /* If the framecounter exceeds  double the size of the frames variable, the counter gets reset to 0 to loop the method from start */
                if (frameCounter > frames * 2)
                {
                    frameCounter = 0; //Set to 0 to start the loop 
                }
            }
            /* Every frame the framecounter gets +1 to change color based on above conditions */
            frameCounter++;
        }
    }
}
