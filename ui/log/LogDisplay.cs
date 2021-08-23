

namespace Game.UI.Log
{
    public class LogDisplay : ScrollableTextDisplay, ILogListener
    {
        public LogDisplay(int width, int height, Log log) : base(log.ToString(), width, height, true)
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