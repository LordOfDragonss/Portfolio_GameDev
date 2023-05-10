using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* <ClassSummary>*
 * Made by Mike and Sam
 * Class to make waves of enemies to load into the playingstate
 * Enemies only get loaded in through the use of this class
 * Is a GoL to hold melee and ranged enemies
 * Has access to the playingstate to add enemies in the screen
 * <EndClassSummary> */

namespace BeatPAD
{
    public class Wave : GameObjectList
    {
        PlayingState playingState;
        public int meleeEnemies; //Int to track how many melee enemies want to be added to a wave
        public int rangedEnemies; //Int to track how many ranged enemies want to be added to a wave

        /* Constructor that takes two ints as arguments to see how many enemies want to be added */
        public Wave(int meleeEnemyAmount, int rangedEnemyAmount, PlayingState _playingState)
        {
            playingState = _playingState; //Set the playingstate
            meleeEnemies = meleeEnemyAmount; //Set the class variable to the given amount of melee enemies 
            rangedEnemies = rangedEnemyAmount; //Set the class variable to the amount of the given argument


            /* For loop to add melee enemies to the wave */
            for (int i = 0; i < meleeEnemies; i++) //Make as much melee enemies as wanted above 
            {
                MeleeEnemy enemy = new MeleeEnemy(playingState, Global.Random(8, 13), Global.Random(1, 8)); ; //Make a new enemy on a random position
                Add(enemy); //Add the made enemy to the wave
            }

            /* For loop to add ranged enemies to the wave */
            for (int i = 0; i < rangedEnemies; i++) //Make as much ranged enemies as wanted above 
            {
                RangedEnemy rangedEnemy = new RangedEnemy(playingState, Global.Random(8, 13), Global.Random(1, 8));//Make a new enemy on a random position
                Add(rangedEnemy); //Add the made ranged enemy to the wave
            }
        }

        /* Method to reset the enemies of the waves, used when finishing the game and restarting from begin
         * Same as cnstructor */
        public override void Reset()
        {
            /* For loop to add melee enemies to the wave */
            for (int i = 0; i < meleeEnemies; i++) //Make as much melee enemies as wanted above 
            {
                MeleeEnemy enemy = new MeleeEnemy(playingState, Global.Random(8, 13), Global.Random(1, 8)); //Make a new enemy on a random position
                Add(enemy); //Add the made enemy to the wave
            }

            /* For loop to add ranged enemies to the wave */
            for (int i = 0; i < rangedEnemies; i++) //Make as much ranged enemies as wanted above 
            {
                RangedEnemy rangedEnemy = new RangedEnemy(playingState, Global.Random(8, 13), Global.Random(1, 8));//Make a new enemy on a random position
                Add(rangedEnemy); //Add the made ranged enemy to the wave
            }
        }
    }
}
