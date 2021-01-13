using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class Actor : MonoBehaviour, IActor
    {
        public List<IActorAbility> Abilities { get; set; }

        public IActor Spawner { get; set; }

        public Transform Transform
        {
            get
            {
                if (_transform != null) return transform;
                return _transform = GetComponent<Transform>();
            }
        }

        private Transform _transform;

        private void Awake()
        {
            Abilities = GetComponents<IActorAbility>().ToList();

            foreach (var ability in Abilities)
            {
                ability.Initialize(this);
            }
        }

        private void Start()
        {
            PrewarmSetup();
        }

        public virtual void Reset()
        {
            Spawner = null;
            gameObject.SetActive(false);

            foreach (var ability in Abilities.Where(a => a is IResettable).ToList())
            {
                ((IResettable) ability).Reset();
            }
        }

        public virtual void PrewarmSetup()
        {
            gameObject.SetActive(true);
            
            foreach (var ability in Abilities.Where(a => a is IResettable).ToList())
            {
                ((IResettable) ability).PrewarmSetup();
            }
        }
    }
}