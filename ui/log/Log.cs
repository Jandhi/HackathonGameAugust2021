using System.Collections.Generic;

namespace Game.UI.Log
{
    public class Log
    {
        public List<string> Contents { get; }
        public int MaxLines { get; }
        public List<ILogListener> Listeners { get; } = new List<ILogListener>();

        public Log(int maxLines = 100)
        {
            MaxLines = maxLines;
        }
        
        public void AddLine(string line)
        {
            Contents.Add(line);

            while(Contents.Count > MaxLines)
            {
                Contents.RemoveAt(0);
            }

            NotifyListeners();
        }

        public void NotifyListeners()
        {
            foreach(var listener in Listeners)
            {
                listener.OnLogChange(this);
            }
        }

        public override string ToString()
        {
            var text = "";
            
            foreach(var line in Contents)
            {
                text += line + "\n";
            }

            return text;
        }
    }
}