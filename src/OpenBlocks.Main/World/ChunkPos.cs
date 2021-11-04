using System;
using Microsoft.Xna.Framework;

namespace OpenBlocks.Main.World
{
    /// <summary>
    /// Chunk position
    /// </summary>
    public readonly struct ChunkPos : IEquatable<ChunkPos>
    {
        public readonly int X;
        public readonly int Y;

        public ChunkPos(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public static ChunkPos FromWorldPos(int x, int y)
            => new ChunkPos(x >> 4, y >> 4);
        
        public static ChunkPos FromWorldPos(Vector3 data)
            => new ChunkPos((int)data.X >> 4, (int)data.Y >> 4);

        #region Equality members
        public bool Equals(ChunkPos other)
        {
            return X == other.X && Y == other.Y;
        }
 
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ChunkPos other && Equals(other);
        }
 
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                return hashCode;
            }
        }
 
        public static bool operator ==(ChunkPos left, ChunkPos right)
        {
            return left.Equals(right);
        }
 
        public static bool operator !=(ChunkPos left, ChunkPos right)
        {
            return !left.Equals(right);
        }
        #endregion
    }
}