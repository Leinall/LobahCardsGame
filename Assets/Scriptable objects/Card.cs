using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    public Sprite gameSprite;
    public CardSuits cardsuit;
    public CardNumber cardNumber;
    public GameObject cardPrefab;
}
