using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class RestartScreen : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private int _gameSceneNumber;

        public void SetScreenVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
        
        private void Awake()
        {
            SetScreenVisibility(false);
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(_gameSceneNumber);
            SetScreenVisibility(false);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}