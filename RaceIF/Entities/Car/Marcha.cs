namespace RaceIF
{
    public class Marcha
    {
        public readonly int MarchaIndex;
        public readonly float Min;
        public readonly float Max;

        public Marcha(int marcha, float min, float max)
        {
            MarchaIndex = marcha;
            Min = min;
            Max = max;
        }
    }
}