using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MageGame.GameObjects;
using System.Diagnostics;
using System.Linq;

namespace MageGame.GameStates
{
    class PlayingState : GameObjectList
    {
        SpriteGameObject background;
        Player player;
        LevelLoader levelLoader;
        GameObjectList playerprojectiles;
        Enemy enemy;
        public float timer;
        public PlayingState()
        {
            background = new SpriteGameObject("background");
            player = new Player();
            levelLoader = new LevelLoader();
            playerprojectiles = new GameObjectList();
            enemy = new Enemy(new Vector2(Game1.Screen.X - 128, Game1.Screen.Y - 300));



            Add(background);
            Add(levelLoader);
            Add(playerprojectiles);
            Add(player);
            Add(enemy);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
        public override void HandleInput(InputHelper inputHelper)
        {

            if (inputHelper.KeyPressed(Keys.E))
            {
                Debug.WriteLine("FIRE!");
                if (!player.Mirror)
                {
                    playerprojectiles.Add(new Fireball(player.Position + new Vector2(2, -16), new Vector2(250, 0)));
                }
                else if (player.Mirror)
                {
                    Fireball fireball = new Fireball(player.Position + new Vector2(2, -16), new Vector2(-250, 0));
                    fireball.Mirror = true;
                    playerprojectiles.Add(fireball);
                }
            }
            if (inputHelper.KeyPressed(Keys.Q))
            {

                playerprojectiles.Add(new FireAura(player.Position));
                if (timer > 3)
                {
                    foreach (FireAura aura in playerprojectiles.Children.OfType<FireAura>())
                    {
                        playerprojectiles.Remove(aura);
                    }
                }

            }
            base.HandleInput(inputHelper);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(player.Position.X > Game1.Screen.X)
            {
                Debug.WriteLine(levelLoader.level);
                levelLoader.currentlevel++;
                player.Position = Vector2.Zero;
            }

            foreach (Platform platform in levelLoader.platforms.Children)
            {
                if (player.CollidesWith(platform))
                {
                    player.stopFalling();
                }
                if (enemy.CollidesWith(platform))
                {
                    enemy.stopFalling();
                }
            }

            foreach(Spellbook spellbook in levelLoader.LevelOne.Children.OfType<Spellbook>())
            {
                if (player.CollidesWith(spellbook))
                {
                    spellbook.Visible = false;
                }
            }

            foreach (Fireball fireball in playerprojectiles.Children.OfType<Fireball>())
            {
                if (fireball.CollidesWith(enemy))
                {
                    fireball.Visible = false;
                    enemy.Visible = false;
                }
            }
            foreach (FireAura aura in playerprojectiles.Children.OfType<FireAura>())
            {
                if (aura.CollidesWith(enemy))
                {
                    enemy.burning = true;
                }
            }

        }
    }
}
