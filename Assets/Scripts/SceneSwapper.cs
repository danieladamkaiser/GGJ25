using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwapper : MonoBehaviour
{
    public string mainMenuSceneName;
    public string level1SceneName;
    public string level2SceneName;
    public string level3SceneName;

    private int level = 1;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    public void AddLevel()
    {
        level++;
    }

    public void StartGame()
    {
        if (level == 1)
        {
            SceneManager.LoadScene(level1SceneName);
        }

        if (level == 2)
        {
            SceneManager.LoadScene(level2SceneName);
        }

        if (level >= 3)
        {
            SceneManager.LoadScene(level3SceneName);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
