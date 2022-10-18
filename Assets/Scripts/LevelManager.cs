using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        GameManager.instance.LevelUpgrade();
    }
    public void BackToFirstLevel()
    {
        SceneManager.LoadScene(0);
        GameManager.instance.LevelUpgrade();
    }
}
