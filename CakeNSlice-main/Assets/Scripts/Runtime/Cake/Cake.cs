using Euphrates;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    readonly List<CakeLayer> _layers = new List<CakeLayer>();
    [SerializeField] RecipeSO _currentRecipe;
    [SerializeField] TriggerChannelSO _hideTrigger;
    [SerializeField] SliceableCake _sliceableCake;

    private void OnEnable()
    {
        _hideTrigger.AddListener(HidePieces);

    }

    private void OnDisable()
    {
        _hideTrigger.RemoveListener(HidePieces);
    }

    public void AddPiece(CakeLayer piece)
    {
        _layers.Add(piece);
        _currentRecipe.AddPiece(piece.Data);

        piece.Picked(this, _layers.Count - 1);

        if (piece is not ISliceable sliceable)
            return;

        _sliceableCake.AddLayer(sliceable.SliceableLayer);
    }

    public bool TryGetPieceAtIndex(int index, out CakeLayer piece)
    {
        piece = null;

        if (index < 0 || index > _layers.Count - 1)
            return false;

        piece = _layers[index];

        return true;
    }

    public float GetOffsetAtIndex(int index)
    {
        float offset = 0f;

        for (int i = 0; i < index; i++)
            offset += _layers[i].Data.Height;

        return offset;
    }

    void HidePieces()
    {
        foreach (var layer in _layers)
        {
            if (layer.TryGetComponent<ISliceable>(out var sliceable))
            {
                sliceable.SwitchToSliceable();

                continue;
            }

            layer.gameObject.SetActive(false);
        }
    }
}
