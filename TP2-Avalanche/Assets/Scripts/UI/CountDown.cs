using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private int baseCount;

    private int count;

    private void Start()
    {
        count = baseCount;
        InvokeRepeating(nameof(LowerCountDown), 0, 1);
    }

    private void LowerCountDown()
    {
        countDownText.text = $"Returning to menu in {count}...";
        count--;
    }
}
