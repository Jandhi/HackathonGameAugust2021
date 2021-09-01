using Microsoft.Xna.Framework.Audio;

namespace Game.Audio
{
    public class AudioManager
    {
        public static SoundEffect Click { get; set; }
        public static SoundEffect Tap { get; set; }

        public static void Init()
        {
            var loader = SadConsole.Game.Instance.Content;

            Click = loader.Load<SoundEffect>("Click 1");
            Tap = loader.Load<SoundEffect>("Tap 1");
        }
    }
}