using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

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
    }
}