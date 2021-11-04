using System;
using Microsoft.Xna.Framework;
using OpenBlocks.Main.Exceptions;

namespace OpenBlocks.Main.World
{
    public class Chunk
    {
        private readonly ushort[] _voxels = new ushort[16 * 16 * 16];
        public const int MaxHeight = 256;
 
        public ushort this[int x, int y, int z]
        {
            get {
                if (y > MaxHeight)
                    throw new WorldHeightException();
                return _voxels[x * 16 * 16 + y * 16 + z];
            }
            
            set {
                if (y > MaxHeight)
                    throw new WorldHeightException();
                _voxels[x * 16 * 16 + y * 16 + z] = value;
            }
        }
    }
}