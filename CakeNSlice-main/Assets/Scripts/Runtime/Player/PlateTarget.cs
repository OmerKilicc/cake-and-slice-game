using Euphrates;
using UnityEngine;

public class PlateTarget : MonoBehaviour
{
    Transform _transform;

    [SerializeField] TriggerChannelSO _enableTrigger;
    [SerializeField] TriggerChannelSO _disableTrigger;

    [Space]
    [Header("Movement")]
    [SerializeField] InputReaderSO _inputs;
    [SerializeField] FloatSO _forwardSpeed;
    [SerializeField] FloatSO _horizontalSpeed;
    [SerializeField] float _horizontalDistance = 5f;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _enableTrigger.AddListener(Enable);
        _disableTrigger.AddListener(Disable);

        _inputs.OnTouchDown += TouchDown;
        _inputs.OnTouchMove += TouchMove;
    }

    private void OnDisable()
    {
        _enableTrigger.RemoveListener(Enable);
        _disableTrigger.RemoveListener(Disable);

        _inputs.OnTouchDown -= TouchDown;
        _inputs.OnTouchMove -= TouchMove;
    }

    bool _moveEnabled = false;
    float _xTarget = 0f;
    private void FixedUpdate()
    {
        if (!_moveEnabled)
            return;

        float verticalTarget = _transform.position.z + _forwardSpeed * Time.fixedDeltaTime;
        Vector3 target = new Vector3(_xTarget, 0f, verticalTarget);

        Vector3 nextPos = Vector3.Lerp(_transform.position, target, 4f * Time.fixedDeltaTime);
        _transform.position = nextPos;
    }

    void Enable()
    {
        _moveEnabled = true;
    }

    void Disable()
    {
        _moveEnabled = false;
    }

    Vector2 _initialTouch;
    void TouchDown(Vector2 position) => _initialTouch = position;

    void TouchMove(Vector2 position)
    {
        Vector2 touchMove = position - _initialTouch;

        if (touchMove.magnitude > 50f)
        {
            _initialTouch = position - touchMove.normalized * 50f;
            touchMove = position - _initialTouch;
        }

        float xMagnitude = touchMove.x / 50f;
        _initialTouch = position;

        _xTarget = Mathf.Clamp(_transform.position.x + xMagnitude * _horizontalSpeed, -_horizontalDistance, _horizontalDistance);
    }
}
