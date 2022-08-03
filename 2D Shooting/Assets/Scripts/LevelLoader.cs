using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static int CurrentScene { get; set; } = 2;

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        CurrentScene = level;
    }

    public void LoadNextLevel()
    {
        if (CurrentScene < 4)
            SceneManager.LoadScene(CurrentScene + 1);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(CurrentScene);
    }

    public void LoadSelectLevelScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
