using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject feedbackPanel;
    [SerializeField]
    private TMP_Text feedbackPanelText;
    [SerializeField]
    private GameObject EndPanel;
    [SerializeField]
    private TMP_Text EndPanelText;

    private string winnersData = null;


    private void Start()
    {
        feedbackPanel.SetActive(false);
        EndPanel.SetActive(false);
    }

    public void PLayGame()
    {
        Time.timeScale = 1.0f;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EnableFeedbackPanel(int winnerIndex)
    {
        feedbackPanelText.text = string.Format("PLayer {0} wins", winnerIndex + 1);
        feedbackPanel.SetActive(true);
        StartCoroutine(FeedbackPanelTurnOff());
    }

    IEnumerator FeedbackPanelTurnOff()
    {
        yield return new WaitForSeconds(3);
        feedbackPanel.SetActive(false);
        GameManager.instance.NextRound();
    }

    public void LoadWinnersData()
    {
        int[] winnersDataArr = new int[GameManager.instance.playersNumber];

        var winners = GameReferences.instance.assemblyPoint.getWinnerIndices();
        foreach (var winner in winners)
        {
            winnersDataArr[winner]++;
        }
        winnersData = string.Format("Player 1: {0}\n Player 2: {1}\n Player 3: {2}\n Player 4: {3}\n Winner: Player{4}"
            , winnersDataArr[0], winnersDataArr[1], winnersDataArr[2], winnersDataArr[3], CalculateWinner(winnersDataArr));
        print(winnersData);
        winnersData = CalculateWinner(winnersDataArr) + "";
    }

    public void DisplayWinnersData()
    {
        LoadWinnersData();
        EndPanelText.text = "Congratulations \n Player " + winnersData;

        EndPanel.SetActive(true);
    }

    private int CalculateWinner(int[] winnersDataArr)
    {
        return winnersDataArr.Select((x, i) => new { x, i }).Aggregate((a, a1) => a.x > a1.x ? a : a1).i;
    }
}
