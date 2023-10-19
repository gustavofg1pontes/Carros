using System.Drawing;

namespace RaceIF.Replay
{
    public interface IPresentable
    {
        public Image Present(IEntityState state);
    }
}
