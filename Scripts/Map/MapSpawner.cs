using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [Header("Prefabs")]
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private GameObject _mainBlockPrefab;
    [Header("Environment")]
    [SerializeField] [Range(0, 1)] private float _spawpChance;
    [SerializeField] private Environment[] _environments;

    private Block[,] _mapBlocks;
    public int CountBlock => _mapBlocks.Length;

    private void MapUpdate()
    {
        ClearMap();
        SpawnerMap();
    }
    private void ClearMap()
    {
        if (_mapBlocks == null) return;

        for (int x = 0; x < _mapBlocks.GetLength(0); x++)
        {
            for (int z = 0; z < _mapBlocks.GetLength(1); z++)
            {
                if (_mapBlocks[x, z] != null)
                {
                    Destroy(_mapBlocks[x, z].gameObject);
                }
            }
        }
    }

    private void SpawnerMap()
    {
        MapGenerator mapGenerator = new MapGenerator();
        MapGeneratorBlock[,] map = mapGenerator.GenerateMap(_width, _height);
        int countBlocks = 0;
        _mapBlocks = new Block[_width, _height];  

    
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _width; z++)
            {
                if (x == 7 && z == 2)
                {
                    if (FindObjectOfType<MainBlock>() == null)
                        Instantiate(_mainBlockPrefab, new Vector3(x, 0, z), Quaternion.identity);
                    
                    continue;
                }

                Block currentBlock = Instantiate(_blockPrefab, new Vector3(x, 0, z), Quaternion.identity, transform).GetComponent<Block>();
                if (map[x, z] != null)
                {
                    currentBlock.TopFence.SetActive(map[x, z].fenceTop);
                    currentBlock.BottonFence.SetActive(map[x, z].fenceBotton);
                    currentBlock.LeftFence.SetActive(map[x, z].fenceLeft);
                    currentBlock.RightFence.SetActive(map[x, z].fenceRight);
                }
                else
                {
                    if (x != 7 || (z!=0 && z!=1))
                    {
                        if (Random.Range(0, 1f) < _spawpChance)
                        {
                            Instantiate(GetRandomEnvironment(), new Vector3(x, 0.5f, z), Quaternion.Euler(0, Random.Range(-180, 180), 0), currentBlock.transform);
                        }
                    }
                }

                _mapBlocks[x, z] = currentBlock;
            }
        }
    }

    private Environment GetRandomEnvironment()
    {
        List<float> _chances = new List<float>();

        for (int i = 0; i < _environments.Length; i++)
        {
            _chances.Add(_environments[i].ChanceSpawn);
        }

        float value = Random.Range(0, _chances.Sum());
        float sum = 0;

        for (int i = 0; i < _chances.Count; i++)
        {
            sum += _chances[i];
            if (sum > value)
            {
                return _environments[i];
            }
        }

        return _environments[_environments.Length - 1];
    }

    private void OnEnable()
    {
        GameManager.Instance.OnNextLevel += MapUpdate;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnNextLevel -= MapUpdate;
    }
}
