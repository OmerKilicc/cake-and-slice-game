using Euphrates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    Transform _transform;

    [Header("Toggles")]
    [SerializeField] TriggerChannelSO _startTrigger;
    [SerializeField] TriggerChannelSO _endTrigger;

    [Space, Header("Cut Info")]
    [SerializeField] TriggerChannelSO _cutTrigger;
    [SerializeField] IntSO _currentCut;

    [Space, Header("Cake Info")]
    [SerializeField] RecipeSO _cake;
    [SerializeField] FloatSO _sliceSize;

    [Space, Header("Knife Info")]
    [SerializeField] Transform _knife;
    [SerializeField] Animator _animator;
    [SerializeField] string _cutAnimName;
    [SerializeField] Vector3 _offset;

    float _cakeTopOffset { get => _cake.GetOffsetAtIndex(_cake.PieceCount); }

    private void Awake() => _transform = transform;

    private void OnEnable()
    {
        _startTrigger.AddListener(EnableKnife);
        _endTrigger.AddListener(DisableKnife);
        _cutTrigger.AddListener(OnCut);
        _currentCut.OnChange += OnCutIndexChange;
    }

    private void OnDisable()
    {
        _startTrigger.RemoveListener(EnableKnife);
        _endTrigger.RemoveListener(DisableKnife);
        _cutTrigger.RemoveListener(OnCut);
        _currentCut.OnChange -= OnCutIndexChange;
    }

    void EnableKnife()
    {
        _knife.gameObject.SetActive(true);
        _transform.localPosition = _offset + Vector3.up * _cakeTopOffset;
    }

    void DisableKnife()
    {
        _knife.gameObject.SetActive(false);
    }

    void OnCut()
    {
        _animator.Play(_cutAnimName);
    }

    TweenData twd;
    void OnCutIndexChange(int change)
    {
        int curCut = _currentCut;
        if (_currentCut < 0)
        {
            int cnt = Mathf.RoundToInt(360f / _sliceSize);
            int curCnt = _currentCut.Value % cnt;

            curCut = cnt + curCnt;
        }

        twd?.Stop();
        Quaternion rotate = Quaternion.Euler(0f, _sliceSize * curCut, 0f);
        twd = Tween.DoTween(_transform.rotation, rotate, .1f, Ease.Lerp, (Quaternion rotation) => _transform.rotation = rotation);
    }

}
