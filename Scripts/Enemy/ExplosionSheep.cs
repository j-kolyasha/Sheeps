using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSheep : Sheep
{
    [Header("Explosion Settings")]
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField, Range(0.1f, 1f)] private float _radius;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _center;

    public override void Ability()
    {
        Collider[] sheeps = Physics.OverlapSphere(_center.position, _radius, _layerMask);

        _explosionEffect.SetActive(true);

        for (int i = 0; i < sheeps.Length; i++)
        {
            sheeps[i].GetComponent<Sheep>().Dead();
        }

        Destroy(gameObject, 0.1f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_center.position, _radius);
    }
}
