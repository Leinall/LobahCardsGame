using DG.Tweening;
using UnityEngine;
public class MainPlayerBehaviour : CardGamePlayer
{
    public GameObject selectedCard;
    public void UnFoldMyCards()
    {
        foreach (var card in myCards)
        {
            card.transform.DORotateQuaternion(new Quaternion(0, 0, 0, 0), 2f)
           .OnComplete(() =>
            {
                card.AddComponent<CardDragEffect>();
            });
        }

    }

    public override void playTurn()
    {
        foreach (var card in myCards)
        {
            card.GetComponent<CardDragEffect>().canBeDragged = true;
        }
    }

    public void AssignSelectedCard(GameObject cardgo)
    {
        selectedCard = cardgo;

    }
    public void RemovePlayedCard()
    {
        myCards.Remove(selectedCard);
    }

    public void TurnOffDragEffect()
    {
        anim.enabled = false;
        foreach (var card in myCards)
        {
            card.GetComponent<CardDragEffect>().canBeDragged = false;
        }
    }
}
