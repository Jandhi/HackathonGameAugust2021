using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Game.UI
{
    public static class ColorStrings
    {
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
            var inCommand = false;
            var count = 0;

            foreach(char c in s)
            {
                if(c == '[')
                {
                    chars.Add(c);
                }
                else if(c == 'c' && chars.Count > 0 && chars[0] == '[')
                {
                    chars.Add(c);
                }
                else if(c == ':' && chars.Count > 1 && chars[1] == 'c')
                {
                    chars.Add(c);
                    inCommand = true;
                }
                else if(c == ']' && chars.Count > 2)
                {
                    chars.Clear();
                    inCommand = false;
                }
                else if(!inCommand)
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

        public static Color RandomColor()
        {
            var random = new Random();
            return new Color(random.Next(256),random.Next(256),random.Next(256));
        }
    }
}