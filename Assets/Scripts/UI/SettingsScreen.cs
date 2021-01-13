using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class SettingsScreen : MonoBehaviour
    {
        [SerializeField] private Button _showSettingsButton;
        [SerializeField] private GameObject _screenContent;
        
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _restartButton;

        [SerializeField] private int _gameSceneNumber;
        [SerializeField] private int _preloaderSceneNumber;

        private void Awake()
        {
            _showSettingsButton.onClick.AddListener(SetScreenContentVisibility);
            _exitButton.onClick.AddListener(ExitGame);
            _restartButton.onClick.AddListener(RestartGame);
            
            _screenContent.SetActive(false);
        }

        private void SetScreenContentVisibility()
        {
            _screenContent.SetActive(!_screenContent.activeSelf);
        }

        private void ExitGame()
        {
            SceneManager.LoadScene(_preloaderSceneNumber);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(_gameSceneNumber);
        }

        private void OnDestroy()
        {
            _showSettingsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}