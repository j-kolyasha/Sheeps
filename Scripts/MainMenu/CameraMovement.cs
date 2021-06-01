using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 _defaultAngle;
        [SerializeField, Range(0, 180)] private float _reviewAngle;
        private bool _moveRight;
        private float _nextAngle;

        private void Update()
        {
            if (transform.rotation.y >= _nextAngle && _moveRight)
            {
                _moveRight = false;
                _nextAngle = _defaultAngle.y - _reviewAngle;
            }
            else if (transform.rotation.y >= _nextAngle && !_moveRight)
            {
                _moveRight = true;
                _nextAngle = _defaultAngle.y + _reviewAngle;
            }


            for (float i = 0; i < _reviewAngle; i += Time.deltaTime)
            {
                float y = transform.rotation.y;
                if (_moveRight)
                    y += Time.deltaTime;
                else
                    y -= Time.deltaTime;

                transform.rotation = Quaternion.Euler(_defaultAngle.x, y, _defaultAngle.z);
            }
        }
    }
}
