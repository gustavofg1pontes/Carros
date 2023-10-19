using System.Collections.Generic;

namespace RaceIF.Replay
{
    public static class ReplayManager
    {

        private readonly static Queue<TickRecords> records = new Queue<TickRecords>();
        private readonly static HashSet<IRecordable> watchingRecordables = new HashSet<IRecordable>();
        public static bool IsRecording { get; set; }
        public static bool IsReplaying { get; set; }
        public static bool HasRecords => records.Count > 0;

        static ReplayManager()
        {
            IsRecording = false;
            IsReplaying = false;
        }

        public static void Reset()
        {
            records.Clear();
            watchingRecordables.Clear();
            IsRecording = false;
            IsReplaying = false;
        }

        public static void StartWatching(List<IRecordable> recordables) {
            Reset();
            IsRecording = true;
            watchingRecordables.UnionWith(recordables);
        }


        public static void Record(long tick)
        {
            if (!IsRecording) return;
            var recordsToProcess = new HashSet<Record>();
            foreach (var recordable in watchingRecordables)
            {
                if (recordable is Carro)
                    recordsToProcess.Add(new Record(EntityType.CAR, recordable.Record()));
            }
            records.Enqueue(new TickRecords(tick, recordsToProcess));
        }

        public static TickRecords Replay() {
            if (!IsReplaying) return null;
            if (records.Count < 1)
            {
                IsReplaying = false;
                Reset();
                return null;
            }

            var tickRecords = records.Dequeue();
            return tickRecords;
        }
        

    }
}
