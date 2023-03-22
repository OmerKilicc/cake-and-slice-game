using Euphrates;
using UnityEngine;
using UnityEngine.Events;

public class ChannelListener : MonoBehaviour
{
    [SerializeField] TriggerChannelSO _listenedChannel;
    public UnityEvent OnInvoked;

    protected virtual void OnEnable()
    {
        _listenedChannel.AddListener(OnInvoked.Invoke);
    }

    protected virtual void OnDisable()
    {
        _listenedChannel.RemoveListener(OnInvoked.Invoke);
    }
}
