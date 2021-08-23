namespace Game.Combat.Action
{
    public class StatChange : ICombatAction
    {
       
        public Entity Reciever {get;}
        public Stat AffectedStat {get;}
        public float StatAfter {get;}
        public float StatBefore {get;}
       


        public StatChange ( Entity reciever, Stat affectedStat, float statBefore, float statAfter)
        {
            Reciever = reciever;
            AffectedStat = affectedStat;
            StatBefore = statBefore;
            StatAfter = statAfter;
        }

        public void Do()
        {

           Reciever.Stats [AffectedStat] = StatAfter;

        }

        public void Undo()
        {
            Reciever.Stats [AffectedStat] = StatBefore;
        }

    }
}