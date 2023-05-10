using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* <ClassSummary> 
 * Class made by Sam
 * Class to make floating text appear and disappear for polish 
 * Class is unfinished and never used
 * <EndClassSummary> */
namespace BeatPAD
{
    public class FloatingText : TextGameObject
    {
        /* Constructor that takes a string as text, a position vector, and a color var */
        public FloatingText(string textToDisplay, Vector2 _position, Color _color) : base("FloatingText")
        {
            text = textToDisplay; //Text to draw is given as argument
            position = _position; //position the text acoording to the argument
            color = _color; //Set the color of the text to the given color
        }

        /* Method to draw the floating text with */
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(this.spriteFont, text, position, color); //Draw the text
        }
    }
}
