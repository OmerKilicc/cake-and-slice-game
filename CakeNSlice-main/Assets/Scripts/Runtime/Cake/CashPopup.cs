using Euphrates;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup), typeof(TextMeshProUGUI))]
public class CashPopup : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    TextMeshProUGUI _text;

    [SerializeField] IntSO _collectedCash;

    private void OnEnable() => _collectedCash.OnChange += CollectedUpdated;

    private void OnDisable() => _collectedCash.OnChange -= CollectedUpdated;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void CollectedUpdated(int change)
    {
        if (change > 0)
        {
            _text.text = $"+{change}$";
            _canvasGroup.DoAlpha(1f, .5f, Ease.Lerp, null, () => _canvasGroup.DoAlpha(0f, .5f));
        }

        else 
        {
            _text.text = $"-{change}$";
            _canvasGroup.DoAlpha(1f, .5f, Ease.Lerp, null, () => _canvasGroup.DoAlpha(0f, .5f));
        }



    }
}
