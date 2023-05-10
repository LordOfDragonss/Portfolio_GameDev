using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeatPAD
{
    public class PlayingState : GameObjectList
    {
        // Declare the ground
        public Ground ground;

        // Declare both players
        public PlayerOne playerOne;
        public PlayerTwo playerTwo;

        // Declare both healthBars
        private HealthBar healthBar1;
        private HealthBar healthBar2;

        // Declare the pointer
        private Pointer pointer;

        // Declare the levelLoader
        private LoadLevel levelLoader;
        public Wave currentWave; //Track the currentwave

        // Declare all hitboxes GameObjectLists
        public GameObjectList enemyAttackHitboxes;
        public GameObjectList playerAttackHitboxes;

        public GameObjectList enemyProjectiles;

        public PlayingState()
        {
            // Initialize the ground
            ground = new Ground(new Vector2(Global.width / 2, Global.height / 2));

            // Initialize the players
            playerOne = new PlayerOne(this);
            playerTwo = new PlayerTwo(this);

            // Initialize the healthBars
            healthBar1 = new HealthBar(new Vector2(Global.width * 0.10f, 50), playerOne); // Change the X to give a different width size to the healthbar
            healthBar2 = new HealthBar(new Vector2(Global.width * 0.6f, 50), playerTwo); // Change the X to give a different width size to the healthbar

            // Initialize the Pointer that helps the player move to the next level
            pointer = new Pointer();

            // Initialize all hitboxes
            playerAttackHitboxes = new GameObjectList();
            enemyAttackHitboxes = new GameObjectList();

            enemyProjectiles = new GameObjectList();

            // Initialize the levelLoader
            levelLoader = new LoadLevel(this);
            currentWave = levelLoader.wave1; //Set the currentWave to wave 1 when starting the game


            // ADD ALL GameObjects
            // Add the Ground
            Add(ground);

            // Add the players
            Add(playerOne);
            Add(playerTwo);

            // Add the healthBars of the players
            Add(healthBar1);
            Add(healthBar2);

            // Add the pointer
            Add(pointer);

            // ADD ALL GameObectList
            // Add all the hitboxes
            Add(playerAttackHitboxes);
            Add(enemyAttackHitboxes);

            Add(enemyProjectiles);

            // Add the levelLoader
            Add(levelLoader);
            levelLoader.SwitchToLevel(levelLoader.nextLevelCount);
        }

        public override void Reset()
        {
            base.Reset();
            children.Clear();
            enemyAttackHitboxes.Children.Clear();


            // ADD ALL GameObjects
            // Add the Ground
            Add(ground);

            // Add the players
            Add(playerOne);
            Add(playerTwo);

            // Add the healthBars of the players
            Add(healthBar1);
            Add(healthBar2);

            // Add the pointer
            Add(pointer);

            // Add the levelLoader
            Add(levelLoader);

            // ADD ALL GameObectList
            // Add all the hitboxes
            Add(playerAttackHitboxes);
            Add(enemyAttackHitboxes);

            Add(enemyProjectiles);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            // Debug key to test things
            if (inputHelper.IsKeyDown(Keys.Enter))
            {
                playerOne.currentHealth -= 10;
                playerTwo.currentHealth -= 10;
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Collision check for the enemies projectile
            foreach (Arrow arrow in enemyProjectiles.Children.ToList())
            {
                if (arrow.CollidesWith(playerTwo)) // If it hits player two
                {
                    RemoveArrow(arrow);
                    playerTwo.TakeDamage(arrow.damage); // Remove the health from the player that got hit
                }
                else if (arrow.CollidesWith(playerOne)) // If it hits player one
                {
                    RemoveArrow(arrow);
                    playerOne.TakeDamage(arrow.damage); // Remove the health from the player that got hit
                }
                if (arrow.Position.X > MainGame.Screen.X || arrow.Position.Y > MainGame.Screen.Y) // If it goes out of the screen
                {
                    RemoveArrow(arrow);
                }
            }

            // Check if the enemies of the current wave are dead. And if they are dead set the pointer to visible.
            if (levelLoader.CheckIfEnemiesAreDead(currentWave))
            {
                pointer.Visible = true;
            }

            // Game over when both players are out of health
            if (playerOne.currentHealth <= 0 && playerTwo.currentHealth <= 0)
            {
                MainGame.GameStateManager.SwitchTo(MainGame.GAMEOVERSTATE);
                Reset();
                levelLoader.ResetWaves();
            }

            // Remove player one and their staminaBar when they are out of health
            if (playerOne.currentHealth <= 0)
            {
                playerOne.IsDead = true;
            }

            // Remove player two and their staminaBar when they are out of health
            if (playerTwo.currentHealth <= 0)
            {
                playerTwo.IsDead = true;
            }

            foreach (Hitbox EnemyHitbox in enemyAttackHitboxes.Children)
            {
                // PlayerOne takes damage if it collides with a enemyHitbox
                if (EnemyHitbox.CollidesWith(playerOne))
                {
                    // Damage Amount
                    playerOne.TakeDamage(50);
                }
                // PlayerTwo takes damage if it collides with a enemyHitbox
                if (EnemyHitbox.CollidesWith(playerTwo))
                {
                    // Damage Amount
                    playerTwo.TakeDamage(50);
                }
            }

            foreach (Hitbox playerHitbox in playerAttackHitboxes.Children)
            {
                /* Disable enemies from wave the current loaded wave */
                foreach (Enemy enemy in currentWave.Children.ToList())
                {
                    if (playerHitbox.CollidesWith(enemy))
                    {
                        enemy.TakeDamage(1);
                    }
                    if (enemy.Health <= 0)
                    {
                        currentWave.Children.Remove(enemy);

                    }
                }

                // Let the enemies take damage if the collide with a playerHitbox
                foreach (Enemy enemy in Children.OfType<Enemy>())
                {
                    if (playerHitbox.CollidesWith(enemy))
                    {
                        enemy.TakeDamage(1);
                    }
                }
            }

            /* Check if there is a healthPack in the list, then check if they collide with a player */
            foreach (HealthPickup healthPickup in Children.OfType<HealthPickup>().ToList())
            {
                if (playerOne.Sprite != null && playerTwo.Sprite != null) //Check if the sprite is not null to prevent Null pointer exep.
                {
                    // If playerOne picks up the healthpack they get the health bonus
                    if (healthPickup.CollidesWithPlayer(playerOne))
                    {
                        healthPickup.CollectHealthPack(playerOne);
                    } // Else if player two picks it up, they get the bonus
                    else if (healthPickup.CollidesWithPlayer(playerTwo))
                    {
                        healthPickup.CollectHealthPack(playerTwo);
                    }
                }
            }

            /* Move to the next wave / level if all enemies are dead and a player is on the right end of the screen */
            if (levelLoader.CheckIfEnemiesAreDead(currentWave))
            {
                if (levelLoader.CheckIfPlayerWantsToGoToNextLevel(playerOne) || levelLoader.CheckIfPlayerWantsToGoToNextLevel(playerTwo))
                {
                    playerOne.Position = new Vector2(50, playerOne.Position.Y);
                    playerTwo.Position = new Vector2(50, playerTwo.Position.Y);
                    levelLoader.SwitchToLevel(levelLoader.nextLevelCount);
                    MainGame.AssetManager.PlaySound(levelLoader.NextLevelSoundEffect);
                    pointer.Visible = false;
                }

            }
            base.Update(gameTime);
        }

        // Method that removes the arrow from the list so the enemy that shot it can shoot again
        public void RemoveArrow(Arrow arrow)
        {
            enemyProjectiles.Children.Remove(arrow);
        }

        // Adds the hitboxes for the players to the playingstate hitbox list
        public void AddPlayerAttackHitboxesLeft(int Playerid, Vector2 hitboxPosition)
        {
            playerAttackHitboxes.Add(new Hitbox(Playerid, new Vector2(hitboxPosition.X - 100, hitboxPosition.Y - 150)));
        }

        // Adds the hitboxes for the players to the playingstate hitbox list
        public void AddPlayerAttackHitboxesRight(int Playerid, Vector2 hitboxPosition)
        {
            playerAttackHitboxes.Add(new Hitbox(Playerid, new Vector2(hitboxPosition.X + 100, hitboxPosition.Y - 150)));
        }

        // Adds the hitboxes for the enemies to the playingstate hitbox list
        public void AddEnemyAttackHitboxesLeft(Enemy enemy, Vector2 hitboxPosition)
        {
            enemyAttackHitboxes.Add(new Hitbox(enemy, new Vector2(hitboxPosition.X - 75, hitboxPosition.Y - 75)));
        }

        // Adds the hitboxes for the enemies to the playingstate hitbox list
        public void AddEnemyAttackHitboxesRight(Enemy enemy, Vector2 hitboxPosition)
        {
            enemyAttackHitboxes.Add(new Hitbox(enemy, new Vector2(hitboxPosition.X + 75, hitboxPosition.Y - 75)));
        }

        // Deletes the first hitbox from the list
        public void DeleteFirstHitbox(int id) // Id from the player
        {
            if (playerAttackHitboxes.Children.Count != 0) // Check if the list is empty
            {
                if (playerAttackHitboxes.Children.OfType<Hitbox>().Where(x => x.id == id).Any()) // Check if the id of the list is the same as the one from the player
                {
                    Hitbox hbox = playerAttackHitboxes.Children.OfType<Hitbox>().Where(x => x.id == id).First(); // Select the first hitbox with the id
                    playerAttackHitboxes.Remove(hbox); // Remove the hitbox that was selected
                }
            }
        }
    }
}