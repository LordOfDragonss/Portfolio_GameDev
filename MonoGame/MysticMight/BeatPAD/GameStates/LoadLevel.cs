using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* <ClassSummary>
 * Class made by Sam
 * This class is used to switch between levels and to make and keep track of waves
 * Adds a wave to the playingstate that contains x amount of enemies 
 * Adds healthpacks to the playingstate
 * Automatically switches to the next level if the wave is over (and player positions)
 * initializes and resets waves used to make a level
 * Makes the game loop work properly 
 * <EndClassSummary> */
namespace BeatPAD
{
    public class LoadLevel : GameObject
    {
        protected PlayingState playingState; //To gain acces to the playingState variables, and to add waves and healthpacks
        public HealthPickup healthPickup; //Make a healthpickup to spawn
        private int loadingLevel; //Decides the level that needs to be loaded
        public int nextLevelCount; //Decides what the next level is 
        public string NextLevelSoundEffect; //Sound effect to call when moving to a new level


        /* Make waves of enemies to load in a level */
        public Wave wave1;
        public Wave wave2;
        public Wave wave3;

        public LoadLevel(PlayingState _playingState)
        {
            this.playingState = _playingState;
            nextLevelCount = 1; //Set nextlevelcount to 1 to immediatly load level 1 when starting the game for the first time

            /* Make three new waves with x melee enemies and y ranged enemies */
            wave1 = new Wave(2, 1, playingState);
            wave2 = new Wave(4, 2, playingState);
            wave3 = new Wave(6, 3, playingState);

            NextLevelSoundEffect = "Sounds/Soundeffects/NextLevel";
        }

        /* Method to switch to a level; _level is the level you want to load in the playingState */
        public void SwitchToLevel(int _level)
        {
            loadingLevel = _level; //Passes the given argument into the loadingLevel variable

            if (loadingLevel == 1)
            {
                Level1(); //Call level 1
            }

            if (loadingLevel == 2)
            {
                Level2(); //Call level 2
            }

            if (loadingLevel == 3)
            {
                Level3(); //Call level 3
            }
            if (loadingLevel == 4)
            {
                WinScreen(); //Call the win screen
            }
        }

        /* Method to call when calling a new level;
         * Removes the current wave from the playingState
         * Sets the currentWave to the argument that's given when calling the method
         * Adds the given argument as wave */
        public void StartNewLevel(Wave newWave)
        {
            playingState.Remove(playingState.currentWave); //Remove the current wave that is active from the playingstate
            playingState.currentWave = newWave; //Set the currentwave to the newly given wave to now track this wave
            playingState.Add(newWave); //Add the newly given wave to the playingstate to display enemies and healthpacks
        }

        /* Three methods to describe three individual levels 
         * Calls the StartNewLevel method to add a new wave
         * Adds a healthpickup on a given location in this level
         * Sets the nextLevelCount +1 to make sure the next level is getting called when this level is over */
        public void Level1()
        {
            StartNewLevel(wave1); //Level 1 uses wave 1
            playingState.Add(new HealthPickup(playingState, new Vector2(200, 500))); //Add a healthpack to the playingstate
            nextLevelCount++; //Set the counter +1 load the next level when finished
        }

        public void Level2()
        {
            StartNewLevel(wave2); //Level 2 uses wave 2
            playingState.Add(new HealthPickup(playingState, new Vector2(600, 300))); //Add a healthpack to the playingstate
            nextLevelCount++; //Set the counter +1 load the next level when finished
        }

        public void Level3()
        {
            StartNewLevel(wave3);
            playingState.Add(new HealthPickup(playingState, new Vector2(800, 700))); //Add a healthpack to the playingstate
            nextLevelCount++; //Set the counter +1 load the winscreen when finished
        }

        /* Method to call after finishing wave 3 to indicate players have won;
         * Win screen removes the current wave and adds the winscreen */
        public void WinScreen()
        {
            playingState.Remove(playingState.currentWave); //Remove the currentwave from the playingstate
            playingState.Reset(); //Reset the whole playingstate to start over 
            ResetWaves(); //Calls method to reset the waves

            MainGame.GameStateManager.SwitchTo(MainGame.WINSTATE); //Switch to the Winstate
            MainGame.AssetManager.PlayMusic("Sounds/winsound"); //Play a winningsound
        }

        /* Method to reset the waves after finishing the game to make sure the game loop works properly*/
        public void ResetWaves()
        {
            /* Clear all three waves to empty them*/
            wave1.Children.Clear();
            wave2.Children.Clear();
            wave3.Children.Clear();

            /* Call the reset method to refill them with enemies*/
            wave1.Reset();
            wave2.Reset();
            wave3.Reset();

            nextLevelCount = 1; //Set the count to 1 to start over from level 1 after exiting the winscreen
            SwitchToLevel(1); //Switch to level 1 when displaying the winscreen to immediatly load level 1
        }

        /* Method of type Bool to see if all the enemies from the current wave are dead (removed from the list) *
         * Used to check if the level is completed */
        public Boolean CheckIfEnemiesAreDead(Wave _wave)
        {
            if (_wave.Children.Count == 0)
            {
                return true; //Returns true if the wave is empty
            }
            else
            {
                return false; //Else returns false
            }
        }

        /* Method of type Bool to check if players want to go to the next level 
         * Used to move to the next level if the enemies of the currentwave are dead
         * PlayerParent argument to check both players easily */
        public Boolean CheckIfPlayerWantsToGoToNextLevel(PlayerParent _player)
        {
            if (_player.GlobalPosition.X > Global.width - _player.texture.Width / 2)
            {
                return true; //Is true if a player moves to the right edge of the screen
            }
            else
            {
                return false; //Is false if the player is not to the right edge of the screen
            }
        }
    }
}
