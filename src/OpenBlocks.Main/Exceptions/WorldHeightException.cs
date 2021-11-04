using System;

namespace OpenBlocks.Main.Exceptions
{
    /// <summary>
    /// Thrown when player tried to place a block above MaxHeight
    /// </summary>
    public class WorldHeightException : Exception { }
}