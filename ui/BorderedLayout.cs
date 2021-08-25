using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class BorderedLayout : BorderedLayout<SadConsole.Console>
    {
        public BorderedLayout(int width, int height, Theme theme = null) : base(width, height, theme)
        {
        }
    }

    public class BorderedLayout<T> : SadConsole.Console, IUIElement where T : SadConsole.Console
    {
        public T Containee { get; set; }
        public Theme Theme { get; }

        public BorderedLayout(int width, int height, Theme theme = null) : base(width, height)
        {
            Theme = theme ?? Theme.CurrentTheme;
            Draw();
        }

        public TAdded Add<TAdded>(Func<int, int, TAdded> consoleConstructor) where TAdded : T
        {
            var containee = consoleConstructor(Width - 2, Height - 2);
            containee.Position = new Point(1, 1);
            containee.Parent = this;
            Containee = containee;
            return containee;
        }

        public void Draw()
        {
            Border.Draw(this, Theme.AccentColor);
        }
    }
}