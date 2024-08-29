using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CardGameNPC : CardGamePlayer
{
    private Stack<GameObject> mysortedcards = new Stack<GameObject>();

    private GameObject selectedCard;
    private UnityAction OnSortingComplete;
    private void Start()
    {
        //quickSort(GameManager.instance.gameCards.cards, 0, GameManager.instance.gameCards.cards.Length - 1);
        //Invoke("SortMyCards", 5); // TESTING
        OnSortingComplete += printsorting;
    }

    public Card playCard()
    {
        return null;
    }

    public void SortMyCards()
    {
        //Debug.Log("Sorting");
        //Array.Copy(GameManager.instance.gameCards.cards, npcCards, 13);

        //var str = "";
        //foreach (GameObject card in myCards)
        //{
        //    var tempcard = card.GetComponent<CardInfo>().card;
        //    str += tempcard.cardNumber + "," + tempcard.cardsuit + "\t";
        //}
        //print(this.gameObject.name + "," + str);

        //quickSort(GameManager.instance.gameCards.cards, 0, GameManager.instance.gameCards.cards.Length - 1);

        quickSort(myCards, 0, myCards.Count - 1);

        Invoke("onSortingComplete", 5);
        //TESTING
        //while (mysortedcards.Count >= 1)
        //{
        //    print(mysortedcards.Peek());
        //    mysortedcards.Pop();
        //}
    }

    public void onSortingComplete()
    {
        OnSortingComplete.Invoke();
        GameManager.instance.ReadyToJoinRound();
    }

    public void printsorting() // change this function's name
    {
        foreach (GameObject card in myCards)
        {
            mysortedcards.Push(card);
        }

        //Debug.Log(this.gameObject.name + " Sorted");

        //var str = "";
        //foreach (GameObject cardgo in myCards)
        //{
        //    var card = cardgo.GetComponent<CardInfo>().card;
        //    str += card.cardNumber + "," + card.cardsuit + "\t";
        //}
        //print(this.gameObject.name + "," + str);

    }

    public override void playTurn()
    {
        selectedCard = mysortedcards.Pop();
        Sequence seq = DOTween.Sequence();
        seq.Append(selectedCard.transform.DOMove(GameManager.instance.AssemblyPointChildren[playerID].transform.position, 1));
        seq.Append(selectedCard.transform.DORotateQuaternion(new Quaternion(0, 0, 0, 0), 0.25f))
            .OnComplete(() =>
            {
                turnIsPlayed = true;
                anim.enabled = false;
            });
        seq.Play();

    }

    // A utility function to swap two elements
    private void swap(List<GameObject> cards, int i, int j)
    {
        var temp = cards[i];
        cards[i] = cards[j];
        cards[j] = temp;
    }

    private int partition(List<GameObject> cards, int low, int high)
    {
        // Choosing the pivot
        var pivot = cards[high];

        // Index of smaller element and indicates
        // the right position of pivot found so far
        int i = (low - 1);

        for (int j = low; j <= high - 1; j++)
        {
            var card = cards[j].GetComponent<CardInfo>().card;
            // If current element is smaller than the pivot
            if (card.cardNumber < pivot.GetComponent<CardInfo>().card.cardNumber)
            {
                // Increment index of smaller element
                i++;
                swap(cards, i, j);
            }
            else if (cards[j].GetComponent<CardInfo>().card.cardNumber == pivot.GetComponent<CardInfo>().card.cardNumber) // if they have the same card number we will combare by the card suit
            {
                if (cards[j].GetComponent<CardInfo>().card.cardsuit < pivot.GetComponent<CardInfo>().card.cardsuit)
                {
                    // Increment index of smaller element
                    i++;
                    swap(cards, i, j);
                }
            }
        }
        swap(cards, i + 1, high);
        return (i + 1);
    }

    // The main function that implements QuickSort
    // arr[] --> Array to be sorted,
    // low --> Starting index,
    // high --> Ending index
    private void quickSort(List<GameObject> cards, int low, int high)
    {
        int counter = 0;
        if (low < high)
        {
            counter++;
            // pi is partitioning index, arr[p]
            // is now at right place
            int pi = partition(cards, low, high);

            // Separately sort elements before
            // and after partition index
            quickSort(cards, low, pi - 1);
            quickSort(cards, pi + 1, high);
        }


    }
}
