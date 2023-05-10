using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeatPAD
{
    public class PlayerOne : PlayerParent
    {
        protected PlayingState playingState; // Give the playingState
        private const string AssetName = "DummySprites/dummyidle"; // String for the AssetName of the sprite

        public PlayerOne(PlayingState playstate) : base(playstate, AssetName)
        {
            playingState = playstate; // Give the playingState
            dodge = new Dodge(this); // Give this player a dodge, pass this player onto the dodge constructor
            staminaBar = new StaminaBar(this); // Give this player a staminaBar, pass this player onto the staminaBar constructor

            position.X = (Global.width / 2) - (texture.Width / 2); // Set the start X position of the player
            position.Y = (Global.height / 2) - (texture.Height / 2); // Set the start Y position of the player

            origin.X = texture.Width / 2; // Center the origin
            origin.Y = texture.Height / 2; // Center the origin

            velocity.X = 0; // Asume the player isn't moving
            velocity.Y = 0; // Asume the player isn't moving

            IsDead = false; // Start the game alive

            // Load all Animations
            LoadAnimation("PlayerSprites/Bryce/idle/WolfIdle@4x1", "idle", true, 0.2f);
            LoadAnimation("PlayerSprites/Bryce/walk/WolfWalk@4x1", "walk", true, 0.2f);
            LoadAnimation("PlayerSprites/Bryce/attack/WolfAttack@4x1", "attack", true, 0.2f);
            LoadAnimation("PlayerSprites/Bryce/hurt/WolfHurt@1x1","hurt", true, 1f);
        }

        public override void Reset()
        {
            base.Reset();

            velocity.X = 0; // Asume the player isn't moving
            velocity.Y = 0; // Asume the player isn't moving

            IsDead = false; // Start the game alive
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // If the player is not dead draw it
            if (!IsDead)
            {
                base.Draw(gameTime, spriteBatch);
            }
            else // If the player is dead do not draw it
            {
                visible = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            dodge.Update(gameTime);
            staminaBar.Update(gameTime);

            // If a player is slowed (due to staminaBurn) they get slowed down
            if (isSlowed)
            {
                xSpeed = xStartingSpeed / (int)staminaBar.outOfStaminaDecrease;
                ySpeed = yStartingSpeed / (int)staminaBar.outOfStaminaDecrease;
            }
            else if (!isSlowed) // If they aren't slowed, they have their base movement speed
            {
                xSpeed = xStartingSpeed;
                ySpeed = yStartingSpeed;
            }

            if (attacking)
            {
                PlayAnimation("attack");//plays the animation
                if (animations["attack"].SheetIndex == 2)//checks if it's at the "impact frame"
                {
                    attackcooldown = 30; // The cooldown of the Attack in frames
                    playingState.DeleteFirstHitbox(1); // Remove the first hitbox in a list from hitboxes from player One
                    playingState.AddPlayerAttackHitboxesLeft(1, this.Position); // Add a new attack hitbox for player One
                    MainGame.AssetManager.PlaySound("Sounds/soundEffects/attack"); // Play the attack soundEffect
                    staminaBar.UseStamina(20); // Use a certain amount of stamina when the player uses an attack

                }
                else
                {
                    attacking = false; // Set the boolean attacking to false
                }
                if (animations["attack"].SheetIndex == 3) // If the animation is on the third sprite (last sprite in this case)
                {
                    attacking = false; // Set the boolean attacking to false
                }
                else// needs this to stop the attack from breaking for unknown reasons
                {
                    attacking = false; // Set the boolean attacking to false
                }
            }
        }

        // Method to handle the input of PlayerOne specific
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (!IsDead) // If the player is not dead
            {
                // Move Left
                if (inputHelper.IsKeyDown(Keys.A))
                {
                    direction = "Left"; // Set the direction to left
                    velocity.X = -xSpeed + speedIncreaseX; // Change the X velocity
                    PlayAnimation("walk"); // Play the walk animation
                    Mirror = true; // Mirror the sprite so it is facing the left
                } 
                //Move Right
                else if (inputHelper.IsKeyDown(Keys.D))
                {
                    direction = "Right"; // Set the direction to right
                    velocity.X = xSpeed + speedIncreaseX; // Change the X velocity
                    PlayAnimation("walk"); // Play the walk animation
                    Mirror = false; // Set Mirror to false the sprite so it is facing the right
                }
                // If the player is not pressing the attack key and is not moving then play the idle animation
                else if (!inputHelper.IsKeyDown(Keys.T))
                {
                    PlayAnimation("idle"); // Play idle animation
                }

                // Move Up
                if (inputHelper.IsKeyDown(Keys.W))
                {
                    velocity.Y = -ySpeed;
                } 
                //Move down
                else if (inputHelper.IsKeyDown(Keys.S))
                {
                    velocity.Y = ySpeed;
                }
                
                
                //Dodge

                /* Input to perform the dodge to the left */
                if (inputHelper.KeyPressed(Keys.Q))
                {
                    if (dodge.dashAvailable) // Dodge can only be used if it is available
                    {
                        staminaBar.UseStamina(dodgeStaminaCost); // Take away stamina acording to the dodgeStaminaCost
                        dodge.UseDodge("Left"); // Dodge to the left
                    }
                }
                /* Input to perform the dodge to the right*/
                if (inputHelper.KeyPressed(Keys.E))
                {
                    if (dodge.dashAvailable) //Dodge can only be used if it is available
                    {
                        staminaBar.UseStamina(dodgeStaminaCost); // Take away stamina acording to the dodgeStaminaCost
                        dodge.UseDodge("Right"); // Dodge to the right
                    }
                }

                // If the player is not slowed down (burnt out) they can attack, otherwise they cannot
                if (!isSlowed)
                {
                    //Attack Left
                    if (inputHelper.KeyPressed(Keys.T) && (direction == "Left") && attackcooldown == 0 && !attacking)
                    {
                        attacking = true;//lets the game know the player is doing it's attacking animation to avoid spam

                    }
                    else if (direction == "Left") // If the player is facing left
                    {
                        playingState.DeleteFirstHitbox(1); // Remove the first hitbox in a list from hitboxes from player One
                    }

                    //Attack Right
                    if (inputHelper.IsKeyDown(Keys.T) && direction == "Right" && attackcooldown == 0 && !attacking)
                    {
                        attacking = true; // Set the boolean attacking to true
                        PlayAnimation("attack"); // Play animation Attack
                        if (animations["attack"].SheetIndex == 2)//checks if it's at the "impact frame"
                        {
                            attackcooldown = 30; // The cooldown of the Attack in frames
                            playingState.DeleteFirstHitbox(1); // Remove the first hitbox in a list from hitboxes from player One
                            playingState.AddPlayerAttackHitboxesRight(1, this.Position); // Add a new attack hitbox for player One
                            MainGame.AssetManager.PlaySound("Sounds/soundEffects/attack"); // Play the attack soundEffect
                            staminaBar.UseStamina(20); // Use a certain amount of stamina when the player uses an attack
                        }
                        else
                        {
                            attacking = false; // Set the boolean attacking to false
                        }
                        if (animations["attack"].SheetIndex == 3)//makes the enemy know it's done attacking on the final frame the third sprite (last sprite in this case)
                        {
                            attacking = false; // Set the boolean attacking to false
                        }
                        else// needs this to stop the attack from breaking for unknown reasons
                        {
                            attacking = false; // Set the boolean attacking to false
                        }
                    }
                    else if (direction == "Right") // If the player is facing right
                    {
                        playingState.DeleteFirstHitbox(1); // Remove the first hitbox in a list from hitboxes from player One
                    }
                }
                else
                {
                    playingState.DeleteFirstHitbox(1); // Remove the first hitbox in a list from hitboxes from player One

                }
            }
        }

        // Method for when the player needs to take damage
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage); // Take damage
            PlayAnimation("hurt"); // Play the hurt animation
            MainGame.AssetManager.PlaySound("Sounds/soundEffects/manhurt"); // Play the manHurt soundEffect 
        }
    }
}