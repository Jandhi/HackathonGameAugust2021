using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class ScrollableTextDisplay : SadConsole.Console, IDrawable
    {
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
        public int MaxScrollPosition => Math.Max(0, Lines.Count - Height); 

        public Button ScrollUpButton { get; set; }
        public Button ScrollDownButton { get; set; }

        public ScrollableTextDisplay(int width, int height, string text) : base(width, height)
        {
            Text = text;
            UsePrintProcessor = true; // allow for coloring
            
            SetupButtons();
            Draw();
        }

        public void SetupButtons()
        {
            ScrollUpButton = new Button("^", () => {
                if(ScrollPosition > 0) ScrollPosition--;
                Draw();
            });
            ScrollUpButton.Position = new Point(Width - 1, 0);
            ScrollUpButton.Parent = this;

            ScrollDownButton = new Button("v", () => {
                if(ScrollPosition < MaxScrollPosition) ScrollPosition++;
                Draw();
            });
            ScrollDownButton.Position = new Point(Width - 1, Height - 1);
            ScrollDownButton.Parent = this;
        }

        public void Draw()
        {
            for(var i = 0; i < Height && ScrollPosition + i < Lines.Count; i++)
            {
                Print(0, i, Lines[ScrollPosition + i]);
            }
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

                    if(line.Length == maxLineLength)
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