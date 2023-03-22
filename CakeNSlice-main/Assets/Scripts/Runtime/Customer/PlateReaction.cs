using Euphrates;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlateReaction : MonoBehaviour
{
    [SerializeField] Customer _customer;

    [Space]
    [Header("Animations")]
    [SerializeField] Animator _animator;
    [SerializeField] string _perfectTrigger;
    [SerializeField] string _successTrigger;
    [SerializeField] string _failTrigger;

    [Space]
    [Header("UI")]
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Image _cakeSizeIcon;
    [SerializeField] Image _orderSatisfactionIcon;

    private void Start() => _animator.SetFloat("Offset", Random.Range(0f, 1f));

    private void OnEnable()
    {
        if (!_customer)
            return;

        _customer.OnPlateReceived += React;
    }

    private void OnDisable()
    {
        if (!_customer)
            return;

        _customer.OnPlateReceived -= React;
    }

    void React(float orderSimilarness, bool sliceSizeSatisfaction)
    {
        Tween.DoTween(0f, .9f, .25f, Ease.Lerp, v => _canvasGroup.alpha = v);

        Color sizeColor = sliceSizeSatisfaction ? Color.green : Color.red;
        Color orderColor = Color.Lerp(Color.red, Color.green, orderSimilarness);

        _cakeSizeIcon.color = sizeColor;
        _orderSatisfactionIcon.color = orderColor;

        bool perfectOrder = Mathf.Approximately(orderSimilarness, 1f);

        if (sliceSizeSatisfaction && perfectOrder)
            Perfect();
        else if (sliceSizeSatisfaction || perfectOrder)
            Complete(sliceSizeSatisfaction);
        else
            Fail();
    }

    void Perfect()
    {
        _animator.SetTrigger(_perfectTrigger);
    }

    void Complete(bool sizeSatisfaction)
    {
        _animator.SetTrigger(_successTrigger);
    }

    void Fail()
    {
        _animator.SetTrigger(_failTrigger);
    }
}
