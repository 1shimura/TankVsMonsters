using System.Collections.Generic;
using System.Linq;
using Scripts.UI;
using UnityEngine;

namespace Scripts
{
    public class UIReceiver : Actor
    {
        [Header("Each UI Behaviour must have a component derived from IUIElement")]
        [SerializeField] protected List<MonoBehaviour> _uiBehaviours = new List<MonoBehaviour>();

        private List<IUIElement> _uiElements;

        private void Start()
        {
            _uiElements = _uiBehaviours
                .Select(uiBehaviour => uiBehaviour.GetComponent<IUIElement>())
                .Where(uiElement => uiElement != null)
                .ToList();

            if (Spawner == null)
            {
                return;
            }

            foreach (var uiElement in _uiElements)
            {
                uiElement.Initialize(Spawner);
            }
        }
    }
}