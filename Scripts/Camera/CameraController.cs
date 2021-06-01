using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    private Camera _camera;

    private int _defaultFov = 60, _explosionFov = 57;

    public void Explosion()
    {
        StartCoroutine("ChangeFov");
    }
    private void ChangeBackgroundColor()
    {
        _camera.backgroundColor = _colors[Random.Range(0, _colors.Length)];
    }
    private IEnumerator ChangeFov()
    {
        _camera.fieldOfView = _explosionFov;
        yield return new WaitForSeconds(0.05f);
        _camera.fieldOfView = _defaultFov;
    }
    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
        GameManager.Instance.OnNextLevel += ChangeBackgroundColor;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnNextLevel -= ChangeBackgroundColor;
    }
}
