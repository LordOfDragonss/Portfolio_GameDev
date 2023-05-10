using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD.GameStates
{
    public class ControlsState : GameObjectList
    {
        SpriteGameObject controls;

        public ControlsState()
        {
            controls = new SpriteGameObject("UI/Controls");
            Add(controls);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.AnyKeyPressed)
            {
                MainGame.GameStateManager.SwitchTo(MainGame.PLAYINGSTATE);
                MainGame.AssetManager.PlayMusic("Sounds/bensound-badass");
                Microsoft.Xna.Framework.Media.MediaPlayer.Volume = 0.4f;
            }
        }


    }
}
