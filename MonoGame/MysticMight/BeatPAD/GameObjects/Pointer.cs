using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD
{
    public class Pointer : SpriteGameObject
    {
        public Pointer(string assetname = "Pointer") : base(assetname)
        {
            position = new Vector2(Global.width - 400, 0);
            visible = false;
        }

    }
}
