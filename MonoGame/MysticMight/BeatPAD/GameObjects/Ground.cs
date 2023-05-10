using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* <ClassSummary>
 * Class made by Sam
 * Class for the background of the game
 * Needs a sprite and to be fitted in the window
 * <EndClassSummary> */
namespace BeatPAD
{
    public class Ground : SpriteGameObject
    {
        public Vector2 offset; //Variable to adjust the sprite so it fits in the screen

        /* Constructor that takes a vector2 as argument to declare the position */
        public Ground(Vector2 _position) : base("ground_1_png")
        {
            //Setting the SprGameObject variabales
            origin = Center; //Set origin to center
            scale = 0.55f; //Scale down to fit the sprite in the window
            position = _position; //Set the position to the given vector2

            //Setting the offset to help fit the sprite in the window
            offset = new Vector2(offset.X, offset.Y)
            {
                Y = 100
            };

            //Fit the sprite in the screen
            position.Y = _position.Y - offset.Y;
        }
    }
}
