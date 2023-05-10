using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeatPAD
{
    public class PlayerParent : AnimatedGameObject
    {
        public Texture2D texture;
        protected Dodge dodge;
        protected StaminaBar staminaBar;
        protected bool attacking; // A boolean to check if the player is attacking

        public bool IsDead; // Boolean to check if the player is dead
        public bool hasCollision; // Boolean for collision
        

        /* Variables to let players walk */
        public int xStartingSpeed; // xStartingSpeed of the player
        public int yStartingSpeed; // yStartingSpeed of the player
        public int xSpeed; // xSpeed of the player
        public int ySpeed; // ySpeed of the player

        public int attackcooldown; // The cooldown of the attack
        public bool isSlowed; // Boolean to check if the player is slowed

        /*Variables to let players dodge */
        public int speedIncreaseX; // Adds to the X velocity of players when performing a dodge
        public string direction; // Variable to let players dash in a certain direction
        public bool dashAvailable; // Bool to track if the players are able to use a dash
        private float cooldown; // Float to set the dodge on a cooldown

        /* Variables for the stamina of the players */
        public float maxStamina; // Max stamina to define how much stamina a player has
        public float minimumStaminaToDodge; // The minimal stamina that a player needs to be able to dodge
        public float stamina; // Float to change how much stamina the player currently has
        public float waitForRefillTime; // Float to define how long the game waits before starting to refill the stamina bar
        public Color staminaColor; // The color of the staminaBar

        public float dodgeStaminaCost; // The staminaCost of dodging

        /* Health variables */
        public float maxHealth; // The max health of the player
        public float currentHealth; // The current health of the player

        public PlayerParent(PlayingState playingState, string assetName) : base()
        {
            texture = Global.content.Load<Texture2D>(assetName); // Load the sprite
            dodge = new Dodge(this); // Assign the dodge class to the dodge variable
            staminaBar = new StaminaBar(this); // Assign the StaminaBar class to the staminaBar variable

            hasCollision = true; // Enable collision 
            xStartingSpeed = 300; // Start speed on the X axes when starting the game
            yStartingSpeed = 150; // Start speed on the Y axes when starting the game
            xSpeed = xStartingSpeed; // xSpeed gets set to the startspeed so it can be influenced later on 
            ySpeed = yStartingSpeed; // ySpeed gets set to the startspeed so it can be influenced later on

            speedIncreaseX = 0; // Set to 0 because it adds to the velocity every update loop

            maxStamina = 200; // Starting stamina (change to change width of the bar)
            minimumStaminaToDodge = dodge.cost; // Set the minimumStaminaToDodge equel to the dodge.cost
            stamina = maxStamina; // Set the stamina to the max value
            dodgeStaminaCost = dodge.cost; // Set the dodgeStaminaCost equel to the dodge.cost

            maxHealth = 400; // Starting health of players (change to change width of the bar)
            currentHealth = maxHealth; // Set the currenthealth to the max value
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateMovement(gameTime);
            BorderCollision();

            if (attackcooldown > 0) // Timer for the player attack
            {
                attackcooldown--;
            }

            velocity.X += speedIncreaseX;

            /* Change the color of the stamina bar depending on the players' stamina and slow status */
            if (stamina < minimumStaminaToDodge || isSlowed)
            {
                staminaColor = Color.Gray; //The value is gray to indicate stamina is not usable
            }
            else
            {
                staminaColor = Color.DeepSkyBlue; // The value is blue to indicate it is usable
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            staminaBar.Draw(gameTime, spriteBatch);
            dodge.Draw(gameTime, spriteBatch);
        }

        /* Update the player position using the velocity
         * Player position gets placed in GameObject.cs using inheritence */
        public void UpdateMovement(GameTime gameTime)
        {
            // Assume player is not moving
            velocity.X = 0;
            velocity.Y = 0;
        }

        // Method to prevent payers from going out the screen
        public void BorderCollision()
        {
            // Make sure the player never goes out of the screen.

            // "clamp" the x-position to make sure it never goes out of screen bounds           
            position.X = MathHelper.Clamp(position.X, 0, Global.width);

            // "clamp" the y-position to make sure it never goes out of screen bounds           
            position.Y = MathHelper.Clamp(position.Y, 300, Global.height); //Moet nog een klein beetje gefixed worden, denk dat het aan de Global.height ligt.
        }

        // Method to take damage
        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;
        }
    }
}