using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameCards gameCards;
    public Transform[] playersSpawnPoints; // it shall be saved in clockwise direction
    public int playersNumber = 4; //TODO: Connect it with the dealer class
    public GameObject[] players;
    public Transform AssemblyPoint;
    public Transform[] AssemblyPointChildren; // was added to prevent the card interference

    private int noOfPlayersReady = 0;

    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 0;
    }

    public void ReadyToJoinRound()
    {
        noOfPlayersReady++;
        if (noOfPlayersReady >= playersNumber - 1) // shall not count the main player as he is not organising his cards
        {
            StartCoroutine(Startround());
        }
    }

    public void NextRound()
    {
        StartCoroutine(Startround());
    }

    IEnumerator Startround()
    {
        foreach (var player in players)
        {
            player.GetComponent<Animator>().enabled = true;
            var playerClass = player.GetComponent<CardGamePlayer>();
            playerClass.playTurn();
            yield return new WaitUntil(() => playerClass.turnIsPlayed == true);
            playerClass.turnIsPlayed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
