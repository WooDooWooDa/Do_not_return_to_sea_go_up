using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private GameObject scoreRow;
    [SerializeField] private Transform grid;
    
    private List<GameObject> scoreRows;
    private List<PlayerScore> playerScores;
    private GameManage manager;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManage>();
        playerScores = manager.GetPlayerList();

        foreach (var player in playerScores)
        {
            var row = Instantiate(scoreRow);
            row.GetComponent<ScoreRow>().SetName(player.GetComponentInParent<Player>().Name);
            row.transform.SetParent(grid, false);
            row.transform.localScale = new Vector3(1, 1, 1);
            scoreRows.Add(row);
        }
    }
}
