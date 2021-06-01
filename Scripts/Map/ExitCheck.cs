using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCheck : MonoBehaviour
{
    private EnemyController _enemyController;

    public void Init(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }
    private void OnTriggerEnter(Collider other)
    {
        Sheep sheep = other.gameObject.GetComponent<Sheep>();
        if (sheep != null && sheep.IsPaddock)
        {
            _enemyController.RemoveSheep(sheep);
            sheep.Dead();
        }
    }
}
