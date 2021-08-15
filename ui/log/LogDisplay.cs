

namespace Game.UI.Log
{
    public class LogDisplay : ScrollableTextDisplay, ILogListener
    {
        public LogDisplay(int width, int height, Log log) : base(width, height, log.ToString())
        {}

        public void OnLogChange(Log log)
        {
            Text = log.ToString();
            ScrollPosition = MaxScrollPosition;
        }
    }
}