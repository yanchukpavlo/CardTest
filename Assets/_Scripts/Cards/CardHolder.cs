using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardHolder : MonoBehaviour
{
    [SerializeField] Image imageIcon;
    [SerializeField] Image imageUpgraded;
    [SerializeField] TextMeshProUGUI textName;
    //[SerializeField] TextMeshProUGUI textDescription;
    [SerializeField] TextMeshProUGUI textHelpInfo;

    Card card;

    public void Setup(Card card)
    {
        this.card = card;
        ChangeVisual(card.status);
    }

    void ChangeVisual(CardStatus status)
    {
        switch (status)
        {
            case CardStatus.Default:
                imageIcon.sprite = card.icon;
                textName.text = card.cardType.ToString();
                break;

            case CardStatus.Upgraded:
                imageIcon.sprite = card.iconUpd;
                textName.text = card.CardNameUpd;
                imageUpgraded.gameObject.SetActive(true);
                break;

            default:
                Debug.LogWarningFormat("WTF state in {0}, with - {1}.", this.GetType(), status);
                break;
        }
    }
}
