using Euphrates;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Multi-Channel Listener")]
public class MultiChannelListener : MonoBehaviour
{
    [SerializeField] List<ListenedChannel> _channels;

    private void OnEnable()
    {
        foreach (var channel in _channels)
            channel.Channel.AddListener(channel.OnTrigger.Invoke);
    }

    private void OnDisable()
    {
        foreach (var channel in _channels)
            channel.Channel.RemoveListener(channel.OnTrigger.Invoke);
    }

    [System.Serializable]
    internal struct ListenedChannel
    {
        public string Name;
        public TriggerChannelSO Channel;
        public UnityEvent OnTrigger;
    }
}
