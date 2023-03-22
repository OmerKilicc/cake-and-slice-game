using Euphrates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] IntSO _level;

    [Space]
    [Header("Triggers")]
    [SerializeField] TriggerChannelSO _nextLevel;
    [SerializeField] TriggerChannelSO _levelSet;

    int _scene
    {
        get
        {
            int levelCount = SceneManager.sceneCountInBuildSettings - 1;
            int convertedIndex = _level.Value - 1;

            return convertedIndex >= levelCount ? convertedIndex % levelCount + 1 : convertedIndex + 1;
        }
    }

    void OnEnable()
    {
        _nextLevel.AddListener(LoadNextLevel);
    }

    void OnDisable()
    {
        _nextLevel.RemoveListener(LoadNextLevel);
    }

    void Start()
    {
        LoadLevel();
    }

    void LoadLevel() => SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive).completed += OnLoadFinish;

    void OnLoadFinish(AsyncOperation op)
    {
        Scene curScene = SceneManager.GetSceneByBuildIndex(_scene);
        SceneManager.SetActiveScene(curScene);
        _levelSet?.Invoke();
    }

    void LoadNextLevel() => SceneManager.UnloadSceneAsync(_scene).completed += OnNextLevelUnloadFinish;

    void OnNextLevelUnloadFinish(AsyncOperation op)
    {
        _level.Value += 1;
        LoadLevel();
    }
}
