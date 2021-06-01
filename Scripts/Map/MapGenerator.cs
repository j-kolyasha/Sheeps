using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGeneratorBlock
{
    public int X;
    public int Z;

    public bool fenceTop = false;
    public bool fenceBotton = false;
    public bool fenceLeft = false;
    public bool fenceRight = false;
}
public class MapGenerator
{
    private float _chanceMissionBlock = .07f;

    public MapGeneratorBlock[,] GenerateMap(int width, int height)
    {
        MapGeneratorBlock[,] map = new MapGeneratorBlock[width, height];
        int layers = Random.Range(2, height-10);

        for (int z = 0; z < layers; z++)
        {
            int countBlocks = Random.Range(3, width-7);
            int startSpawn = (width - countBlocks) / 2;
            

            for (int x = startSpawn; x < startSpawn + countBlocks; x++)
            {
                float chanceSpawn = (z == 0 || z == 1) ? 1 : Random.Range(0, 1f);

                if (chanceSpawn > _chanceMissionBlock)
                {
                    map[x, z + 2] = new MapGeneratorBlock { X = x, Z = z + 2 };
                }
            }
        }

        return AddFence(map);
    }

    private MapGeneratorBlock[,] AddFence(MapGeneratorBlock[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int z = 0; z < map.GetLength(1); z++)
            {
                if (map[x, z] != null)
                {
                    if (map[map[x, z].X + 1, map[x, z].Z] == null) map[x, z].fenceRight = true;
                    if (map[map[x, z].X - 1, map[x, z].Z] == null) map[x, z].fenceLeft = true;
                    if (map[map[x, z].X, map[x, z].Z + 1] == null) map[x, z].fenceTop = true;
                    if (map[map[x, z].X, map[x, z].Z - 1] == null) map[x, z].fenceBotton = true;
                }
            }
        }

        return map;
    }
}
