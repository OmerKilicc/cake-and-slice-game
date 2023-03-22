using Euphrates;
using UnityEngine;

public class CashHandler : MonoBehaviour
{
    [SerializeField] RecipeSO _order;
    [SerializeField] RecipeSO _currentCake;
    [SerializeField] IntSO _collectedCash;

    int _multiplier = 1;

    private void OnEnable()
    {
        _currentCake.OnPieceAdded += UpdateCash;
    }

    private void OnDisable()
    {
        _currentCake.OnPieceAdded -= UpdateCash;
    }

    private void UpdateCash()
    {
        if (_currentCake.PieceCount > _order.PieceCount)
        {
            _multiplier = 1;
            return;
        }

        int lastIndex = _currentCake.PieceCount - 1;
        CakeLayerSO added = _currentCake.GetPieceAtIndex(lastIndex);
        CakeLayerSO wanted = _order.GetPieceAtIndex(lastIndex);

        if (added != wanted)
        {
            _multiplier = 1;
            return;
        }

        _collectedCash.Value += added.Value * _multiplier++;
    }


}
