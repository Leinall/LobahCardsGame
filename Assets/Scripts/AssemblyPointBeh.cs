using System.Collections.Generic;
using UnityEngine;

public class AssemblyPointBeh : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private Card[] cards; // we use an array as we always recieve the cards in the same order each time, otherwise we shall use a dictionary
    private GameObject[] cardsGo; // we use an array as we always recieve the cards in the same order each time, otherwise we shall use a dictionary
    private int cardsCounter;

    private List<int> winnersIndices = new List<int>();
    private int roundsCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        cards = new Card[GameManager.instance.playersNumber];
        cardsGo = new GameObject[GameManager.instance.playersNumber];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("card"))
        {
            print("entered");
            _audioSource.Play();

            cardsGo[cardsCounter] = other.gameObject;
            cards[cardsCounter] = other.GetComponent<CardInfo>().card;
            cardsCounter++;
            if (cardsCounter == cards.Length)
            {
                Card highestCard = cards[0];
                int winnerIndex = 0;
                for (int i = 0; i < cards.Length; i++)
                {
                    if (cards[i].cardNumber > highestCard.cardNumber)
                    {
                        highestCard = cards[i];
                        winnerIndex = i;
                    }
                    else if (cards[i].cardNumber == highestCard.cardNumber)
                    {
                        if (cards[i].cardsuit > highestCard.cardsuit)
                        {
                            highestCard = cards[i];
                            winnerIndex = i;
                        }
                    }

                    GameReferences.instance.mainPlayer.RemovePlayedCard();
                    GameReferences.instance.mainPlayer.TurnOffDragEffect();
                }
                roundsCounter++;
                print("round number +" + roundsCounter);
                if (roundsCounter >= 13)
                {
                    GameReferences.instance.uiHandler.DisplayWinnersData();
                }
                else
                {
                    // UI round feedback shall appear
                    GameReferences.instance.uiHandler.EnableFeedbackPanel(winnerIndex);
                    winnersIndices.Add(winnerIndex);
                    cardsCounter = 0;
                }
                KillThemAll();
            }
        }
    }

    public List<int> getWinnerIndices()
    {
        return winnersIndices;
    }

    private void KillThemAll()
    {
        foreach (var cardgo in cardsGo)
        {
            Destroy(cardgo);
        }
    }

}
