using System.Collections.Generic;

namespace Game.UI
{
    public class TextDisplay : SadConsole.Console, IUIElement
    {
        public Theme Theme { get; }
        public List<string> Lines { get; set; }
        public virtual int MaxLineLength => Width;
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
        public bool DoWrapping { get; }

        public TextDisplay(string text, int width, int height, bool doWrapping = true, Theme theme = null) : base(width, height)
        {
            DoWrapping = doWrapping;
            Theme = theme ?? Theme.CurrentTheme;
            Text = text;
            UsePrintProcessor = true; // allow for coloring
        }

        private List<string> CalculateLines()
        {
            return DoWrapping ? CalculateLinesWithWrapping() : CalculateLinesWithoutWrapping();
        }

        public List<string> CalculateLinesWithWrapping()
        {
            var lines = new List<string>();
            var line = "";
            var words = ColoredString.GetWords(Text);

            while(words.Count > 0)
            {
                var word = words[0];
                words.RemoveAt(0);

                if(word.StartsWith("[c:")) // command
                {
                    line += word;
                }
                else if(word == "\n")
                {
                    lines.Add(line);
                    line = "";
                }
                else if(word.Length > MaxLineLength) // word exceeds length of a line
                {
                    var space = MaxLineLength - ColoredString.GetLength(line);
                    line += word.Substring(0, space);
                    words.Insert(0, word.Substring(space)); // add rest of word for future processing
                }
                else if(ColoredString.GetLength(line) + word.Length > MaxLineLength) // line with word would be too long
                {
                    lines.Add(line);
                    line = word;

                    // Add space after word
                    if(line.Length < MaxLineLength)
                    {
                        line += " ";
                    }
                }
                else
                {
                    line += word;
                }
            }

            return lines;
        }

        public List<string> CalculateLinesWithoutWrapping()
        {
            var lines = new List<string>();
            var line = "";

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

                    if(ColoredString.GetLength(line) == MaxLineLength)
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

        public virtual void Draw()
        {
            Clear();
            
            var y = 0;
            foreach(var line in Lines)
            {
                Print(0, y, line);
                y++;

                if(y >= Height)
                {
                    return;
                }
            }
        }
    }
}