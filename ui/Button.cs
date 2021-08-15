using SadConsole.Input;
using System;

namespace Game.UI
{
    public class Button : SadConsole.Console
    {
        public string Text { get; }
        public Action Action { get; }

        public Button(string text, Action action) : base(text.Length, 1)
        {
            Action = action;
            Text = text;

            Print(0, 0, text);
        }

        protected override void OnMouseLeftClicked(MouseConsoleState state)
        {
            base.OnMouseLeftClicked(state);
            Action();
        }
    }
}