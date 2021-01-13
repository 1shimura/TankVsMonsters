using UnityEngine;

namespace Scripts
{
    public class ActorSpawnPoint : MonoBehaviour
    {
        public bool IsBusy => _isBusy;
        public Transform PointTransform
        {
            get
            {
                if (_pointTransform != null) return _pointTransform;
                return _pointTransform = GetComponent<Transform>();
            }
        }
        
        private Transform _pointTransform;
        private bool _isBusy;
        private IActor _owner;

        public void TryRelease()
        {
            if (!_isBusy || _pointTransform.position == _owner.Transform.position) return;
            
            Release();
        }

        public void OccupyPoint(IActor actor)
        {
            _owner = actor;
            _isBusy = true;
        }

        private void Release()
        {
            _owner = null;
            _isBusy = false;
        }
    }
}