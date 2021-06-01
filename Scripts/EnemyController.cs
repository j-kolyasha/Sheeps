using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private UIPresenter _uiPresenter;
    public Sheep[] Sheeps;
    public List<Sheep> SheepsInPaddock = new List<Sheep>();
    
    private float _spawnTime, _cooldownSpawnTime = 0.55f;
    private List<float> _chancesSpawnSheeps;
    private Transform _spawnEnemyPosition;

    public void Init(Transform spawnEnemyPosition)
    {
        _spawnEnemyPosition = spawnEnemyPosition;
    }
    public void Spawn()
    {
        if (_spawnTime > 0 || GameManager.Instance.GameState == GameState.Pause) return;
       
        Sheep currentSheep = Instantiate(GetRandomSheep(), _spawnEnemyPosition.position, Quaternion.identity);
        _spawnTime = _cooldownSpawnTime;
    }
    public void AddSheep(Sheep sheep)
    {
        SheepsInPaddock.Add(sheep);
        _uiPresenter.CountSheepUpdate(SheepsInPaddock.Count);
    }
    public void RemoveSheep(Sheep sheep)
    {
        SheepsInPaddock.Remove(sheep);
        _uiPresenter.CountSheepUpdate(SheepsInPaddock.Count);
    }
    //public List<Sheep> SortedSheeps()
    //{
    //    for (int i = 0; i < SheepsInPaddock.Count; i++)
    //    {
    //        for (int j = 0; j < SheepsInPaddock.Count - i - 1; j++)
    //        {
    //            if (SheepsInPaddock[j + 1].TurnAbility < SheepsInPaddock[j].TurnAbility)
    //            {
    //                Sheep temp = SheepsInPaddock[j + 1];
    //                SheepsInPaddock[j + 1] = SheepsInPaddock[j];
    //                SheepsInPaddock[j] = temp;
    //            }
    //        }
    //    }

    //    return SheepsInPaddock;
    //}
    private Sheep GetRandomSheep()
    {
        _chancesSpawnSheeps = new List<float>();
        for (int i = 0; i < Sheeps.Length; i++)
        {
            _chancesSpawnSheeps.Add(Sheeps[i].ChanceSpawn.Evaluate(GameManager.Instance.Score));
        }

        float value = UnityEngine.Random.Range(0, _chancesSpawnSheeps.Sum());
        float sum = 0;

        for (int i = 0; i < _chancesSpawnSheeps.Count; i++)
        {
            sum += _chancesSpawnSheeps[i];
            if (sum > value)
            {
                return Sheeps[i];
            }
        }

        return Sheeps[UnityEngine.Random.Range(0, Sheeps.Length)];
    }
    private void DestroyAllSheeps()
    {
        for (int i = SheepsInPaddock.Count - 1; i >= 0; i--)
        {
            SheepsInPaddock[i].Dead();
        }
    }
    private void Update()
    {
        _spawnTime -= Time.deltaTime;
    }
    private void OnEnable()
    {
        GameManager.Instance.OnNextLevel += DestroyAllSheeps;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnNextLevel -= DestroyAllSheeps;
    }
}  