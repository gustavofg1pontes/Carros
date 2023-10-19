using System.Collections.Generic;

namespace RaceIF.Replay
{
    public class TickRecords
    {

        public long Tick { get; set; }
        public HashSet<Record> Records { get; set; }

        public TickRecords()
        {
            Records = new HashSet<Record>();
        }

        public TickRecords(long tick, HashSet<Record> records)
        {
            Tick = tick;
            foreach (var r in records)
            {
                r.Tick = tick;
            }
            Records = records;
        }
    }
}
