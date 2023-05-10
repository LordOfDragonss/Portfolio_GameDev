using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD
{
    public class TitleState : GameObjectList
    {
        SpriteGameObject titlescreen;

        public TitleState()
        {
            titlescreen = new SpriteGameObject("UI/titlescreen");

            Add(titlescreen);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.AnyKeyPressed)
            {
                MainGame.GameStateManager.SwitchTo(MainGame.CONTROLSSTATE);
            }
        }
    }
}
