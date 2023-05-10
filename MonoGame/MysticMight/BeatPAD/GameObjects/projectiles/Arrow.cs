using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatPAD
{
    public class Arrow : RotatingSpriteGameObject
    {
        PlayerParent player;//the local player obtained trough the constructor
        public int damage = 50;//decides the damage it deals to players
        public Arrow(Vector2 starposition, Vector2 startvelocity, PlayerParent player) : base("spr_arrow")
        {
            this.player = player;//sets the local player
            position = starposition;//sets the position it spawns at
            velocity = startvelocity;//sets the velocity it starts with
            origin = Center;
            AngularDirection = velocity;//makes it turned in the direction it's going
        }

        public override void Update(GameTime gameTime)
        {
            velocity = player.Position - this.velocity;//makes the arrow go towards the players position
            base.Update(gameTime);
        }

    }
}
