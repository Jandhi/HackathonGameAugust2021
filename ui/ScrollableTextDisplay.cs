using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class ScrollableTextDisplay : SadConsole.Console, IUIElement
    {
        public Theme Theme { get; }
        private string text = "";
        public string Text 
        { 
            get 
            {
                return text;
            }
            set 
            {
                text = value;
                Lines = CalculateLines();
            } 
        }
        public List<string> Lines { get; set; }
        public int ScrollPosition { get; set; } = 0;
        public int MaxScrollPosition => Math.Max(0, Lines.Count - Height - 1); 

        public Button ScrollUpButton { get; set; }
        public Button ScrollDownButton { get; set; }

        public ScrollableTextDisplay(int width, int height, string text, Theme theme = null) : base(width, height)
        {
            Text = text;
            UsePrintProcessor = true; // allow for coloring
            Theme = theme ?? Theme.CurrentTheme;
            
            SetupButtons();
            Draw();
        }

        public void SetupButtons()
        {
            var buttonTheme = new Theme(Theme);
            buttonTheme.TextColor = Theme.MainColor;

            ScrollUpButton = new Button("^", () => {
                if(ScrollPosition > 0) ScrollPosition--;
                Draw();
            }, buttonTheme);
            ScrollUpButton.Position = new Point(Width - 1, 0);
            ScrollUpButton.Parent = this;

            ScrollDownButton = new Button("v", () => {
                if(ScrollPosition < MaxScrollPosition) ScrollPosition++;
                Draw();
            }, buttonTheme);
            ScrollDownButton.Position = new Point(Width - 1, Height - 1);
            ScrollDownButton.Parent = this;
        }

        public void Draw()
        {
            for(var i = 0; i < Height && ScrollPosition + i < Lines.Count; i++)
            {
                Print(0, i, Lines[ScrollPosition + i]);
            }

            for(var y = 1; y < Height - 1; y++) 
            {
                Print(Width - 1, y, ".", Theme.MainColor);
            }

            var heightRatio = (ScrollPosition * (Height - 3) / MaxScrollPosition);
            if(ScrollPosition > 0 && heightRatio == 0) // Don't show highest scroll for non-highest line
            {
                heightRatio = 1;
            }
            if(ScrollPosition != MaxScrollPosition && heightRatio == Height - 3) // Don't show lowest scroll for non-lowest line
            {
                heightRatio = Height - 4;
            }
            Print(Width - 1, heightRatio + 1, "X", Theme.MainColor);
        }

        private List<string> CalculateLines()
        {

            var lines = new List<string>();
            var line = "";
            var maxLineLength = Width - 1;

            void newLine() 
            {
                lines.Add(line);
                line = "";
            }

            foreach (var character in Text)
            {
                if(character == '\n')
                {
                    newLine();
                }
                else
                {
                    line += character;

                    if(ColorStrings.GetLength(line) == maxLineLength)
                    {
                        newLine();
                    }
                }
            }

            if(line != "")
            {
                newLine();
            }

            return lines;
        }
    }
}