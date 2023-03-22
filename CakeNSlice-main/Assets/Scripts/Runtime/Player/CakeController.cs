using Euphrates;
using UnityEngine;

public class CakeController : MonoBehaviour
{
    Transform _transform;

    [SerializeField] InputReaderSO _inputs;
    [SerializeField] float _maxHorizontalDistance = 6f;
    [SerializeField] float _inputRadius = 100f;
    [SerializeField] FloatSO _maxHorizontalSpeed;
    [SerializeField] FloatSO _maxVerticalSpeed;
    [SerializeField] float _moveLerp = 5f;

    [Space]
    [Header("Triggers")]
    [SerializeField] TriggerChannelSO _phaseOneTrigger;
    [SerializeField] TriggerChannelSO _phaseTwoTrigger;

    bool _moveEnabled = false;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _inputs.OnTouchDown += TouchDown;
        _inputs.OnTouchMove += TouchMove;
        //_inputs.OnTouchUp += TouchUp;

        _phaseOneTrigger.AddListener(OnPhaseOne);
        _phaseTwoTrigger.AddListener(OnPhaseTwo);
    }

    private void OnDisable()
    {
        _inputs.OnTouchDown -= TouchDown;
        _inputs.OnTouchMove -= TouchMove;
        //_inputs.OnTouchUp -= TouchUp;

        _phaseOneTrigger.RemoveListener(OnPhaseOne);
        _phaseTwoTrigger.RemoveListener(OnPhaseTwo);
    }

    float _xTarget = 0f;
    private void FixedUpdate()
    {
        if (!_moveEnabled)
            return;

        float verticalTarget = _transform.position.z + _maxVerticalSpeed * Time.fixedDeltaTime;
        Vector3 target = new Vector3(Mathf.Lerp(_transform.position.x, _xTarget, _moveLerp * Time.fixedDeltaTime), 0f, verticalTarget);

        _transform.position = target;
    }

    Vector2 _initialTouch;
    void TouchDown(Vector2 position) => _initialTouch = position;

    void TouchMove(Vector2 position)
    {
        Vector2 touchMove = position - _initialTouch;

        if (touchMove.magnitude > _inputRadius)
        {
            _initialTouch = position - touchMove.normalized * _inputRadius;
            touchMove = position - _initialTouch;
        }

        float xMagnitude = touchMove.x / _inputRadius;
        _initialTouch = position;

        _xTarget = Mathf.Clamp(_xTarget + xMagnitude * _maxHorizontalSpeed, -_maxHorizontalDistance, _maxHorizontalDistance);
    }

    void OnPhaseOne()
    {
        _moveEnabled = true;
    }

    void OnPhaseTwo()
    {
        _moveEnabled = false;
    }

    //void TouchUp(Vector2 position) => _target = Vector3.zero;
}
