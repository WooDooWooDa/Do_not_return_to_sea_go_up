using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraRig : NetworkBehaviour
{
    [Header("Winner Screen")]
    [SerializeField] GameObject winnerScreen;
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] TextMeshProUGUI winnerScoreText;

    [Header("Solo Winner Screen")]
    [SerializeField] GameObject soloWinnerScreen;

    [SerializeField] TextMeshProUGUI soloScoreText;

    [Header("Dead Screen")]
    [SerializeField] TextMeshProUGUI deadFollowingText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject deadScreen;

    [Header("ScoreBoard")]
    [SerializeField] GameObject scoreBoard;

    [Header("Player Item")]
    [SerializeField] GameObject playerItem;
    [SerializeField] TextMeshProUGUI playerItemText;
    [SerializeField] TextMeshProUGUI durationText;

    public GameObject AssociatedPlayer { get; set; }

    private Vector3 basePos = new Vector3(0, 3, -20);

    public GameObject GetPlayerItemUI()
    {
        return playerItem;
    }

    void Update()
    {
        if (AssociatedPlayer == null) {
            return;
        }
        transform.position = AssociatedPlayer.transform.position + basePos;
    }

    public void UpdatePlayerEffect(string effect, string duration)
    {
        playerItemText.text = $"Active Effect : {effect}";
        durationText.text = $"{duration} seconds left...";
    }

    public void FollowLeader()
    {
        var players = GameObject.Find("GameManager").GetComponent<GameManage>().GetPlayerList();
        GameObject leader = null;
        foreach (PlayerScore player in players)
        {
            if (player == gameObject.GetComponent<PlayerScore>()) {
                continue;
            } else if (leader == null) {
                leader = player.gameObject;
            }
        }
        AssociatedPlayer = leader;
        deadFollowingText.text = "Now following : " + AssociatedPlayer.GetComponent<Player>().Name;
    }

    public void SoloWinnerScreen(float score)
    {
        PersonnalScore(score);
        deadScreen.SetActive(false);
        scoreBoard.SetActive(false);
        soloWinnerScreen.SetActive(true);
        soloScoreText.text = $"You've reach {score:0.0} ft";
    }

    public void WinnerScreen(string winnerName, float winnerScore, float personnalScore)
    {
        PersonnalScore(personnalScore);
        deadScreen.SetActive(false);
        scoreBoard.SetActive(false);
        winnerScreen.SetActive(true);
        winnerText.text = winnerName;
        winnerScoreText.text = $"With a score of : {winnerScore:0.0} ft";
    }

    public void DeadScreen(float score)
    {
        deadScreen.SetActive(true);
        scoreText.text = $"You've reach {score:0.0} ft";
    }

    private void PersonnalScore(float score)
    {
        if (PlayerPrefs.GetFloat("maxHeigth") < score)
            PlayerPrefs.SetFloat("maxHeigth", score);
    }
}
