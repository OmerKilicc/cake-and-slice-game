using Euphrates;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class RecipeItemUI : MonoBehaviour
{
    CanvasGroup _canvasGroup;

    [Header("Compliments")]
    [SerializeField] Image _image;
    [SerializeField] Image _isPickedCheck;

    [Header("Images")]
    [SerializeField] Sprite _notPickedGraphic;
    [SerializeField] Sprite _pickedGraphic;
    [SerializeField] Sprite _wrongPickedGraphic;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        ChangePickedState(0);
    }

    public void DisplayItem(CakeLayerSO piece)
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.DoAlpha(1f, 1f, Ease.OutCubic);

        _image.sprite = piece.Icon;
    }

    public void HideItem()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.DoAlpha(0f, 1f, Ease.OutCubic);
    }

    public void ChangePickedState(int flag)
    {
        flag = Mathf.Clamp(flag, 0, 2);
        _isPickedCheck.gameObject.SetActive(true);

        _isPickedCheck.sprite = flag switch
        {
            0 => _notPickedGraphic,
            1 => _pickedGraphic,
            2 => _wrongPickedGraphic,
            _ => _notPickedGraphic
        };

        if (flag == 0)
            _isPickedCheck.gameObject.SetActive(false);

        if (!_isPickedCheck.TryGetComponent<IAnim>(out var anim))
            return;

        anim.Play();
    }
}
