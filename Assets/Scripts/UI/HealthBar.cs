using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class HealthBar : MonoBehaviour, IUIElement
    {
        [SerializeField] private Vector3 _followOffset;
        [SerializeField] private Transform _contentTransform;
        [SerializeField] private Image _sliderFiller;

        private Camera _cachedCamera;
        private IActorAbility _spawnerPlayerAbility;
        private IActor _target;

        public void Initialize(IActor target)
        {
            _target = target;
            
            var abilityActorPlayer =
                _target.Abilities.FirstOrDefault(ability => ability is AbilityActorPlayer);

            if (abilityActorPlayer == null) return;
            
            _spawnerPlayerAbility = abilityActorPlayer;
            ((AbilityActorPlayer) _spawnerPlayerAbility).OnHealthChanged += UpdateHealthInfo;
        }
        
        private void Start()
        {
            _cachedCamera = Camera.main;
        }

        private void UpdateHealthInfo()
        {
            if (_target == null || _spawnerPlayerAbility == null) return;

            _sliderFiller.fillAmount = ((AbilityActorPlayer) _spawnerPlayerAbility).CurrentHealth / ((AbilityActorPlayer) _spawnerPlayerAbility).MaxHealth;
        }

        private void FixedUpdate()
        {
            if (_cachedCamera != null && _target != null)
            {
                _contentTransform.position =
                    _cachedCamera.WorldToScreenPoint(_target.Transform.position + _followOffset);
            }
        }
    }
}