using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI position;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI score;

    public void SetName(string name)
    {
        playerName.text = name;
    }

    public string GetName()
    {
        return playerName.text;
    }

    public void UpdateScoreAndPos(float score, int pos)
    {
        position.text = pos.ToString();
        this.score.text = score.ToString("0.0");
    }
}
