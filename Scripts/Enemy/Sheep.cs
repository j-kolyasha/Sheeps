using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class Sheep : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _eyes;
    [SerializeField] private GameObject _deadEffect;
    [SerializeField] private LayerMask LayerMask;
    
    [Range(1, 5)] public int TurnAbility;
    public AnimationCurve ChanceSpawn;
    public bool IsPaddock;

    private Rigidbody _selfRigidbody;

    public void Dead()
    {
        StartCoroutine("DestroySheep");
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameState.Pause) return;

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        if (Physics.Raycast(_eyes.position, direction, out hit, 0.4f, LayerMask))
        {
            float rotate;
            if (!IsPaddock)
            {
                rotate = Random.Range(0, 1f) >= .5f ? Random.Range(-90, -100) : Random.Range(90, 100);
                Dead();
            }
            else
            {
                rotate = Random.Range(-180, 180);
            }

            transform.Rotate(0, transform.rotation.y + rotate, 0);
        }

        _selfRigidbody.velocity = direction.normalized * _speed;
    }
    private IEnumerator DestroySheep()
    {
        if (!IsPaddock)
            yield return new WaitForSeconds(1f);
        else
            FindObjectOfType<EnemyController>().RemoveSheep(this);

        _deadEffect.SetActive(true);
        Destroy(gameObject, .2f);
    }
    private void OnEnable()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door")) IsPaddock = true;
    }

    public abstract void Ability();
}
