using Euphrates;
using UnityEngine;

[RequireComponent(typeof(LocalPositionLerp))]
public class CoreCakeLayer : CakeLayer, ICoverable, ISliceable
{
    LocalPositionLerp _localPositionLerp;

    [SerializeField] FloatSO _baseLerp;
    [SerializeField] FloatSO _lerpOffset;

    [Space, Header("References")]
    [SerializeField] GameObject _visual;
    [SerializeField] SliceableCakeLayer _sliceableLayer;
    [SerializeField] MeshRenderer _cover;

    public SliceableCakeLayer SliceableLayer => _sliceableLayer;

    private void Awake()
    {
        _localPositionLerp = GetComponent<LocalPositionLerp>();
    }

    private void Start()
    {
        _sliceableLayer.gameObject.SetActive(false);
    }

    public void SetFollowPosition(Transform parent, Vector3 position, float lerp) => _localPositionLerp.SetFollow(parent, position, lerp);
    public void StopFollow() => _localPositionLerp.StopFollow();

    public override void Picked(Cake cake, int index)
    {
        float offset = cake.GetOffsetAtIndex(index);
        Vector3 localPosition = new Vector3(0f, offset, 0f);

        float lerp = _baseLerp - _lerpOffset * index;
        SetFollowPosition(cake.transform, localPosition, lerp);
    }

    public void Cover(Material coverMaterial)
    {
        if (_cover == null)
            return;

        _cover.gameObject.SetActive(true);
        _cover.material = coverMaterial;
    }

    public void SwitchToSliceable()
    {
        _visual.SetActive(false);
        _sliceableLayer.gameObject.SetActive(true);

        if (_cover.gameObject.activeSelf)
        {
            _sliceableLayer.Cover(_cover.material);
        }
    }
}
