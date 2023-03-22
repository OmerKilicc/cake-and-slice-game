using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTriggererCollider : MonoBehaviour
{
    [SerializeField] UnityEvent _onEnter;
    [SerializeField] UnityEvent _onExit;

    [SerializeField] LayerMask _observedLayers;
    [SerializeField] List<string> _observedTags;

    private void OnTriggerEnter(Collider other)
    {
        int pow = Mathf.RoundToInt(Mathf.Pow(2, other.gameObject.layer));

        if ((_observedLayers != 0 && (_observedLayers & pow) != pow) 
            || (_observedTags != null && _observedTags.Count != 0 && !_observedTags.Exists(t => other.gameObject.CompareTag(t))))
            return;

        _onEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        int pow = Mathf.RoundToInt(Mathf.Pow(2, other.gameObject.layer));

        if ((_observedLayers != 0 && (_observedLayers & pow) != pow)
            || (_observedTags != null && _observedTags.Count != 0 && !_observedTags.Exists(t => other.gameObject.CompareTag(t))))
            return;

        _onExit.Invoke();
    }
}
