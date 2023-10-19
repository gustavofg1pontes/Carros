namespace RaceIF.Replay
{
    public class EntityView : IEntityState
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int SpriteIndex { get; set; }

        public EntityView() { }

        public EntityView(int posX, int posY, int spriteIndex)
        {
            PosX = posX;
            PosY = posY;
            SpriteIndex = spriteIndex;
        }

        public object GetData()
        {
            return this;
        }
    }

}
