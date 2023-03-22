using Euphrates;
using System.Collections.Generic;
using UnityEngine;

public class SliceableCakeLayer : MonoBehaviour, ICoverable
{
    [SerializeField] CakeSlicePiece[] _pieces;
    List<int> _cuts = new List<int>();

    [SerializeField] FloatSO _piecePercent;

    int _sliceCount = 0;

    public void Slice(int index)
    {
        index = Mathf.Clamp(index, 0, _pieces.Length - 1);
        int halfLen = Mathf.RoundToInt(_pieces.Length * .5f);

        index = index > halfLen - 1 ? index - halfLen : index;
        _cuts.Add(index);
        _cuts.Add(index + halfLen);

        _sliceCount ++;

        _cuts.Sort();
    }

    public List<CakeSliceLayer> GeneratePieces()
    {
        List<CakeSliceLayer> slices = new List<CakeSliceLayer>();

        for (int i = 0; i < _sliceCount; i++)
        {
            GameObject go = new GameObject($"Slice {i}");
            go.transform.position = transform.position;
            CakeSliceLayer piece = go.AddComponent<CakeSliceLayer>();

            int sz = 0;
            for (int j = _cuts[i]; j < _cuts[i + 1]; j++)
            {
                sz++;
                piece.AddPiece(_pieces[j % _pieces.Length], _piecePercent);
            }

            GameObject gom = new GameObject($"Slice {i} - Mirror");
            gom.transform.position = transform.position;
            CakeSliceLayer piecem = gom.AddComponent<CakeSliceLayer>();

            int halfLen = Mathf.RoundToInt(_pieces.Length * .5f);
            for (int j = _cuts[i] + halfLen; j < _cuts[i] + halfLen + sz; j++)
                piecem.AddPiece(_pieces[j % _pieces.Length], _piecePercent);

            slices.Add(piece);
            slices.Add(piecem);
        }

        return slices;
    }

    public void Cover(Material coverMaterial)
    {
        for (int i = 0; i < _pieces.Length; i++)
        {
            _pieces[i].Cover(coverMaterial);
        }
    }
}
