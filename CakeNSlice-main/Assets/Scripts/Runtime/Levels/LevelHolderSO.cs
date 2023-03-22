using Euphrates;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Holder", menuName = "Level/Holder", order = 0)]
public class LevelHolderSO : ScriptableObject
{
    [SerializeField] List<LevelSO> _levels;

    public LevelSO GetLevel(int level)
    {
        int index = level - 1;

        return index < _levels.Count && index > -1 ? _levels[index] : _levels.GetRandomItem();
    }
}
