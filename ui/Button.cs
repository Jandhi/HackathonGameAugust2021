using SadConsole.Input;
using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{

    public class Button : SadConsole.Console, IUIElement
    {
        public string Text { get; }
        public Action Action { get; }
        public Theme Theme { get; }
        public bool IsHovered { get; set; } = false;

        public Button(ColoredString text, Action action, int width = -1, int height = -1) : this(text.Contents, action, width == -1 ? ColoredString.GetLength(text.Contents) : width, height == -1 ? 1 : height)
        {
            Theme = Theme.Copy();
            Theme.TextColor = text.Foreground;
            Theme.HoveredColor = new Color(
                r: Math.Min(255, text.Foreground.R + 100),
                g: Math.Min(255, text.Foreground.G + 100),
                b: Math.Min(255, text.Foreground.B + 100)
            );
        }

        public Button(string text, Action action, Theme theme = null) : this(text, action, text.Length, 1, theme)
        {}


        public Button(string text, Action action, int width, int height, Theme theme = null) : base(width, height)
        {
            Theme = theme ?? Theme.CurrentTheme;
            Action = action;
            Text = text;
            UsePrintProcessor = true;
            Draw();
        }

        protected override void OnMouseLeftClicked(MouseConsoleState state)
        {
            base.OnMouseLeftClicked(state);
            Action();
        }

        protected override void OnMouseEnter(MouseConsoleState state)
        {
            IsHovered = true;
            Draw();
        }

        protected override void OnMouseExit(MouseConsoleState state)
        {
            IsHovered = false;
            Draw();
        }

        public virtual void Draw()
        {
            Clear();
            Print(0, 0, Text, IsHovered ? Theme.HoveredColor : Theme.TextColor);
        }
    }
}