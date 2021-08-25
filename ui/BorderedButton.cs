using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class BorderedButton : Button
    {
        public BorderedButton(string text, Action action) : base(text, action, text.Length + 2, 3)
        {
            Draw();
        }

        public override void Draw()
        {
            Print(1, 1, Text, Theme.TextColor);

            var borderColor = IsHovered ? Theme.HoveredColor : Theme.AccentColor;

            Border.Draw(this, borderColor);
        }
    }
}