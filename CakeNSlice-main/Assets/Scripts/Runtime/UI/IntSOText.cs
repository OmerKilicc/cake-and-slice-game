using Euphrates;
using TMPro;
using UnityEngine;

public class IntSOText : MonoBehaviour
{
    [SerializeField] IntSO _int;
    [SerializeField] TextMeshProUGUI _text;

    void OnEnable()
    {
        UpdateText(0);
        _int.OnChange += UpdateText;
    }

    void OnDisable() => _int.OnChange -= UpdateText;

    void UpdateText(int _) => _text.text = _int.ToString();
}
