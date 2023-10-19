namespace RaceIF.Replay
{
    public class Record
    {
        public EntityType Type { get; set; }
        public IEntityState State { get; set; }
        public long Tick { get; set; }

        public Record()
        {
        }

        public Record(EntityType type, IEntityState state)
        {
            Type = type;
            State = state;
        }

        public Record(EntityType type, IEntityState state, long tick)
        {
            Type = type;
            State = state;
            Tick = tick;
        }
    }
}
