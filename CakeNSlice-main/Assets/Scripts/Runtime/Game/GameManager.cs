using Euphrates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelHolderSO _levels;
    [SerializeField] RecipeSO _levelOrder;

    [Header("Game Triggers")]
    [SerializeField] TriggerChannelSO _levelSet;

    [Space]
    [Header("Level")]
    [SerializeField] LevelSO _currentLevel;

    [Space]
    [Header("Economy")]
    [SerializeField] IntSO _collectedCash;
    [SerializeField] IntSO _totalCash;

    private void Start()
    {
        SetCurrentlevel();
    }

    void SetCurrentlevel()
    {
        LevelSO level = _levels.GetLevel(1);

        _currentLevel.Copy(level);

        _levelOrder.Clear();

        _collectedCash.Value = 0;

        for (int i = 0; i < level.Recipe.PieceCount; i++)
        {
            CakeLayerSO piece = level.Recipe.GetPieceAtIndex(i);
            _levelOrder.AddPiece(piece);
        }

        _levelSet.Invoke();
    }
}
