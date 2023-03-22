using UnityEngine;
using UnityEngine.UI;

public class ImageColorChangeListener : ChannelListener
{
    [Space]
    [SerializeField] Image _image;
    [SerializeField] Color _color;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnInvoked.AddListener(ChangeColor);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnInvoked.RemoveListener(ChangeColor);
    }

    void ChangeColor() => _image.color = _color;
}
