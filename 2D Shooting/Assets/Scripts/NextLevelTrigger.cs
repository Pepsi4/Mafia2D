using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public GameObject EndUIPanel;
    public Text ScoreText;
    public ScoreController scoreController;

    private void Start()
    {
        //Time.timeScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("End level trigger!");
        if (collision.gameObject.name == "Player")
        {
            EndUIPanel.SetActive(true);
            ScoreText.text = "Рахунок: " + scoreController.Score;
            //Time.timeScale = 0;
            GameObject.Find("Player").gameObject.SetActive(false);
            //SceneManager.LoadScene(LevelLoader.CurrentScene + 1);
        }
    }
}
