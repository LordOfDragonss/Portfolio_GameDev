using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD
{
    public class GameoverState : GameObjectList
    {
        SpriteGameObject gameover;
        public GameoverState()
        {
            gameover = new SpriteGameObject("UI/gameover");
            Add(gameover);
            
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
