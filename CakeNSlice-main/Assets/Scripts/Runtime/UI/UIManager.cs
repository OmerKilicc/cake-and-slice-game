using Euphrates;
using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] ScreenTriggerCouple[] _screens = new ScreenTriggerCouple[1];

    private void OnEnable()
    {
        for (int i = 0; i < _screens.Length; i++)
        {
            _screens[i].Trigger.AddListener(DisableAll);
            _screens[i].Trigger.AddListener(_screens[i].Activate);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _screens.Length; i++)
        {
            _screens[i].Trigger.RemoveListener(DisableAll);
            _screens[i].Trigger.RemoveListener(_screens[i].Activate);
        }
    }

    void DisableAll()
    {
        for (int i = 0; i < _screens.Length; i++)
            _screens[i].Screen.SetActive(false);
    }

    [System.Serializable]
    struct ScreenTriggerCouple
    {
        public string Name;
        public TriggerChannelSO Trigger;
        public GameObject Screen;

        public void Activate() => Screen.SetActive(true);
    }
}
