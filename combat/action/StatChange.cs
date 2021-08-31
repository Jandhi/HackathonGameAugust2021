namespace Game.Combat.Action
{
    public class StatChange : BaseAction
    {
       
        public Entity Reciever {get;}
        public Stat AffectedStat {get;}
        public float StatAfter {get;}
        public float StatBefore { get;}
        public override bool IsDisplayOnly => false;
       


        public StatChange ( Entity reciever, Stat affectedStat, float statBefore, float statAfter)
        {
            Reciever = reciever;
            AffectedStat = affectedStat;
            StatBefore = statBefore;
            StatAfter = statAfter;
        }

        public override void Do()
        {
           Reciever.Stats [AffectedStat] = StatAfter;
        }

        public override void Undo()
        {
            Reciever.Stats [AffectedStat] = StatBefore;
        }

    }
}