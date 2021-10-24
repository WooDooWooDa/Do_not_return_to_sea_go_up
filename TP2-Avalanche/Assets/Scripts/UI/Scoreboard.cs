using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private GameObject scoreRow;
    [SerializeField] private Transform grid;
    
    private List<GameObject> scoreRows = new List<GameObject>();
    private List<PlayerScore> playerScores = new List<PlayerScore>();
    private GameManage manager;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManage>();
        playerScores = manager.GetPlayerList();
        foreach (var player in playerScores)
        {
            scoreRows.Add(CreateRow(player));
        }
    }

    private void OnGUI()
    {
        playerScores = manager.GetPlayerList();
        List<GameObject> addedRows = new List<GameObject>();
        foreach (PlayerScore score in playerScores)
        {
            var isOnBoard = false;
            if (score == null) {
                continue;
            }
            foreach (var row in scoreRows)
            {
                if (row.GetComponent<ScoreRow>().GetName() == score.GetComponent<Player>().Name) {
                    row.GetComponent<ScoreRow>().UpdateScoreAndPos(score.GetMaxHeigth(), 1);
                    isOnBoard = true;
                }
            }
            if (!isOnBoard)
            {
                addedRows.Add(CreateRow(score));
            }
        }
        foreach (var row in addedRows)
        {
            scoreRows.Add(row);
        }
    }

    private GameObject CreateRow(PlayerScore player)
    {
        GameObject row = Instantiate(scoreRow);
        row.GetComponent<ScoreRow>().SetName(player.GetComponentInParent<Player>().Name);
        row.transform.SetParent(grid, false);
        row.transform.localScale = new Vector3(1, 1, 1);
        return row;
    }
}
