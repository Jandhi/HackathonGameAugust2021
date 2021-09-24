using Game.UI.Log;

namespace Game.Combat.Action
{
    public class LogAction : BaseAction
    {
        public Log Log { get; }
        public string Message { get; }

        public LogAction(Log log, string message)
        {
            Log = log;
            Message = message;
        }

        public override bool IsDisplayOnly => true;

        public override void Do()
        {
            Log.AddLine(Message);
        }

        public override void Undo()
        {
            // nothing
        }
    }
}