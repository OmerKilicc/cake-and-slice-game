using Euphrates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutMarkHandler : MonoBehaviour
{
    [SerializeField] TriggerChannelSO _enableTrigger;
    [SerializeField] TriggerChannelSO _disableTrigger;

    [Space]
    [SerializeField] TriggerChannelSO _cut;
    [SerializeField] IntSO _cutIndex;
    [SerializeField] FloatSO _sliceSize;

    [Space]
    [SerializeField] RecipeSO _cake;
    [SerializeField] GameObject _markPrefab;

    readonly Vector3 MARK_HIDDEN = new Vector3(0f, 1f, 1f);
    List<GameObject> _marks = new List<GameObject>();

    bool _enabled = false;
    private void OnEnable()
    {
        _enableTrigger.AddListener(EnableMarkHandler);
        _disableTrigger.AddListener(DisableMarkHandler);

        _cut.AddListener(OnCut);
    }

    private void OnDisable()
    {
        _enableTrigger.RemoveListener(EnableMarkHandler);
        _disableTrigger.RemoveListener(DisableMarkHandler);

        _cut.RemoveListener(OnCut);
    }

    void EnableMarkHandler()
    {
        _enabled = true;
    }

    void DisableMarkHandler()
    {
        _enabled = false;

        foreach (var mark in _marks)
        {
            mark.transform.DoScale(MARK_HIDDEN, .1f, Ease.Lerp, null, () => mark.SetActive(false));
        }
    }

    void OnCut()
    {
        if (!_enabled) 
            return;

        GameObject go = Instantiate(_markPrefab);
        go.transform.SetParent(transform);

        float offset = _cake.GetOffsetAtIndex(_cake.PieceCount);
        Vector3 localPos = new Vector3(0f, offset, 0f);

        int curCut = _cutIndex;
        if (curCut < 0)
        {
            int cnt = Mathf.RoundToInt(360f / _sliceSize);
            int curCnt = _cutIndex.Value % cnt;

            curCut = cnt + curCnt;
        }

        Quaternion rot = Quaternion.Euler(0f, _sliceSize * curCut, 0f);
        go.transform.SetPositionAndRotation(transform.position + localPos, rot);

        go.transform.localScale = MARK_HIDDEN;
        go.transform.DoScale(Vector3.one, .5f);

        _marks.Add(go);
    }
}
