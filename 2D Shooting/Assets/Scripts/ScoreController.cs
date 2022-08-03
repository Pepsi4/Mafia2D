using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int Score { get; set; }

    public Text ScoreUI;
    public void UpdateScoreUI()
    {
        ScoreUI.text = Score.ToString();
    }
}
