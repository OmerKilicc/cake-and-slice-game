using Euphrates;
using UnityEngine;

public class ServicePlate : MonoBehaviour
{
    Transform _transform;

    [SerializeField] AnimationCurveSO _lerpMove;
    [SerializeField] FloatSO _plateDelay;
    [SerializeField] FloatSO _plateOffset;

    [HideInInspector] public Transform Followed;
    [HideInInspector] public int Index;

    CakeSlice _slice;
    public CakeSlice Slice => _slice;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (!Followed)
            return;

        Vector3 target = Followed.transform.position + Vector3.forward * _plateOffset * Index;
        float xLerp = Mathf.Lerp(_transform.position.x, target.x, (1f - _lerpMove.Value.Evaluate(Index)) * Time.fixedDeltaTime * _plateDelay);
        target.x = xLerp;

        _transform.position = target;
    }

    public void AddSlice(CakeSlice slice)
    {
        _slice = slice;

        slice.transform.SetParent(transform, true);
        Tween.DoTween(Vector3.back * 5f, Vector3.right * .2f, 1f, Ease.Lerp, (Vector3 pos) => slice.transform.localPosition = pos);
    }
}
