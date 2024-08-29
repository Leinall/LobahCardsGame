using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Dealer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private int playerID;
    private int cardsCountForEachPlayer;
    private int cardCounter;

    private Transform[] playersSpawnPoints;
    private UnityAction OnDistributionFinish;
    private void Start()
    {
        playersSpawnPoints = GameManager.instance.playersSpawnPoints;
        playerID = UnityEngine.Random.Range(0, 4);
        cardsCountForEachPlayer = 52 / GameManager.instance.playersNumber;
        CardsRandomizer(GameManager.instance.gameCards);
        OnDistributionFinish += InformPlayersToSortThierCards;
        OnDistributionFinish += CameraZoomIn;

        StartCoroutine(DistributeCards());
    }

    private void InformPlayersToSortThierCards()
    {
        // EveryOne please sort your cards
        foreach (var player in GameManager.instance.players)
        {
            if (player.GetComponent<CardGameNPC>() != null)
                player.GetComponent<CardGameNPC>().SortMyCards();
            else
                player.GetComponent<MainPlayerBehaviour>().UnFoldMyCards();
        }
    }

    IEnumerator DistributeCards()
    {
        _audioSource.Play();

        var cards = GameManager.instance.gameCards.cards;
        int cardID = 0;
        do
        {
            var card =
                GameObject.Instantiate(cards[cardCounter].cardPrefab, new Vector3(0, 1, 0), Quaternion.Euler(new Vector3(0, 0, 180)), playersSpawnPoints[playerID]);


            card.transform.localScale = Vector3.one * 0.2f;

            var pos = playersSpawnPoints[playerID].transform.position;

            var snappingValue = Vector3.zero;
            // the snapping direction shall change according to the direction of the player
            if (playerID == 1 || playerID == 3)
            {
                snappingValue = pos + new Vector3(cardID * 0.055f - 0.4f, 0, 0);
            }
            else
            {
                snappingValue = pos + new Vector3(0, 0, cardID * 0.055f - 0.4f);
            }
            card.transform.DOMove(snappingValue, 0.25f).SetEase(Ease.InCubic)
                .OnComplete(() =>
                {
                    var rot = playersSpawnPoints[playerID].transform.localRotation;
                    card.transform.localRotation = Quaternion.Euler(new Vector3(rot.x, rot.y, 180));
                });
            card.GetComponent<CardInfo>().card = cards[cardCounter];
            GameManager.instance.players[playerID].GetComponent<CardGamePlayer>().AddCard(card);
            cardID++;
            cardCounter++;
            yield return new WaitForSeconds(0.25f);

            if (cardCounter % 13 == 0)
            {
                cardID = 0;
                if (playerID >= GameManager.instance.playersNumber - 1)
                {
                    playerID = 0;
                }
                else
                {
                    playerID++;
                }
            }
        } while (cardCounter < 52);

        _audioSource.Stop();

        OnDistributionFinish.Invoke();
    }

    private void CameraZoomIn()
    {
        Camera.main.DOFieldOfView(49, 2f);
    }

    public void CardsRandomizer(GameCards gameCards)
    {
        for (int i = 0; i < gameCards.cards.Length / 2; i++)
        {
            // choose 2 random cards
            var index1 = UnityEngine.Random.Range(0, 51);
            var index2 = UnityEngine.Random.Range(0, 51);
            //switch cards
            var tempCard = gameCards.cards[index1];
            gameCards.cards[index1] = gameCards.cards[index2];
            gameCards.cards[index2] = tempCard;
        }
    }
}
