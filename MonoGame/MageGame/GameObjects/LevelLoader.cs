using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MageGame.GameObjects
{
    public class LevelLoader : GameObject
    {
        public GameObjectList LevelOne = new GameObjectList();
        public GameObjectList LevelTwo = new GameObjectList();

        public GameObjectList platforms = new GameObjectList();

        public int currentlevel = 0;
        public int level = 0;

        public LevelLoader()
        {
            currentlevel = 1;
            ChangeLevel(currentlevel);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            LevelOne.Draw(gameTime, spriteBatch);
            LevelTwo.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentlevel != level)
            {
                ChangeLevel(currentlevel);
            }
        }

        public void ChangeLevel(int level)
        {
            this.level = level;
            switch (level)
            {
                case 0:

                    break;
                case 1:
                    LoadLevelOne();
                    break;
                default:
                    LoadLevelTwo();
                    break;
            }


        }

        void LoadLevelOne()
        {
            platforms.Add(new Platform("longfloor", new Vector2(0, Game1.Screen.Y - 100)));
            platforms.Add(new Platform("Floor", new Vector2(Game1.Screen.X / 2, Game1.Screen.Y - 175)));
            platforms.Add(new Platform("Floor", new Vector2(Game1.Screen.X / 2 + 150, Game1.Screen.Y - 225)));
            platforms.Add(new Platform("Floor", new Vector2(Game1.Screen.X - 64, Game1.Screen.Y - 225)));
            platforms.Add(new Platform("Floor", new Vector2(Game1.Screen.X - 128, Game1.Screen.Y - 225)));
            Spellbook spellbook = new Spellbook(new Vector2(320, Game1.Screen.Y - 170));
            Spellbook firebook = new Spellbook(new Vector2(530, Game1.Screen.Y - 170),"SpellBooks/firebook");
            LevelOne.Add(spellbook);
            LevelOne.Add(firebook);
            LevelOne.Add(platforms);
        }
        void LoadLevelTwo()
        {
            platforms.Add(new Platform("longfloor", new Vector2(0, Game1.Screen.Y - 100)));
            LevelTwo.Add(platforms);
        }
    }
}
