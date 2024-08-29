using System.Collections.Generic;
using UnityEngine;
public class CardGamePlayer : MonoBehaviour
{
    protected List<GameObject> myCards = new List<GameObject>();
    public Animator anim;
    public int playerID;
    public bool turnIsPlayed = false;
    public void AddCard(GameObject card)
    {
        myCards.Add(card);
    }

    public virtual void playTurn()
    {

    }
}
