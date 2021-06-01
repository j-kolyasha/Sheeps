using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSheep : Sheep
{
    [Header("Ability Settiings")]
    [SerializeField] private GameObject _abilityEffect;
    [SerializeField] private MeshFilter _selfMesh;
    [SerializeField] private Mesh _adultSheep;
    public override void Ability()
    {
        _selfMesh.mesh = _adultSheep;
        _abilityEffect.SetActive(true);
    }
}
