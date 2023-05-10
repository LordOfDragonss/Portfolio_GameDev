using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MageGame.GameStates;
using MageGame.GameObjects;

namespace MageGame
{
    public class Game1 : GameEnvironment
    {      
        protected override void LoadContent()
        {
            base.LoadContent();

            screen = new Point(800, 600);
            ApplyResolutionSettings();

            // TODO: use this.Content to load your game content here
            PlayingState playing = new PlayingState();
            gameStateManager.AddGameState("play", playing);
            gameStateManager.SwitchTo("play");

        }

    }
}
