using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD.GameStates
{
    public class WinState : GameObjectList
    {
        SpriteGameObject Winscreen;
        public WinState()
        {
            Winscreen = new SpriteGameObject("UI/winscreen");
            Add(Winscreen);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.AnyKeyPressed)
            {
                MainGame.AssetManager.PlayMusic("Sounds/titleSong");
                MainGame.GameStateManager.SwitchTo(MainGame.TITLESTATE);
            }
        }

    }
}
