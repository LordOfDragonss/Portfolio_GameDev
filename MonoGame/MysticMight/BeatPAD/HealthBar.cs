using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* <ClassSummary>
 * Class made by Sam
 * Healthbar class for both players to individually indicate their health
 * Needs to be clearly visible on the screen, updated every frame
 * <EndClassSummary> */

namespace BeatPAD
{
    class HealthBar : GameObject
    {
        private PlayerParent myPlayer; //Var to 'attach' the healthbar bar to a player
        private Texture2D border; //Var to draw the border of the health bar
        private Texture2D value; //Var to draw the inside of the health bar
        private float width; //Width of the health bar
        private int height; //Height of the health bar
        private int offsetPos; //Var to fit the value inside the border of the health bar; starting position
        private int offsetVal; //Var to fit the value inside the border of the health bar; Value itself
        private Color healthColor;
        public HealthBar(Vector2 StartPosition, PlayerParent _player)
        {
            position = StartPosition;
            myPlayer = _player; //Give the health bar a player to track their players' health

            width = myPlayer.maxHealth; //Set the width to maxHealth to change the width of the border according to the players' maxhealth

            border = new Texture2D(Global.GraphicsDevice, 1, 1); //Make a sprite for the outline of the health bar, pixel size, to draw a rectangle
            value = new Texture2D(Global.GraphicsDevice, 1, 1); //Make a sprite for the filling of the health bar, pixel size, to draw a rectangle
            height = 20; //Change to change the Y size of the stamina bar (height of the border)
            offsetPos = 2; //Do not change
            offsetVal = 4; //Do not change

            healthColor = Color.LimeGreen; //Set the startingcolor to limegreen

            //Create a 1D array of color data to fill the pixel texture with.  
            Color[] colorData = {
                        Color.White,
            };

            //Set the texture data with our color information.  
            value.SetData<Color>(colorData);
            border.SetData<Color>(colorData);
        }

        /* Method to reset the players health to full */
        public override void Reset()
        {
            base.Reset();
            healthColor = Color.LimeGreen;
            myPlayer.currentHealth = myPlayer.maxHealth;
        }

        /* Update method to change the healthbar color based on the currenthealth of their player */
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ChangeBarColor();
        }

        //Changes the color of the healthbar between Green, yellow and red based on current health
        public void ChangeBarColor()
        {
            //The bar is green if the player has more then 66% health
            if (myPlayer.currentHealth >= myPlayer.maxHealth * 0.66)
            {
                healthColor = Color.LimeGreen;
            } //The bar is yellow if the player has between 33% and 66% health
            else if (myPlayer.currentHealth >= myPlayer.maxHealth * 0.33 && myPlayer.currentHealth <= myPlayer.maxHealth * 0.66)
            {
                healthColor = Color.Yellow;
            } //The bar is red if the player has less then 33% health
            else if (myPlayer.currentHealth <= myPlayer.maxHealth * 0.33)
            {
                healthColor = Color.Red;
            }
        }

        /* Method to draw the healthbar */
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(border, new Rectangle((int)position.X, (int)position.Y, (int)width, height), Color.Black); //Border of the healthbar, colored black
            spriteBatch.Draw(value, new Rectangle((int)position.X + offsetPos, (int)position.Y + offsetPos, (int)myPlayer.currentHealth - offsetVal, height - offsetVal), healthColor); //Inside value of the healthbar, based on the players currenthealth and colored according to above method
        }
    }
}
