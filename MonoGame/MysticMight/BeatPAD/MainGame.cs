using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BeatPAD.GameStates;

namespace BeatPAD
{
    public class MainGame : GameEnvironment
    {
        // ScreenStates
        public const string PLAYINGSTATE = "PlayingState";
        public const string TITLESTATE = "title";
        public const string GAMEOVERSTATE = "GameOver";
        public const string WINSTATE = "WinState";
        public const string CONTROLSSTATE = "ControlScreen";

        public MainGame()
        {
            this.Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            IsMouseVisible = false;
            //FullScreen = true;

            //add the gamestates here
            PlayingState play = new PlayingState();
            gameStateManager.AddGameState(PLAYINGSTATE, play);
            TitleState title = new TitleState();
            gameStateManager.AddGameState(TITLESTATE, title);
            GameoverState gameover = new GameoverState();
            gameStateManager.AddGameState(GAMEOVERSTATE, gameover);
            WinState winstate = new WinState();
            gameStateManager.AddGameState(WINSTATE, winstate);
            ControlsState controls = new ControlsState();
            gameStateManager.AddGameState(CONTROLSSTATE, controls);

            AssetManager.PlayMusic("Sounds/titleSong");
            gameStateManager.SwitchTo(TITLESTATE);
        }

        protected override void Initialize()
        {
            //size of the screen
            screen = new Point(1280, 720);
            ApplyResolutionSettings();

            //initializing Global
            Global.GraphicsDevice = GraphicsDevice;
            Global.content = Content;

            base.Initialize();
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}