using Microsoft.Xna.Framework;

namespace OpenBlocks.Main.Database.Blocks
{
    /// <summary>
    /// An unbreackable block
    /// </summary>
    public class Bedrock : Block {
        public new int Id = 1;
        public new Rectangle AtlasTopPosition = new Rectangle(16, 16, 16, 16);
        public new Rectangle AtlasBottomPosition = new Rectangle(16, 16, 16, 16);
        public new Rectangle AtlasLeftPosition = new Rectangle(16, 16, 16, 16);
        public new Rectangle AtlasRightPosition = new Rectangle(16, 16, 16, 16);
        public new Rectangle AtlasBackPosition = new Rectangle(16, 16, 16, 16);
        public new Rectangle AtlasFrontPosition = new Rectangle(16, 16, 16, 16);
        public new BreakProgress Progress = new BreakProgress {
            MaxProgress = -1
        };
    }
}