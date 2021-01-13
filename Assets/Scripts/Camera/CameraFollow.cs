using UnityEngine;

namespace Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] [Range(0.01f, 1f)] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector3 _followOffset;

        public Transform Target
        {
            get => _target;
            set => _target = value;
        }

        private void FixedUpdate()
        {
            var desiredPosition = _target.position + _followOffset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            
            transform.position = smoothedPosition;
            transform.LookAt(_target.transform);
        }
    }
}
