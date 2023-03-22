using Euphrates;
using System.Collections.Generic;
using UnityEngine;

public class SliceDivider : MonoBehaviour
{
    [SerializeField] TriggerChannelSO _enableTrigger;
    [SerializeField] TriggerChannelSO _disableTrigger;
    [Space]
    [SerializeField] SliceHolderSO _sliceHolder;
    [SerializeField] ServicePlate _smallPlatePrefab;
    [SerializeField] FloatSO _plateOffset;
    [SerializeField] Transform _plateFollowTarget;
    [SerializeField] float _plateSize = 1f;
    
    List<ServicePlate> _plates = new List<ServicePlate>();

    private void OnEnable()
    {
        _sliceHolder.OnListSet += DivideSlices;
    }

    private void OnDisable()
    {
        _sliceHolder.OnListSet -= DivideSlices;
    }

    void DivideSlices()
    {
        CreatePlates();

        for (int i = 0; i < _sliceHolder.Objects.Count; i++)
        {
            var slc = _sliceHolder.Objects[i];
            _plates[i].AddSlice(slc);
        }
    }

    void CreatePlates()
    {
        //float offset = GetOffset();
        for (int i = 0; i < _sliceHolder.Objects.Count; i++)
        {
            Vector3 pos = _plateFollowTarget.position + new Vector3(0f, 0f, i * _plateSize); 

            ServicePlate sp = Instantiate(_smallPlatePrefab, pos, Quaternion.identity);
            sp.name = $"Plate-{i}";

            sp.Followed = _plateFollowTarget;
            sp.Index = i;

            _plates.Add(sp);
        }
    }

    // float GetOffset()
    // {
        //float cnt = _sliceHolder.Objects.Count;
        //float rval = _plateSize * .5f * (cnt - 1f);
        //rval += _plateOffset * .5f * (cnt - 1f);

        // return rval;
    // }
}
