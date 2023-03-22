using UnityEngine;

[CreateAssetMenu(fileName = "New Save Channel", menuName = "SO Channels/Save")]
public class SaveChannelSO : ScriptableObject
{
    [SerializeField] SaveData _defaultValues;
    [SerializeField] string SAVE_NAME = "save_data_proto";


    public void Save(SaveData save)
    {
        try
        {
            string saveString = JsonUtility.ToJson(save);
            PlayerPrefs.SetString(SAVE_NAME, saveString);
            PlayerPrefs.Save();
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    public SaveData Load()
    {
        try
        {
            if (!PlayerPrefs.HasKey(SAVE_NAME))
                return _defaultValues;

            string saveString = PlayerPrefs.GetString(SAVE_NAME);
            SaveData data = JsonUtility.FromJson<SaveData>(saveString);
            return data;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}

public struct SaveData
{
    public int Level;
    public int Cash;
}
