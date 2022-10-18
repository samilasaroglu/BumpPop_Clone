using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject ball, finish1;
    private float maxDistance,remainingDistance;
    public bool finalPart;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI moneyText,cloneCountLevelText,cloneCountPriceText,incomeLevelText,incomePriceText,totalBallCountText,levelText;
    [SerializeField] private GameObject levelFailed, levelCompleted,settingsButton,leaderBoard;
    [SerializeField] private TextMeshProUGUI[] ballScores;
    private int  cloneCountLevel, cloneCountPrice, incomeLevel, incomePrice,level;
    public int Income, cloneCount, money, totalBallCount;
    private bool settingsOn;
    private bool levelFinish;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        ball = GameObject.FindGameObjectWithTag("throwableBall");  
        finish1 = GameObject.FindGameObjectWithTag("Finish1");
        maxDistance = Vector3.Distance(ball.transform.position, finish1.transform.position);
        PlayerPrefsOperations();
        TotalBallCount();
    }
    void Update()
    {
        if (!finalPart)
        {
            ball = FırlatmaScript.instance.throwObject;
            remainingDistance = Vector3.Distance(ball.transform.position, finish1.transform.position);
            progressBar.fillAmount = 1 - (remainingDistance / maxDistance);
        }
        else
        {
            progressBar.fillAmount = 1;
        }

    }

    public void GainMoney()
    {
        money += Income;
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefsOperations();
    }

    public void PlayerPrefsOperations()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
            levelText.text = "LEVEL " + level;
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            level = 1;
            levelText.text = "LEVEL " + level;
        }
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
            moneyText.text = "" + money;
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            moneyText.text = "" + PlayerPrefs.GetInt("Money").ToString();
        }
        if (PlayerPrefs.HasKey("Income"))
        {
            Income = PlayerPrefs.GetInt("Income");
        }
        else
        {
            PlayerPrefs.SetInt("Income", 5);
            Income = 5;
        }
        if (PlayerPrefs.HasKey("IncomeLevel"))
        {
            incomeLevel = PlayerPrefs.GetInt("IncomeLevel");
            incomeLevelText.text = "" + incomeLevel + " LVL";
        }
        else
        {
            PlayerPrefs.SetInt("IncomeLevel", 1);
            incomeLevel = 1;
            incomeLevelText.text = "" + incomeLevel + " LVL";
        }
        if (PlayerPrefs.HasKey("IncomePrice"))
        {
            incomePrice = PlayerPrefs.GetInt("IncomePrice");
            incomePriceText.text = "" + incomePrice;
        }
        else
        {
            PlayerPrefs.SetInt("IncomePrice", 5);
            incomePrice = 5;
            incomePriceText.text = "" + incomePrice;
        }
        if (PlayerPrefs.HasKey("CloneCount"))
        {
            cloneCount = PlayerPrefs.GetInt("CloneCount");
        }
        else
        {
            PlayerPrefs.SetInt("CloneCount", 5);
            cloneCount = 5;
        }
        if (PlayerPrefs.HasKey("CloneCountLevel"))
        {
            cloneCountLevel = PlayerPrefs.GetInt("CloneCountLevel");
            cloneCountLevelText.text = "" + cloneCountLevel +" LVL";
        }
        else
        {
            PlayerPrefs.SetInt("CloneCountLevel", 1);
            cloneCountLevel = 1;
            cloneCountLevelText.text = "" + cloneCountLevel + " LVL";
        }
        if (PlayerPrefs.HasKey("CloneCountPrice"))
        {
            cloneCountPrice = PlayerPrefs.GetInt("CloneCountPrice");
            cloneCountPriceText.text = "" + cloneCountPrice;
        }
        else
        {
            PlayerPrefs.SetInt("CloneCountPrice", 5);
            cloneCountPrice = 5;
            cloneCountPriceText.text = "" + cloneCountPrice;
        }
    }

    public void LevelUpgrade() 
    {
        level++;
        PlayerPrefs.SetInt("Level", level);

        PlayerPrefsOperations();
    }
    public void IncomeUpgrade()
    {
        if(money > incomePrice)
        {
            Income++;
            PlayerPrefs.SetInt("Income", Income);

            money -= incomePrice;
            PlayerPrefs.SetInt("Money", money);

            incomeLevel++;
            PlayerPrefs.SetInt("IncomeLevel", incomeLevel);

            incomePrice += 10;
            PlayerPrefs.SetInt("IncomePrice", incomePrice);

            PlayerPrefsOperations();
        }
    }

    public void CloneCountUpgrade()
    {
        if(money > cloneCountPrice)
        {
            cloneCount++;
            PlayerPrefs.SetInt("CloneCount", cloneCount);

            money -= cloneCountPrice;
            PlayerPrefs.SetInt("Money", money);

            cloneCountLevel++;
            PlayerPrefs.SetInt("CloneCountLevel", cloneCountLevel);

            cloneCountPrice += 10;
            PlayerPrefs.SetInt("CloneCountPrice", cloneCountPrice);

            PlayerPrefsOperations();
        }
    }

    public void SettingsOnOff()
    {
        if (!settingsOn)
        {
            settingsButton.transform.GetChild(0).gameObject.SetActive(true);
            settingsOn = true;
        }
        else
        {
            settingsButton.transform.GetChild(0).gameObject.SetActive(false);
            settingsOn = false;
        }
    }

    public void LevelFailed()
    {
        levelFailed.SetActive(true);
    }
    public void LevelCompleted()
    {
        if (!levelFinish)
        {
            StartCoroutine(LevelCompleteOperations());
            levelFinish = true;
        }
    }

    public void TotalBallCount()
    {
        totalBallCount++;
        totalBallCountText.text = "" + totalBallCount;
    }

    IEnumerator LevelCompleteOperations()
    {
        yield return new WaitForSeconds(.2f);
        leaderBoard.SetActive(true);
        for(int i = 0; i < ballScores.Length; i++)
        {
            if (i < 4)
            {
                ballScores[i].text = "" +(totalBallCount-i+4);
            }
            if (i > 3 && i< 8)
            {
                ballScores[i].text = "" + (totalBallCount - i+3);
            }
            if (i == 8)
            {
                ballScores[i].text = "" + totalBallCount;
            }
        }
        // top sayılarını yazdır

        yield return new WaitForSeconds(2.5f);
        leaderBoard.SetActive(false);
        levelCompleted.SetActive(true);


    }
}
