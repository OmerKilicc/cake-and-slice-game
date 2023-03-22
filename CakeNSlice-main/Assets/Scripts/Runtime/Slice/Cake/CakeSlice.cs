using System.Collections.Generic;
using UnityEngine;

public class CakeSlice : MonoBehaviour
{
    List<CakeSliceLayer> _layers = new List<CakeSliceLayer>();
    public float Percent
    {
        get
        {
            if (_layers.Count == 0)
                return 0;

            return _layers[0].Percent;
        }
    }

    public void AddLayer(params CakeSliceLayer[] layers)
    {
        for (int i = 0; i < layers.Length; i++)
            _layers.Add(layers[i]);
    }
}
