using System;
using System.Collections.Generic;

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

        public Button ScrollUpButton { get; }
        public Button ScrollDownButton { get; }

        public ScrollableTextDisplay(int width, int height, string text) : base(width, height)
        {
            Text = text;
            UsePrintProcessor = true; // allow for coloring

            Draw();
        }

        public void Draw()
        {
            var lineNum = 0;
            foreach(var line in Lines)
            {
                Print(0, lineNum, line);
                lineNum++;
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