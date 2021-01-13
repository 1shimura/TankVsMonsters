using System;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Bullet : Actor
    {
        [SerializeField] private bool _limitedLifeSpan;
        [SerializeField] private float _lifeSpan;
        
        [Space]
        [SerializeField] private bool _disableAfterCollision;
        
        public Action<Bullet> OnBulletDisable { get; set; }

        private void Update()
        {
            if (_limitedLifeSpan)
            {
                StartCoroutine(CoroutineUtils.DelayAction(_lifeSpan, () =>
                {
                    OnBulletDisable?.Invoke(this);
                }));
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            HandleCollision(col);
        }

        private void OnCollisionEnter(Collision col)
        {
            HandleCollision(col.collider);
        }

        private void HandleCollision(Collider col)
        {
            if (_disableAfterCollision)
            {
                OnBulletDisable?.Invoke(this);
            }   
        }
    }
}