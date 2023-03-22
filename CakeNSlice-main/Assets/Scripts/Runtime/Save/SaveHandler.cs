using Euphrates;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    [SerializeField] SaveChannelSO _saveChannel;

    [Space]
    [Header("Triggers")]
    [SerializeField] TriggerChannelSO[] _saveTriggers;
    [SerializeField] TriggerChannelSO[] _loadTriggers;

    [Space]
    [Header("Saved Values")]
    [SerializeField] IntSO _totalCash;
    [SerializeField] IntSO _level;

    private void OnEnable()
    {
        for (int i = 0; i < _saveTriggers.Length; i++)
            _saveTriggers[i].AddListener(SaveGameState);

        for (int i = 0; i < _loadTriggers.Length; i++)
            _loadTriggers[i].AddListener(LoadGameState);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _saveTriggers.Length; i++)
            _saveTriggers[i].RemoveListener(SaveGameState);

        for (int i = 0; i < _loadTriggers.Length; i++)
            _loadTriggers[i].RemoveListener(LoadGameState);
    }

    private void Awake()
    {
        _saveChannel.Load();
    }

    void SaveGameState()
    {
        SaveData data = new SaveData()
        {
            Level = _level.Value,
            Cash = _totalCash.Value
        };

        _saveChannel.Save(data);
    }

    void LoadGameState()
    {

    }
}
