using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class PreloaderScreen : MonoBehaviour
    {
        [SerializeField] private int _gameSceneNumber;
        [SerializeField] private Button _playButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(LoadGameScene);
        }

        private void LoadGameScene()
        {
            SceneManager.LoadScene(_gameSceneNumber);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveAllListeners();
        }
    }
}