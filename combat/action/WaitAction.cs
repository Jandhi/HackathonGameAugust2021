using System.Threading;

namespace Game.Combat.Action
{
    public class WaitAction : BaseAction
    {
        public override bool IsDisplayOnly => true;
        public int WaitTime { get; }

        public WaitAction(int waitTime)
        {
            WaitTime = waitTime;
        }
        public override void Do()
        {
            Thread.Sleep(WaitTime);
        }

        public override void Undo()
        {
            Thread.Sleep(WaitTime);
        }
    }
}