using Euphrates;
using System.Collections.Generic;
using UnityEngine;

public class SliceableCake : MonoBehaviour
{
    [SerializeField] TriggerChannelSO _createTrigger;
    [SerializeField] RecipeSO _cake;

    List<SliceableCakeLayer> _layers = new List<SliceableCakeLayer>();

    public void AddLayer(SliceableCakeLayer layer)
    {
        _layers.Add(layer);
    }

    public List<CakeSlice> Slice(params int[] cuts)
    {
        for(int l = 0; l < cuts.Length; l++)
        {
            foreach (var layer in _layers)
            {
                layer.Slice(cuts[l]);
            }
        }

        var l1 = _layers[0];

        var pieces = l1.GeneratePieces();

        List<CakeSlice> slices = new List<CakeSlice>();

        for (int i = 0; i < pieces.Count; i++)
        {
            GameObject go = new GameObject("Slice -- " + i);
            go.transform.position = transform.position;

            slices.Add(go.AddComponent<CakeSlice>());
            pieces[i].transform.SetParent(go.transform);
        }

        for (int i = 1; i < _layers.Count; i++)
        {
            SliceableCakeLayer l = _layers[i];

            var sls = l.GeneratePieces();

            for (int j = 0; j < sls.Count; j++)
            {
                CakeSliceLayer slc = sls[j];
                slc.transform.SetParent(slices[j].transform);
                slices[j].AddLayer(slc);
            }
        }

        return slices;
    }
}
