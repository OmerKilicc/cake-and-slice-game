using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [Header("On Trigger Enter")]
    [SerializeField] UnityEvent _onTriggerEnterEvents;
    [SerializeField] LayerMask _triggerEnterLayers;
    [SerializeField] string[] _triggerEnterTags;

    [Space]
    [Header("On Trigger Exit")]
    [SerializeField] UnityEvent _onTriggerExitEvents;
    [SerializeField] LayerMask _triggerExitLayers;
    [SerializeField] string[] _triggerExitTags;

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        int layer = go.layer;
        int pow = Mathf.RoundToInt(Mathf.Pow(2, layer));

        string tag = go.tag;
        bool tagObserved = false;

        for (int i = 0; i < _triggerEnterTags.Length; i++)
        {
            if (_triggerEnterTags[i] == tag)
            {
                tagObserved = true;
                break;
            }
        }

        if ((pow & _triggerEnterLayers.value) != pow && !tagObserved)
            return;

        _onTriggerEnterEvents.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;

        int layer = go.layer;
        int pow = Mathf.RoundToInt(Mathf.Pow(2, layer));

        string tag = go.tag;
        bool tagObserved = false;

        for (int i = 0; i < _triggerExitTags.Length; i++)
        {
            if (_triggerExitTags[i] == tag)
            {
                tagObserved = true;
                break;
            }
        }

        if ((pow & _triggerExitLayers.value) != pow && !tagObserved)
            return;

        _onTriggerExitEvents.Invoke();
    }
}
