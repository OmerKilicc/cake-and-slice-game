using Euphrates;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] IntSO _collectedMoney;
    [SerializeField] TriggerChannelSO _endPhase;

    bool _triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_triggered || other.gameObject.layer != 8 || !other.TryGetComponent<PlateTarget>(out var _))
            return;

        _triggered = true;

        _endPhase.Invoke();
    }
}
