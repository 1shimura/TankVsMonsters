using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class AbilityFire : MonoBehaviour, IActorAbility
    {
        [SerializeField] private List<Bullet> _bulletPrefabs = new List<Bullet>();

        [SerializeField] private KeyCode _nextBulletAssignedKey;
        [SerializeField] private KeyCode _prevBulletAssignedKey;

        [Space] [SerializeField] private KeyCode _fireAssignedKey;
        [Space] [SerializeField] private float _cooldownTime;
        [Space] [SerializeField] private Transform _spawnPoint;

        public IActor Actor { get; set; }

        private List<Pool<Bullet>> _bulletPools = new List<Pool<Bullet>>();
        private Pool<Bullet> _currentActivePool;

        private bool _enabled = true;

        public void Initialize(IActor actor)
        {
            Actor = actor;

            foreach (var bulletPrefab in _bulletPrefabs)
            {
                var newPool = new Pool<Bullet>(new PrefabFactory<Bullet>(bulletPrefab.gameObject, false), 3);
                _bulletPools.Add(newPool);
            }

            _currentActivePool = _bulletPools.First();
        }

        public void Execute()
        {
            if (!_enabled)
            {
                return;
            }

            GetNewBullet();

            if (Math.Abs(_cooldownTime) < 0.01f)
            {
                return;
            }

            _enabled = false;
            StartCoroutine(CoroutineUtils.DelayAction(_cooldownTime, () => _enabled = true));
        }

        private void GetNewBullet()
        {
            var newBullet = _currentActivePool.Allocate();
            newBullet.Transform.position = _spawnPoint.position;
            
            newBullet.OnBulletDisable += DisableBulletHandler;
            newBullet.Spawner = Actor;
        }
        
        private void DisableBulletHandler(Bullet bullet)
        {
            var currentPool = _bulletPools.First(pool => pool.Members.Contains(bullet));

            currentPool?.Release(bullet);
        }

        private void Update()
        {
            if (Input.GetKeyDown(_nextBulletAssignedKey))
            {
                for (var i = 0; 0 < _bulletPools.Count; i++)
                {
                    if (_bulletPools[i] != _currentActivePool)
                    {
                        continue;
                    }

                    if (_bulletPools.Count - 1 <= i)
                    {
                        break;
                    }

                    _currentActivePool = _bulletPools[i + 1];
                    
                    return;
                }
            }

            if (Input.GetKeyUp(_prevBulletAssignedKey))
            {
                for (var i = 0; 0 < _bulletPools.Count; i++)
                {
                    if (_bulletPools[i] != _currentActivePool)
                    {
                        continue;
                    }

                    if (i == 0)
                    {
                        break;
                    }

                    _currentActivePool = _bulletPools[i - 1];
                    return;
                }
            }

            if (Input.GetKeyUp(_fireAssignedKey))
            {
                Execute();
            }
        }
    }
}