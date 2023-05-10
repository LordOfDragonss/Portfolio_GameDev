using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD
{
    class Hitbox : SpriteGameObject
    {

        public int id;
        public Enemy enemy;

        public Hitbox(int id, Vector2 hitboxPosition) : base("pinkCube50-50")//spawns a hitbox with an id selected (primarily used for the player attack hitboxes)
        {
            this.id = id;
            this.enemy = null;
            position = hitboxPosition;
            visible = false;
            origin = Center;
        }

        public Hitbox(Enemy enemy, Vector2 hitboxPosition) : base("pinkCube50-50")//spawns a hitbox with an enemy attached (primarily used for the enemy attack hitboxes)
        {
            this.id = 0;
            this.enemy = enemy;
            position = hitboxPosition;
            visible = false;
            origin = Center;
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
