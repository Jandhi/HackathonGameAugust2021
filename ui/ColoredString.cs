using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Game.UI
{
    public class ColoredString
    {
        public string Contents { get; set; }
        public Color Foreground { get; set; } = Color.White;
        public Color Background { get; set; } = Color.Black;
        
        public ColoredString() // for object initializer
        {}

        public ColoredString(string contents)
        {
            Contents = contents;
        }

        public ColoredString(string contents, Color foreground) : this(contents)
        {
            Foreground = foreground;
        }

        public ColoredString(string contents, Color foreground, Color background) : this(contents, foreground)
        {
            Background = background;
        }

        public override string ToString()
        {
            return RecolorForeground(Foreground) + RecolorBackground(Background) + Contents + Undo() + Undo();
        }
        public int Length => GetLength(this.ToString());

        public static string operator+ (ColoredString a, string b)
        {
            return a.ToString() + b;
        }

        public static string operator+ (string a, ColoredString b)
        {
            return a + b.ToString();
        }

        public static string RecolorForeground(Color color)
        {
            return $"[c:r f:{color.R},{color.G},{color.B},{color.A}]";
        }

        public static string RecolorBackground(Color color)
        {
            return $"[c:r b:{color.R},{color.G},{color.B},{color.A}]";
        }

        public static string Undo()
        {
            return "[c:undo]";
        }

        public static int GetLength(string s)
        {
            var chars = new List<char>();
            var isInCommand = false;
            var count = 0;

            foreach(char c in s)
            {
                if(c == '[')
                {
                    chars.Clear();
                    chars.Add(c);
                }
                else if(c == 'c' && chars.Count > 0 && chars[0] == '[')
                {
                    chars.Add(c);
                }
                else if(c == ':' && chars.Count > 1 && chars[1] == 'c')
                {
                    chars.Add(c);
                    isInCommand = true;
                }
                else if(c == ']' && chars.Count > 2)
                {
                    chars.Clear();
                    isInCommand = false;
                }
                else if(!isInCommand)
                {
                    count++;

                    if(chars.Count > 0)
                    {
                        count += chars.Count;
                        chars.Clear();
                    }
                }
            }

            return count;
        }

        public static string GetRawString(string s)
        {
            var isInCommand = false;
            var commandCount = "";
            var raw = "";

            foreach(char c in s)
            {
                if(c == '[')
                {
                    raw += commandCount;
                    commandCount = "[";
                }
                else if(commandCount == "[" && c == 'c')
                {
                    commandCount += c;
                }
                else if(commandCount == "[c" && c == ':')
                {
                    commandCount += c;
                    isInCommand = true;
                }
                else if(isInCommand && c == ']')
                {
                    commandCount = "";
                    isInCommand = false;
                }
                else if(!isInCommand)
                {
                    raw += c;
                }
            }

            return raw;
        }

        public static List<string> GetWords(string s)
        {
            var currentWord = "";
            var list = new List<string>();

            foreach(char c in s)
            {
                if(currentWord.StartsWith("[c:"))
                {
                    currentWord += c;

                    if(c == ']')
                    {
                        list.Add(currentWord);
                        currentWord = "";
                    }
                }
                else if(c == '\n')
                {
                    list.Add(currentWord);
                    list.Add("\n");
                    currentWord = "";
                }
                else if(c == ' ')
                {
                    list.Add(currentWord);
                    currentWord = "";
                }
                else if(currentWord.EndsWith("[c:"))
                {
                    list.Add(currentWord.Substring(0, currentWord.Length - 3));
                    currentWord = "[c:";
                    currentWord += c;
                }
                else
                {
                    currentWord += c;
                }
            }

            if(currentWord != "")
            {
                list.Add(currentWord);
            }

            return list;
        }

        public static Color RandomColor()
        {
            var random = new Random();
            return new Color(random.Next(256),random.Next(256),random.Next(256));
        }

        public static string Transform(string source, Func<string, string> transform)
        {
            return ColoredString.From(source).Transform(transform).ToString();
        }

        public ColoredString Transform(Func<string, string> transform)
        {
            var words = GetWords(this.ToString());
            var result = "";

            foreach(var word in words)
            {
                if(word.StartsWith("[c:"))
                {
                    result += word;
                }
                else
                {
                    result += transform(word) + " ";
                }
            }

            if(result.EndsWith(" "))
            {
                result = result.TrimEnd();
            }

            return new ColoredString(result);
        }

        public static ColoredString From(string text)
        {
            var newText = "";
            var foreground = Color.White;
            var hasForeground = false;
            var background = Color.Black;
            var hasBackground = false;
            var isFirstRealWord = true;

            var words = ColoredString.GetWords(text);

            foreach(var word in words)
            {
                if(word.StartsWith("[c:r f:"))
                {
                    var colors = word.Substring(7, word.Length - 8).Split(",").Select(str => int.Parse(str)).ToArray();
                    hasForeground = true;
                    foreground = new Color(colors[0], colors[1], colors[2], colors[3]);
                }
                else if(word.StartsWith("[c:r b:"))
                {
                    var colors = word.Substring(7, word.Length - 8).Split(",").Select(str => int.Parse(str)).ToArray();
                    hasBackground = true;
                    background = new Color(colors[0], colors[1], colors[2], colors[3]);
                }
                else
                {
                    if(!word.StartsWith("[c:"))
                    {
                        if(isFirstRealWord)
                        {
                            isFirstRealWord = false;
                        }
                        else
                        {
                            newText += " ";
                        }
                    }

                    newText += word;
                }
            }

            if(newText.EndsWith(" "))
            {
                newText = newText.TrimEnd();
            }

            if(hasBackground)
            {
                return new ColoredString(newText, foreground, background);
            }
            else if(hasForeground)
            {
                return new ColoredString(newText, foreground);
            }
            else
            {
                return new ColoredString(newText);
            }
        }
    }
}