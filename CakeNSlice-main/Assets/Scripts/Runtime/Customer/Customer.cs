using Euphrates;
using UnityEngine;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{
    [SerializeField] Transform _plateTarget;
    [SerializeField] LevelSO _level;

    [Header("Recipes")]
    [SerializeField] RecipeSO _cake;
    [SerializeField] RecipeSO _order;

    /// <summary>
    /// Event that invokes when customer recieves their plate. First parameter is order completion and the other one is slice size happiness. 
    /// </summary>
    public event UnityAction<float, bool> OnPlateReceived;

    bool _serviced = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_serviced || !other.TryGetComponent<ServicePlate>(out var plate))
            return;

        _serviced = true;
        
        TakePlate(plate);
    }

    void TakePlate(ServicePlate plate)
    {
        plate.Followed = null;
        plate.transform.SetParent(_plateTarget, true);

        Tween.DoTween(plate.transform.localPosition, Vector3.zero, .5f, Ease.OutCirc, p => plate.transform.localPosition = p);

        float expectedSize = 180f / (float)_level.SliceCount;
        bool sizeMatched = Mathf.Approximately(expectedSize, plate.Slice.Percent);

        float recipeSimilarness = _order.Similarness(_cake);

        OnPlateReceived?.Invoke(recipeSimilarness, sizeMatched);
    }
}
