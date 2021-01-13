using System;
using UnityEngine;

namespace Scripts
{
    public class AbilityActorPlayer : MonoBehaviour, IActorAbility, IResettable
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] [Range(0f, 1f)] private float _armor;

        public IActor Actor { get; set; }
        public Action<IActor> OnDeath { get; set; }
        public Action OnHealthChanged { get; set; }
        
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;
        public float Armor => _armor;
        public bool IsAlive => _currentHealth > 0;

        private float _currentHealth;

        public void Initialize(IActor actor)
        {
            Actor = actor;
        }
        
        public void Execute()
        {
        }

        public void PrewarmSetup()
        {
            UpdateHealth(_maxHealth);
        }

        public void Reset()
        {
        }
        
        public void UpdateHealth(float delta)
        {
            var newHealth = _currentHealth + delta;
            _currentHealth = newHealth < 0 ? 0 : newHealth > _maxHealth ? _maxHealth : newHealth;
            
            OnHealthChanged?.Invoke();

            if (!IsAlive)
            {
                DeathHandle();
            }
        }
        
        private void DeathHandle()
        {
            OnDeath?.Invoke(Actor);
        }
    }
}