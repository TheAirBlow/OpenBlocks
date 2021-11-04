using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace OpenBlocks.Main.World
{
    public class World
    {
        public class SpawnData
        {
            public const int Chunks = 5;
            public Vector3 Position;
        }

        public string Name;
        public int Seed;
        public SpawnData Spawn = new SpawnData();
        public Dictionary<ChunkPos, Chunk> Chunks = new Dictionary<ChunkPos, Chunk>();

        public ushort this[int x, int y, int z] {
            get {
                var chunk = Chunks[ChunkPos.FromWorldPos(x, y)];
                return chunk[x & 0xF, y & 0xF, z & 0xF];
            }
 
            set {
                var chunk = Chunks[ChunkPos.FromWorldPos(x, y)];
                chunk[x & 0xF, y & 0xF, z & 0xF] = value;
            }
        }

        /// <summary>
        /// Unload and save chunks that are not in range, and generate new ones
        /// </summary>
        /// <param name="currentChunk">Current chunk</param>
        public void ProcessRenderingRange(ChunkPos currentChunk)
        {
            // Do it as a background task
            Task.Factory.StartNew(() => {
                foreach (var pos in GetChunksInRange(currentChunk, SpawnData.Chunks)) {
                    if (Chunks.ContainsKey(pos)) {
                        SaveChunk(pos);
                        Chunks.Remove(pos);
                    } else {
                        Chunks.Add(pos, GenerateChunk(pos));
                        SaveChunk(pos);
                    }
                }
            });
        }

        /// <summary>
        /// Updates spawn position as it gets obstucted
        /// </summary>
        public void UpdateSpawn()
        {
            // Do it as a background task
            Task.Factory.StartNew(UpdateSpawn_NoTask);
        }

        /// <summary>
        /// Updates spawn position as it gets obstucted
        /// </summary>
        private void UpdateSpawn_NoTask()
        {
            for (int i = 0; i <= Chunk.MaxHeight; i++) {
                if (i == Chunk.MaxHeight)
                    Spawn.Position.Y = Chunk.MaxHeight + 1;
                else if (this[(int) Spawn.Position.X, i, (int) Spawn.Position.Z] == 0)
                    Spawn.Position.Y = i;
            }
        }

        /// <summary>
        /// Get chunks in range
        /// </summary>
        /// <param name="currentChunk">Current chunk</param>
        /// <param name="range">Range</param>
        /// <returns>List of chunk positions</returns>
        public List<ChunkPos> GetChunksInRange(ChunkPos currentChunk, int range)
        {
            var pos = new List<ChunkPos>();
            for (int x = currentChunk.X - range; x <= currentChunk.X + range; x++)
            for (int y = currentChunk.Y - range; y <= currentChunk.Y + range; y++)
                pos.Add(new ChunkPos(x, y));
            return pos;
        }

        /// <summary>
        /// Generate a chunk
        /// </summary>
        /// <param name="pos">Position</param>
        /// <returns>Generated chunk</returns>
        private Chunk GenerateChunk(ChunkPos pos)
        {
            Chunk chunk = new Chunk();
            for (int x = 0; x < 16; x++)
            for (int z = 0; z < 16; z++)
            for (int y = 0; y < 1; y++)
            {
                switch (y) {
                    case 0:
                        chunk[x, y, z] = 1;
                        break;
                }
            }

            return chunk;
        }

        /// <summary>
        /// Saves a chunk
        /// </summary>
        /// <param name="pos">Position</param>
        private void SaveChunk(ChunkPos pos)
        {
            
        }

        /// <summary>
        /// Loads a world from directory
        /// </summary>
        /// <param name="dir">Directory</param>
        public World(string dir)
        {
            Name = File.ReadAllText(Path.Combine(dir, "name.txt"));
            Seed = int.Parse(File.ReadAllText(Path.Combine(dir, "seed.txt")));
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate a new world and save it
        /// </summary>
        /// <param name="dir">Directory</param>
        /// <param name="name">World name</param>
        /// <param name="seed">World seed</param>
        public World(string dir, string name, int seed)
        {
            File.WriteAllText(Path.Combine(dir, "name.txt"), name);
            File.WriteAllText(Path.Combine(dir, "seed.txt"), seed.ToString());
            Spawn.Position = new Vector3(0, 0, 0);
            foreach (var pos in GetChunksInRange(ChunkPos.FromWorldPos(Spawn.Position), SpawnData.Chunks)) {
                Chunks.Add(pos, GenerateChunk(pos));
                SaveChunk(pos);
            }
            UpdateSpawn_NoTask();
        }
    }
}