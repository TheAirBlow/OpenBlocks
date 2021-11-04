using System.Drawing;

namespace OpenBlocks.Main.Database
{
    public abstract class Block
    {
        public Rectangle AtlasTopPosition;
        public Rectangle AtlasBottomPosition;
        public Rectangle AtlasLeftPosition;
        public Rectangle AtlasRightPosition;
        public Rectangle AtlasBackPosition;
        public Rectangle AtlasFrontPosition;
        public BreakProgress Progress;
        public int Id;
        
        public virtual void OnClick() { }
        public virtual void OnStartBreak()
            => Progress.Progress++;
        public virtual void OnEndBreak()
            => Progress.Progress = 0;
    }
}