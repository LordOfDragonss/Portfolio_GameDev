using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* <ClassSummary>
 * Class made by Sam
 * Players can refill their health by picking up a healthpack made from this class
 * Has access to the playingstate to get variables and access to the GoL
 * Has collision with players only
 * <EndClassSummary> */

namespace BeatPAD
{
    public class HealthPickup : SpriteGameObject
    {
        protected PlayingState playingState; //Acces to the playingstate to access variables and GoL
        public float healthPackAmount; //Float to decide how much health a player gets back when picking up a healthpack

        /* Constructor with an argument for the pllayingstate and the position where it needs to be spawned */
        public HealthPickup(PlayingState _playingState, Vector2 _position) : base("health pack") //Base spr called health pack, resembling a health pack
        {
            origin = Center; //Set the origin to the center
            scale = 0.4f; //Scale the sprite down to a fitting size

            playingState = _playingState;
            position = _position; //Set the position to the vector2 of the argument

            healthPackAmount = playingState.playerOne.maxHealth; //The bonus a player gets when picking up a health pack is the maxhealth a player can have, full hp 
        }

        /* Method of type bool to detect collision between healthpack and players
         * Gets called in playingstate Update() method */
        public Boolean CollidesWithPlayer(PlayerParent _player)
        {
            if (this.CollidesWith(_player))
            {
                return true; //Is true if a healthpack collides with a player
            }
            else
            {
                return false; //Else returns false
            }
        }

        /* Method to add the healthpack to the player who picks it up 
         * Calls delete method */
        public void CollectHealthPack(PlayerParent _player)
        {
            DeleteHealthPack(); //Calls method to delete the healthpack from the playingstate so it can only be picked up once
            _player.currentHealth = healthPackAmount;
        }

        /* Method to call when a player picks up a healthpack, healthpack gets removed after picking it up */
        public void DeleteHealthPack()
        {
            playingState.Children.Remove(this); //Remove out of the GoL to stop it's  draw and update method
        }
    }
}