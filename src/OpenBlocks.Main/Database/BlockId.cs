using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenBlocks.Main.Database
{
    public static class BlockId
    {
        /// <summary>
        /// Cached blocks
        /// </summary>
        private static readonly Dictionary<int, Block> LoadedBlocks = new Dictionary<int, Block>();

        /// <summary>
        /// Load a block class by reflection or use a cached one
        /// </summary>
        /// <param name="id"></param>
        public static Block Load(int id)
        {
            if (LoadedBlocks.ContainsKey(id))
                return LoadedBlocks[id];
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(Block).IsAssignableFrom(p))
                .Where(p => p.GetField("Id")?.GetValue(null) as int? == id);
            var enumerable = types as Type[] ?? types.ToArray();
            if (!enumerable.Any())
                throw new InvalidOperationException("Invalid block ID");
            var block = (Block) Activator.CreateInstance(enumerable.First());
            LoadedBlocks.Add(id, block);
            return block;
        }
    }
}