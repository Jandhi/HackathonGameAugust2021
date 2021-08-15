using SadConsole.Input;
using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class Button : SadConsole.Console, IDrawable
    {
        public string Text { get; }
        public Action Action { get; }
        public Color DefaultColor { get; }= Color.White;
        public Color HoveredColor { get; } = Color.Red;
        public bool IsHovered { get; set; } = false;

        public Button(string text, Action action) : base(text.Length, 1)
        {
            Action = action;
            Text = text;
            Draw();
        }


        public Button(string text, Action action, int width, int height) : base(width, height)
        {
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
            Print(0, 0, Text, IsHovered ? HoveredColor : DefaultColor);
        }
    }
}