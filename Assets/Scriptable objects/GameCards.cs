using UnityEngine;

[CreateAssetMenu(menuName = "GameCards")]
public class GameCards : ScriptableObject
{
    public Card[] cards;
}

public enum CardSuits { clubs, diamonds, hearts, spades }
public enum CardNumber { two, three, four, five, six, seven, eight, nine, ten, jack, queen, king, ace }
