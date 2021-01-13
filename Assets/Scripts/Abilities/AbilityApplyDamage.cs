using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Collider))]
    public class AbilityApplyDamage : MonoBehaviour, IActorAbility
    {
        [SerializeField] private float _damageValue;
        [SerializeField] private List<string> _targetTags = new List<string>();
        
        public IActor Actor { get; set; }

        private IActor _currentTarget;

        public void Initialize(IActor actor)
        {
            Actor = actor;
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
            var collisionActor = col.transform.GetComponent<IActor>();

            if (collisionActor == null || !_targetTags.Contains(col.gameObject.tag))
            {
                return;
            }
            
            _currentTarget = collisionActor;
            Execute();
        }

        public void Execute()
        {
            var actorPlayerAbility =
                _currentTarget.Abilities.FirstOrDefault(ability => ability is AbilityActorPlayer) as AbilityActorPlayer;

            if (actorPlayerAbility == null)
            {
                return;
            }
            
            var damage = _damageValue * actorPlayerAbility.Armor;
            actorPlayerAbility.UpdateHealth(-damage);
        }
    }
}