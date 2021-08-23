

namespace Game.UI.Log
{
    public class LogDisplay : ScrollableTextDisplay, ILogListener
    {
        public LogDisplay(int width, int height, Log log) : base(width, height, log.ToString())
        {
            log.Listeners.Add(this);
        }

        public void OnLogChange(Log log)
        {
            Text = log.ToString();
            ScrollPosition = MaxScrollPosition;
            Draw();
        }
    }
}