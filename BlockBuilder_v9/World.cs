using System;
using System.Collections.Generic;           
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuilder_v9
{
    class World
    {
  public  const int LoadDistance = 16;

        public Chunk[,] chunkArrey { get; private set; } = new Chunk[LoadDistance, LoadDistance];

        public void LoadWorld()
        {
            Chunk c;
            for (int x_i = 0; x_i < LoadDistance; ++x_i)
                for (int z_i = 0; z_i < LoadDistance; ++z_i)
                {
                    c = new Chunk(x_i << 4, z_i << 4);
                    c.GenerateChunk(x_i,z_i);
                    chunkArrey[x_i, z_i] = c;
                }
            c = null;



            //      for (int x_i = 0; x_i < LoadDistance; ++x_i)
            Parallel.For(0, LoadDistance, x_i =>
            {
                for (int z_i = 0; z_i < LoadDistance; ++z_i)
                {
                    for (int i = 0; i < 18; ++i)
                        for (int j = 0; j < 128; j++)
                        {
                          if (x_i != 0)
                              chunkArrey[x_i, z_i].BlockList[0, j, i] = chunkArrey[x_i - 1, z_i].BlockList[16, j, i];
                            if (x_i != LoadDistance-1)
                              chunkArrey[x_i, z_i].BlockList[17, j, i] = chunkArrey[x_i + 1, z_i].BlockList[1, j, i];
                            if (z_i != 0)
                                chunkArrey[x_i, z_i].BlockList[i, j, 0] = chunkArrey[x_i, z_i-1].BlockList[i, j, 16];
                            if (z_i != LoadDistance - 1)
                                chunkArrey[x_i, z_i].BlockList[i, j, 17] = chunkArrey[x_i , z_i+1].BlockList[i, j, 1];
                        }
                    chunkArrey[x_i, z_i].Refresh();
                }
            });

            for (int x_i = 0; x_i < LoadDistance; ++x_i)
                for (int z_i = 0; z_i < LoadDistance; ++z_i)
                    chunkArrey[x_i, z_i].SendGPU();
        }
    }
}

                                