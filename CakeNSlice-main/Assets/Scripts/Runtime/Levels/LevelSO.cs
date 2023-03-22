using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level/Level", order = 0)]
public class LevelSO : ScriptableObject
{
    public string Name;
    public int Level;
    public GameObject LevelPrefab;
    public RecipeSO Recipe;
    public ushort SliceCount;

    public void Copy(LevelSO copied)
    {
        Name = copied.Name;
        Level = copied.Level;
        LevelPrefab = copied.LevelPrefab;
        Recipe = copied.Recipe;
        SliceCount = copied.SliceCount;
    }
}
