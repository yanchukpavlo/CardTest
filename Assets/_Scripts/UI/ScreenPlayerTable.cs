using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenPlayerTable : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textCurrencyAmount;
    [SerializeField] Button buttonUpgrade;
    [SerializeField] Button buttonDestroy;

    private void OnEnable()
    {
        Player.OnChangeCurrency += SetTextCurrency;
    }

    private void OnDisable()
    {
        Player.OnChangeCurrency -= SetTextCurrency;
    }

    private void SetTextCurrency(int amount) => textCurrencyAmount.text = amount.ToString();
}
