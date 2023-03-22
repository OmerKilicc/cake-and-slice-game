using UnityEngine;

public class LocalPositionLerp : MonoBehaviour
{
    Transform _transform;

    Vector3 _targetLocalPosition;
    Transform _targetParent;
    float _lerpAmount = 5f;

    bool _isSet = false;

    private void Awake()
    {
        _transform = transform;
    }

    public void SetFollow(Transform parent, Vector3 localPosition, float lerp)
    {
        _isSet = true;
        _targetParent = parent;
        _targetLocalPosition = localPosition;
        _lerpAmount = lerp;
    }

    public void StopFollow() => _isSet = false;

    private void FixedUpdate()
    {
        if (!_isSet)
            return;

        Vector3 localized = _targetParent.position + _targetLocalPosition;
        Vector3 lerped = Vector3.Lerp(_transform.position, localized, Mathf.Clamp(_lerpAmount * Time.fixedDeltaTime, .1f, 1f));
        _transform.position = new Vector3(lerped.x, lerped.y, localized.z);
    }
}
