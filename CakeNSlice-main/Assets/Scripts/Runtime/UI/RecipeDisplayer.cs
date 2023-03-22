using Euphrates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDisplayer : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField] TriggerChannelSO _startDisplay;
    [SerializeField] TriggerChannelSO _stopDisplay;

    [Space]
    [Header("Recipes")]
    [SerializeField] RecipeSO _currentCake;
    [SerializeField] RecipeSO _levelOrder;

    [Space]
    [Header("Compliments")]
    [SerializeField] Transform _holder;
    [SerializeField] GameObject _recipeItemPrefab;
    List<RecipeItemUI> _items = new List<RecipeItemUI>();

    private void OnEnable()
    {
        _currentCake.OnPieceAdded += OnItemPicked;

        _startDisplay.AddListener(OnStartDisplay);
        _stopDisplay.AddListener(OnStopDisplay);
    }

    private void OnDisable()
    {
        _currentCake.OnPieceAdded -= OnItemPicked;

        _startDisplay.RemoveListener(OnStartDisplay);
        _stopDisplay.RemoveListener(OnStopDisplay);
    }

    void OnStartDisplay()
    {
        if (_items.Count > 0)
            return;

        for (int i = 0; i < _levelOrder.PieceCount; i++)
        {
            CakeLayerSO piece = _levelOrder.GetPieceAtIndex(i);

            RecipeItemUI uiItem = Instantiate(_recipeItemPrefab, _holder).GetComponent<RecipeItemUI>();
            uiItem.DisplayItem(piece);
            _items.Add(uiItem);
        }
    }
      
    void OnStopDisplay()
    {
        if (_items.Count < 1)
            return;
    }

    void OnItemPicked()
    {
        int lastIndex = _currentCake.PieceCount - 1;

        if (lastIndex > _levelOrder.PieceCount - 1)
            return;

        int flag = _currentCake.GetPieceAtIndex(lastIndex) == _levelOrder.GetPieceAtIndex(lastIndex) ? 1 : 2;
        _items[lastIndex].ChangePickedState(flag);
    }
}
