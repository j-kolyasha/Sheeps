using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlock : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private ExitCheck _exitCheck;

    private EnemyController _enemyController;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            OpenDoor(false);
        else
            OpenDoor(true);
    }
    private void OpenDoor(bool isOpen)
    {
        _door.SetActive(isOpen);
    }
    private void OnTriggerEnter(Collider other)
    {
        Sheep sheep = other.gameObject.GetComponent<Sheep>();
        if (sheep != null && !sheep.IsPaddock)
        {
            sheep.IsPaddock = true;
            _enemyController.AddSheep(sheep);
        }
    }
    private void Start()
    {
        _enemyController = FindObjectOfType<EnemyController>();
        _enemyController.Init(_spawnPoint.transform);
        _exitCheck.Init(_enemyController);
    }
}
