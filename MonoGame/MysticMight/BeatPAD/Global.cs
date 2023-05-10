using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeatPAD
{
    internal static class Global
    {
        public static ContentManager content;
        public static Rectangle screenRect;
        public static KeyboardState keys;
        public static int width, height;
        public static SpriteBatch spriteBatch;
        private static GraphicsDevice _graphicsDevice;

        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return _graphicsDevice;
            }

            set
            {
                _graphicsDevice = value;
                screenRect = new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
                width = screenRect.Width;
                height = screenRect.Height;
            }
        }

        private static Random _rGen = new Random();

        public static int Random(int lower, int upper)
        {
            return _rGen.Next(lower, upper);
        }
    }
}