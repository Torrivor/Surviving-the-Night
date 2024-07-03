using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   public enum GameState
    {
    Gameplay,
    Wstrzymano,
    Koniec_Gry,
    LevelUp
    }

    public GameState currentState;

    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject levelUpScreen;

    [Header("Current Stat Displays")]
    public Text currentHPDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    [Header("Results Screen Displays")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    public Text timeSurvivedDisplay;
    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveItemUI = new List<Image>(6);

    [Header("Stopwatch")]
    public float timeLimit; //czas w sek
    float stopwatchTime; //ile czasu uplynelo od staryu stopwach
    public Text stopwatchDisplay;

    //oznacza czy gra jest zakonczona
    public bool isKoniec_Gry = false;

    //znacznik sprawdzajacy czy gracz wybiera upgradea
    public bool choosingUpgrade;

    //odniesienie do obiektu gracza
    public GameObject playerObject;

    public void Awake()
    {
        //ostrzerzenie jesli jest jeszczejeden singleton tego typu w grze
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
        }

        DisableScreen();
    }
    void Update()
    {


        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopwatch();
                break;

            case GameState.Wstrzymano:
                CheckForPauseAndResume();
                break;

            case GameState.Koniec_Gry:
                if (!isKoniec_Gry)
                {
                    isKoniec_Gry = true;
                    Time.timeScale = 0f; //calkowicie zatrzymuje gre
                    Debug.Log("Koniec Gry");
                    DisplayResults();
                }
                break;

            case GameState.LevelUp:
                if(!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f; //zatrzymuje gre
                    Debug.Log("Upgrades shown");
                    levelUpScreen.SetActive(true);
                }
                break;

            default:
                Debug.LogWarning("STATE DOES NOT EXIST");
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }


    public void PauseGame()
    {
        if (currentState != GameState.Wstrzymano)
        {
            previousState = currentState;
            ChangeState(GameState.Wstrzymano);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Gra jest wstrzymana");
        }
    }
    public void ResumeGame()
    {
        if (currentState == GameState.Wstrzymano)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Wznowienie gry");
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState ==GameState.Wstrzymano)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void Koniec_Gry()
    {
        timeSurvivedDisplay.text = stopwatchDisplay.text;
        ChangeState(GameState.Koniec_Gry);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponAndPassiveItemUI(List<Image> chosenWeaponData, List<Image> chosenPassiveItemData)
    {
        if (chosenWeaponData.Count != chosenWeaponsUI.Count || chosenPassiveItemData.Count != chosenPassiveItemUI.Count)
        {
            Debug.Log("rozne dlugosci list broni lub pasywek");
            return;
        }

        //przydziela chosenweponData to chosenweaponUI
        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            //upewnia czy sprite danego elementu w chosenWeaponData nie jest null
            if (chosenWeaponData[i].sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponData[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveItemUI.Count; i++)
        {
            //upewnia czy sprite danego elementu w chosenPassiveItemData nie jest null
            if (chosenPassiveItemData[i].sprite)
            {
                chosenPassiveItemUI[i].enabled = true;
                chosenPassiveItemUI[i].sprite = chosenPassiveItemData[i].sprite;
            }
            else
            {
                chosenPassiveItemUI[i].enabled = false;
            }
        }
    }

    void UpdateStopwatch()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();

        if(stopwatchTime >= timeLimit)
        {
            Koniec_Gry();
        }
    }

    void UpdateStopwatchDisplay()
    {
        //przelicza z sec na nasze
        int min = Mathf.FloorToInt(stopwatchTime / 60);
        int sec = Mathf.FloorToInt(stopwatchTime % 60);
        //pokazuje czas normalnie
        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1f; //powrot do gry
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}