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

        public Button(string text, Action action, Theme theme = null) : this(text, action, text.Length, 1, theme)
        {}


        public Button(string text, Action action, int width, int height, Theme theme = null) : base(width, height)
        {
            Theme = theme ?? Theme.CurrentTheme;
            Action = action;
            Text = text;
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
            Draw ();
        }

        protected override void OnMouseExit(MouseConsoleState state)
        {
            IsHovered = false;
            Draw ();
        }

        public virtual void Draw()
        {
            Print(0, 0, Text, IsHovered ? Theme.HoveredColor : Theme.TextColor);
        }
    }
}