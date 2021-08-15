using SadConsole.Input;
using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class Button : SadConsole.Console
    {
        public string Text { get; }

        public Action Action { get; }

        public Color DefaultColor { get; }= Color.White;

        public Color HoveredColor { get;}= Color.Red;

        public bool IsHovered {get; set;}= false;


        public Button(string text, Action action) : base(text.Length + 2, 3)
        {
            Action = action;
            Text = text;

            Print(1, 1, text);
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

        public void Draw()
        {

            var borderColor = IsHovered ? HoveredColor : DefaultColor;

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                   if ( x == 0 && y == 0 )
                    {
                        SetGlyph (x, y, 218, borderColor);
                    }
                    else if ( x == Width-1 && y == 0)
                    {
                        SetGlyph (x, y, 191, borderColor);
                    }
                    else if ( x== 0 && y == Height-1)
                    {
                        SetGlyph (x, y, 192, borderColor);
                    }
                    else if ( x == Width-1 && y == Height-1)
                    {
                        SetGlyph (x, y, 217, borderColor);
                    }
                    else if (y == 0 || y== Height-1)
                    {
                        SetGlyph (x, y, 196, borderColor);
                    }
                    else if (x == 0 || x== Width-1)
                    {
                        SetGlyph (x, y, 179, borderColor);
                    }

                }

                
            }
        }
    }
}