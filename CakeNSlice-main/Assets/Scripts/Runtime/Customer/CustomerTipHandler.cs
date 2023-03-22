using Euphrates;
using UnityEngine;

[RequireComponent(typeof(Customer))]
public class CustomerTipHandler : MonoBehaviour
{
    Customer _customer;

    [SerializeField] IntSO _collectedMoney;

    [Space]
    [Header("Bonuses")]
    [SerializeField] IntSO _sliceSizeFullBonus;
    [SerializeField] IntSO _orderFullBonus;

    private void Awake()
    {
        _customer = GetComponent<Customer>();
    }

    private void OnEnable()
    {
        _customer.OnPlateReceived += OnPlateReceived;
    }

    private void OnDestroy()
    {
        _customer.OnPlateReceived -= OnPlateReceived;
    }

    void OnPlateReceived(float orderCompleteness, bool sliceSize)
    {
        int tip = sliceSize ? _sliceSizeFullBonus : 0;
        
        tip += Mathf.RoundToInt(orderCompleteness * (float)_orderFullBonus);
        _collectedMoney.Value += tip;
    }
}