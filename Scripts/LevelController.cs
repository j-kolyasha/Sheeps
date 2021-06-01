using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private MapSpawner _mapSpawner;
    [SerializeField] private UIPresenter _uiPresenter;
    public int LevelNumber { get; private set; } = 1;
    private bool _isDone;
    public void NextLevel()
    {
        if (_isDone) return;

        _isDone = true;
        StartCoroutine("CheckingResult");
        _uiPresenter.CountLevelUpdate(LevelNumber);
    }
    private IEnumerator CheckingResult()
    {
        for (int i = 0; i <= 10; i++)
        {
            Sheep[] sheeps = _enemyController.SheepsInPaddock.Where(sheep => sheep.TurnAbility == i).ToArray();
            if (sheeps.Length != 0)
            {
                if (i == 3)
                    _cameraController.Explosion();

                for (int k = 0; k < sheeps.Length; k++)
                {
                    sheeps[k].Ability();
                }

                yield return new WaitForSeconds(.5f);
            }
        }


        yield return new WaitForSeconds(1f);
        GameManager.Instance.NextLevel();

        _isDone = false;
    }
    private void Update()
    {
        if (GameManager.Instance.GameState == GameState.Pause) return;

        _enemyController.Spawn();
    }
}
